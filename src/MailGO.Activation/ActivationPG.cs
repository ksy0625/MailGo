/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.23 Tran Dinh Thoai
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using OL = Microsoft.Office.Interop.Outlook;
using OC = Microsoft.Office.Core;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Globalization;
using System.Reflection;
// ↓[追加]
using Shell32;
using System.IO;
// ↑[追加]
using OT = Microsoft.Office.Tools;

namespace DataDesign.MailGO.Activation
{
    public class ActivationPG : Model.MActivationPG
    {
        private OL.Application m_outlook = null;
        private System.Timers.Timer m_timer_load;
        private bool m_installed;
        private bool m_evaluated;
        private bool m_activated;
        private bool m_expired;
        private DateTime? m_expired_date;
        private string m_license_id;
        private Model.IEmail m_nocheck_email = null;

        private OT.Ribbon.RibbonButton cmdMailGO = null;
        private OT.Ribbon.RibbonButton cmdOption = null;

        public ActivationPG(Model.IMailGoPG v_mailgo, OL.Application v_outlook, OT.Ribbon.RibbonButton cmdBtn, OT.Ribbon.RibbonButton optBtn) : base(v_mailgo) 
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Creating package ...");

            this.m_outlook = v_outlook;
            this.OnLoadOption();

            this.cmdMailGO = cmdBtn;
            this.cmdOption = optBtn;
            this.CreateToolbar();

            this.m_timer_load = new System.Timers.Timer(10);
            this.m_timer_load.Elapsed += new System.Timers.ElapsedEventHandler(TimerLoad_Elapsed);
            this.m_timer_load.Start();
            this.m_mailgo.Track.Debug("ActivationPG: Start already.");
        }

        void TimerLoad_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Start TimerLoad_Tick ...");
            this.m_timer_load.Stop();
  
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ja-JP");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");

            this.m_mailgo.Track.Debug("ACTIVATION: checking license ...");
            this.m_mailgo.License.CheckLicense(out this.m_installed, out this.m_evaluated, out this.m_activated, out this.m_expired, out this.m_expired_date, out this.m_license_id);

            if (!this.m_installed)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is not installed!");
                return;
            }

            this.m_mailgo.Track.Debug("ACTIVATION: License is installed!");

            this.cmdMailGO.Enabled = true;
            this.cmdOption.Enabled = true;

            if (this.m_activated)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is activated!");
                this.UpdateMailGO();
            }
            else if (this.m_expired)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is expired!");
                this.SetOffMailGO();
            }
            else
            {
                if (this.IsFirstUse())
                {
                    this.m_mailgo.Track.Debug("ACTIVATION: First use!");

                    this.ClearFirstUse();

                    if (this.AskActivation())
                    {
                        this.m_mailgo.Track.Debug("ACTIVATION: Activate license ...");

                        bool t_request;
                        bool t_activate;
                        Model.IEmail t_email;
                        ActivateLicenseWF.ActivateLicense(this.m_mailgo, this.m_license_id, out t_request, out t_activate, out t_email);

                        if (t_activate)
                        {
                            this.m_mailgo.Track.Debug("ACTIVATION: License is activated!");
                            this.m_timer_load.Start();
                            return;
                        }
                        if (t_request)
                        {
                            this.m_mailgo.Track.Debug("ACTIVATION: Activation ID is requested!");
                            t_email.Sender = this.GetSender(null);
                            this.m_nocheck_email = t_email;
                            this.SendEmail(t_email);
                        }
                    }
                    else
                    {
                        this.m_mailgo.Track.Debug("ACTIVATION: Deadline is displayed!");
                        this.ShowDeadline();
                    }
                    this.SetOnMailGO();
                }
                else
                {
                    this.UpdateMailGO();
                }
            }
        }

        private void ShowDeadline()
        {
            if (this.m_expired_date.HasValue)
            {
                DeadlineWF t_form = new DeadlineWF(this.m_expired_date.Value);
                t_form.ShowDialog();
            }
        }

        private void SendEmail(Model.IEmail v_email)
        {
            OL._MailItem t_item =(OL._MailItem) this.m_outlook.CreateItem(OL.OlItemType.olMailItem);
            t_item.To = string.Join(";", v_email.TO.ToArray());
            t_item.CC = string.Join(";", v_email.CC.ToArray());
            t_item.Subject = v_email.Subject;
            t_item.Body = v_email.Body;
            //2009/6/6修正  OL.OlDefaultFolders.olFolderOutbox→OL.OlDefaultFolders.olFolderSentMail
            t_item.SaveSentMessageFolder = this.m_outlook.Session.GetDefaultFolder(OL.OlDefaultFolders.olFolderSentMail);
            t_item.Send();
        }

        private string GetSender(OL.MailItem v_item)
        {
            string t_sender=null;
            try
            {
                if (v_item == null)
                {
                    if (this.m_outlook.Session.CurrentUser.AddressEntry.Type == "EX")
                    {
                        t_sender = GetSMTPAddressOfExchangeUser(v_item);
                    }
                    else
                    {
                        t_sender = this.m_outlook.Session.CurrentUser.Address;
                    }                        
                }
                else
                {
                    if (this.m_outlook.Session.CurrentUser.AddressEntry.Type == "EX")
                    {
                        t_sender = GetSMTPAddressOfExchangeUser(v_item);
                    }
                    else
                    {
                        t_sender = this.m_outlook.Session.CurrentUser.Address;
                    }
                }

                this.m_mailgo.Track.Debug("ACTIVATION: Sender = " + (t_sender == null ? "NULL" : t_sender));
            }
            catch (Exception ex)
            {
                this.m_mailgo.Track.Error("GetSender ERROR: " + ex.Message);
                this.m_mailgo.Track.Error(ex);
                return t_sender;
            }
            return t_sender;
        }

        
        private bool OL2K7()
        {
            string[] fields = this.m_outlook.Version.Split(new char[] { '.' });
            int version = 0;
            int.TryParse(fields[0], out version);

            this.m_mailgo.Track.Debug("Version string: " + this.m_outlook.Version);
            this.m_mailgo.Track.Debug("Version number: " + version.ToString());
            
            return (version >= 12);
        }

        private void ClearFirstUse()
        {
            string t_subkey;
            string t_name;
            string t_boot = "1";

            this.GetBootKeys(out t_subkey, out t_name);
            this.SetValue(Registry.CurrentUser, t_subkey, t_name, t_boot);
        }

        private void SetFirstUse()
        {
            string t_subkey;
            string t_name;
            string t_boot = "0";

            this.GetBootKeys(out t_subkey, out t_name);
            this.SetValue(Registry.CurrentUser, t_subkey, t_name, t_boot);
        }

        private bool IsFirstUse()
        {
            string t_subkey;
            string t_name;
            string t_boot;

            this.GetBootKeys(out t_subkey, out t_name);
            this.GetValue(out t_boot, Registry.CurrentUser, t_subkey, t_name);

            return (t_boot == null || t_boot != "1");
        }

        private bool AskActivation()
        {
            DialogResult t_result;
            AskActivationWF t_form = new AskActivationWF();

            t_result = t_form.ShowDialog();

            return (t_result == DialogResult.Yes);
        }

        private bool InputNewLicenseId()
        {
            DialogResult t_result;
            NewLicenseWF t_form = new NewLicenseWF(this.m_mailgo);

            t_result = t_form.ShowDialog();

            return (t_result == DialogResult.OK);
        }

        private void SetOffMailGO()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Turn off MailGO ...");

            this.m_status = false;
            this.cmdMailGO.Label = ToolBarWF.Instance.lblMailGOTextOff.Text;
            this.cmdMailGO.ScreenTip = ToolBarWF.Instance.lblMailGOTipOff.Text;
            this.OnSaveOption();
        }

        private void SetOnMailGO()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Turn on MailGO ...");

            this.m_status = true;
            this.cmdMailGO.Label = ToolBarWF.Instance.lblMailGOTextOn.Text;
            this.cmdMailGO.ScreenTip = ToolBarWF.Instance.lblMailGOTipOn.Text;
            this.OnSaveOption();
        }

        private void UpdateMailGO()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Update MailGO Status ...");
            if (this.m_status)
            {
                this.cmdMailGO.Label = ToolBarWF.Instance.lblMailGOTextOn.Text;
                this.cmdMailGO.ScreenTip = ToolBarWF.Instance.lblMailGOTipOn.Text;
            }
            else
            {
                this.cmdMailGO.Label = ToolBarWF.Instance.lblMailGOTextOff.Text;
                this.cmdMailGO.ScreenTip = ToolBarWF.Instance.lblMailGOTipOff.Text;
            }
            this.m_mailgo.Track.Debug("ACTIVATION: END UpdateMailGO ");
        }

        private void CreateToolbar()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Creating toolbar ...");
            /*
            this.cmdMailGO.Label = ToolBarWF.Instance.lblMailGOTextOn.Text;
            this.cmdMailGO.ScreenTip = ToolBarWF.Instance.lblMailGOTipOn.Text;

            this.cmdOption.Label = ToolBarWF.Instance.lblOptionTextOn.Text;
            this.cmdOption.ScreenTip = ToolBarWF.Instance.lblOptionTipOn.Text;
            */


            /*
                        this.cmdMailGO.Click += new OC._CommandBarButtonEvents_ClickEventHandler(cmdMailGO_Click);
                        this.UpdateMailGO();

                        this.cmdOption = (OC.CommandBarButton)this.m_command_bar.Controls.Add(OC.MsoControlType.msoControlButton, 1, "", this.m_command_bar.Controls.Count + 1, false);

                        this.cmdOption.Caption = ToolBarWF.Instance.lblOptionTextOn.Text;

                        this.cmdOption.Style = OC.MsoButtonStyle.msoButtonCaption;

                        this.cmdOption.Click += new OC._CommandBarButtonEvents_ClickEventHandler(cmdOption_Click);            

                        //this.m_outlook.ItemSend += new OL.ApplicationEvents_10_ItemSendEventHandler(Outlook_ItemSend);
                        Outlook10EventHelper outlookHeper = new Outlook10EventHelper();
                        outlookHeper.SetupConnection(this.m_outlook, this);
            */
            this.m_mailgo.Track.Debug("ACTIVATION:// END Creating toolbar ");
        }

        private bool NoCheckEmail(Model.IEmail v_email)
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Is no-check email?");

            if (this.m_nocheck_email == null) return false;
            
            Model.IEmail t_email = this.m_nocheck_email;
            this.m_nocheck_email = null;
            string t_target;
            string t_source;

            this.m_mailgo.Track.Debug("ACTIVATION: Check TO list");
            t_source = string.Join(";", v_email.TO.ToArray()).ToLower();
            t_target = string.Join(";", t_email.TO.ToArray()).ToLower();
            this.m_mailgo.Track.Debug("ACTIVATION: Source = " + t_source + " . Target = " + t_target);
            if (t_source != t_target) return false;

            this.m_mailgo.Track.Debug("ACTIVATION: Check CC list");
            t_source = string.Join(";", v_email.CC.ToArray()).ToLower();
            t_target = string.Join(";", t_email.CC.ToArray()).ToLower();
            this.m_mailgo.Track.Debug("ACTIVATION: Source = " + t_source + " . Target = " + t_target);
            if (t_source != t_target) return false;

            this.m_mailgo.Track.Debug("ACTIVATION: No-check email!");
            return true;
        }
                 
        public void Outlook_ItemSend(object v_item, ref bool v_cancel)
        {

            this.m_mailgo.Track.Debug("ACTIVATION: Outlook's email sent event handler ...");

            if (!this.m_installed) return;

            this.m_mailgo.Track.Debug("ACTIVATION: License is installed and status is on!");
            
            OL.MailItem t_item = v_item as OL.MailItem;
            Model.IEmail t_email = new Model.MEmail();

            t_email.Sender = this.GetSender(t_item);
            t_email.Body = t_item.Body;


            /* 2011/10/30 modified to handle null by msekine */
            if (t_email.Body == null)
            {
                t_email.Body = "\r\n";
            }

            t_email.Subject = t_item.Subject;

            //2011/10/11 そもそもMailGo Statusがfalseの場合はチェックしない
            if (!this.m_status) return;

            foreach (OL.Recipient t_recipient in t_item.Recipients)
            {
                string address = string.Empty;
                if (t_recipient.AddressEntry.Type == "EX")
                    address = GetSMTPAddressOfExchangeUser(t_item);
                else
                    address = t_recipient.Address;

                if (t_recipient.Type == (int)OL.OlMailRecipientType.olTo)
                {
                    t_email.TO.Add(address);
                }

                if (t_recipient.Type == (int)OL.OlMailRecipientType.olCC)
                {
                    t_email.CC.Add(address);
                }
            }

            if (this.NoCheckEmail(t_email))
            {
                ConfirmRequestWF t_form = new ConfirmRequestWF();
                DialogResult t_result = t_form.ShowDialog();
                if (t_result != DialogResult.Yes)
                {
                    v_cancel = true;
                }
                return;
            }

            if (!this.m_status) return;

            this.OnBeforeSendEmail(t_email, out v_cancel);

            // ↓[追加]添付ファイルプロパティチェック機能
            // 既存チェックでキャンセルしている場合は、メール送信をキャンセルして終了する。
            if (v_cancel == true)
            {
                return;
            }
            
            // 2009/9/18 ファイルプロパティオプションのチェック
            if (this.m_option.CheckWord == false &&
                this.m_option.CheckExcel == false &&
                this.m_option.CheckPowerPoint == false &&
                this.m_option.CheckText == false ) return;

            // OSのバージョンを確認する。
            String OS_Version = "";
            OperatingSystem osInfo = Environment.OSVersion;

            if (osInfo.Platform == PlatformID.Win32NT && osInfo.Version.Major == 5 && osInfo.Version.Minor == 1)
            {
                OS_Version = "Windows XP";
            }
            else if (osInfo.Platform == PlatformID.Win32NT && osInfo.Version.Major == 6 && osInfo.Version.Minor == 0)
            {
                OS_Version = "Windows Vista";
            }
            // 2010/9/13
            else if (osInfo.Platform == PlatformID.Win32NT && osInfo.Version.Major == 6 && osInfo.Version.Minor == 1)
            {
                OS_Version = "Windows 7";
            }
            else if(osInfo.Version.Major == 6 && osInfo.Version.Minor == 2)
                OS_Version = "Windows 8.0";
            else if (osInfo.Version.Major == 6 && osInfo.Version.Minor == 3)
                OS_Version = "Windows 8.1";
            else if (osInfo.Version.Major == 10 && osInfo.Version.Minor == 0)
                OS_Version = "Windows 10";

            //

            // 添付ファイルの有無を確認する。
            int cnt = 0;
            foreach (OL.Attachment objAttachment in t_item.Attachments)
            {
                cnt++;
            }

            // 以下のいづれかに該当するときに添付ファイルチェックを行う。
            // ・OSのバージョンがWindows XPでかつ添付ファイル有の場合
            // ・OSのバージョンがWindows Vistaでかつ添付ファイル有の場合
            if(cnt > 0 && (OS_Version == "Windows XP" || OS_Version == "Windows Vista" || OS_Version == "Windows 7"||
                OS_Version == "Windows 8.0" || OS_Version == "Windows 8.1" || OS_Version == "Windows 10"))
            {
                // メール本文の宛先を取得する。
                string t_company = this.ReadCompany(t_email.Body);

                // Outlookのバージョンを取得する。
                string oKeyName = @"Outlook.Application\CurVer";
                string oGetValueName = "";
                string outlook_version = "";
                try
                {
                    RegistryKey rKey = Registry.ClassesRoot.OpenSubKey(oKeyName);
                    outlook_version = (string)rKey.GetValue(oGetValueName);
                    rKey.Close();
                }
                catch (NullReferenceException)
                {
                    this.m_mailgo.Track.Debug("レジストリ［" + oKeyName + "］の［" + oGetValueName + "］がありません。");
                }

                // Outlookのバージョンに対応するレジストリキーを設定する。
                string rKeyName = "";
                // Outlook 2002
                if (outlook_version == "Outlook.Application.10")
                {
                    rKeyName = @"Software\Microsoft\Office\10.0\Outlook\Security";
                }
                // Outlook 2003
                else if (outlook_version == "Outlook.Application.11") 
                {
                    rKeyName = @"Software\Microsoft\Office\11.0\Outlook\Security";
                }
                // Outlook 2007
                else if (outlook_version == "Outlook.Application.12")
                {
                    rKeyName = @"Software\Microsoft\Office\12.0\Outlook\Security";
                }
                // OutLook 2010  2010/09/13
                else if (outlook_version == "Outlook.Application.14")
                {
                    rKeyName = @"Software\Microsoft\Office\14.0\Outlook\Security";
                }
                else if (outlook_version == "Outlook.Application.16")
                {
                    rKeyName = @"Software\Microsoft\Office\16.0\Outlook\Security";
                }
                // その他
                else
                {
                    // 添付ファイルプロパティチェック機能を終了する。
                    return;
                }

                string rGetValueName = "OutlookSecureTempFolder";
                string location = "";

                try
                {
                    RegistryKey rKey = Registry.CurrentUser.OpenSubKey(rKeyName);
                    location = (string)rKey.GetValue(rGetValueName);
                    rKey.Close();
                }
                catch (NullReferenceException)
                {
                    this.m_mailgo.Track.Debug("レジストリ［" + rKeyName + "］の［" + rGetValueName + "］がありません。");
                }
                
                // 添付ファイル数だけ以下を繰り返す。
                foreach (OL.Attachment objAttachment in t_item.Attachments)
                {
                    // 　拡張子チェック　2010/9/18
                    String t_extention = "";
                    t_extention = Path.GetExtension(objAttachment.FileName);

                    if (t_extention == ".doc")
                    {
                        if (this.m_option.CheckWord == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".docx")
                    {
                        if (this.m_option.CheckWord == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".xls")
                    {
                        if (this.m_option.CheckExcel == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".xlsx")
                    {
                        if (this.m_option.CheckExcel == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".csv")
                    {
                        if (this.m_option.CheckExcel == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".ppt")
                    {
                        if (this.m_option.CheckPowerPoint == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".pptx")
                    {
                        if (this.m_option.CheckPowerPoint == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else if (t_extention == ".txt")
                    {
                        if (this.m_option.CheckText == true)
                        {
                            // チェック対象。何もしないでスルーする。
                        }
                        else
                        {
                            // チェック対象外。
                            continue;
                        }
                    }
                    else
                    {
                        // チェック対象外。
                        continue;
                    }

                    // 　添付ファイルのプロパティ「タイトル」の値を取得する。
                   
                    int TITLE_ID = 1000;
                    // OSがWindows XPの場合
                    if (OS_Version == "Windows XP")
                    {
                        TITLE_ID = 10;
                    }
                    // OSがWindows Vistaの場合
                    else if (OS_Version == "Windows Vista")
                    {
                        TITLE_ID = 21;
                    }
                    // OSがWindows 7の場合 2010/09/13
                    else if (OS_Version == "Windows 7")
                    {
                        TITLE_ID = 21;
                    }
/*
                    else if (OS_Version == "Windows 8.0")
                    {
                        TITLE_ID = 22;
                    }
                    else if (OS_Version == "Windows 8.1")
                    {
                        TITLE_ID = 23;
                    }
                    else if (OS_Version == "Windows 10")
                    {
                        TITLE_ID = 31;
                    }
*/
                    ShellClass shell = new ShellClass();
                    Folder folder = shell.NameSpace(location);
                    FolderItem folderItem = folder.ParseName(objAttachment.FileName);

                    String t_title = "";
                    if (folderItem != null)
                    {
                        t_title = folder.GetDetailsOf(folderItem, TITLE_ID);
                        folderItem = null;
                    }
                    folder = null;
                    shell = null;
                    
                    // 
                    String l_title = "";
                    String l_company = "";

                    try
                    {
                        l_title = t_title.Trim().ToLower();
                    }
                    catch (NullReferenceException)
                    {
                        l_title = "";
                        t_title = "";
                    }

                    try
                    {
                        l_company = t_company.Trim().ToLower();
                    }
                    catch (NullReferenceException)
                    {
                        l_company = "";
                        t_company = "";
                    }

                    // メール本文の宛先と「タイトル」の値を比較する。
                    if (l_company == l_title)
                    {
                        // 一致する場合、何もしないでスルーする。

                    }
                    else
                    {
                        // 一致しない場合、確認ダイアログを表示する。                       
                        System.Windows.Forms.DialogResult t_result;
                        NotMatchWF t_form = new NotMatchWF(objAttachment.FileName, t_title, t_company);
                        t_result = t_form.ShowDialog();

                        // 送信実行ボタン押下時、そのまま続行。
                        if (t_result == System.Windows.Forms.DialogResult.OK)
                        {
                            v_cancel = false;
                        }

                        // 送信中止ボタン押下時、送信中止処理を実施。
                        if (t_result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            v_cancel = true;
                            return;
                        }
                       
                    }
                                      
                }

            }
            // ↑[追加]添付ファイルプロパティチェック機能
            this.m_mailgo.Track.Debug("Cancel sending mail with v_cancel = " + v_cancel);
        }

        // ↓[追加]添付ファイルプロパティチェック機能
        private string ReadCompany(string v_body)
        {
            const string c_signals = " 　\t\n\r";

            string msgBody = v_body.Trim();
            string t_company = "";
            if (msgBody == string.Empty)
                return t_company;

            char t_char;
            for (int t_idx = 0; t_idx < msgBody.Length; t_idx++)
            {
                t_char = msgBody[t_idx];
                if (c_signals.IndexOf(t_char) >= 0) break;
                t_company += t_char;
            }

            return t_company;
        }
        // ↑[追加]添付ファイルプロパティチェック機能


        private string GetSMTPAddressOfExchangeUser(OL.MailItem mail)
        {
            return mail.SenderEmailAddress;
            //MessageBox.Show(mail.SenderEmailAddress);
            /*
                        const uint PR_SMTP_ADDRESS = 0x39FE001E;

                        MAPI.SessionClass objSession = new MAPI.SessionClass();
                        objSession.MAPIOBJECT = recipient.Application.Session.MAPIOBJECT;
                        MAPI.AddressEntry addEntry = (MAPI.AddressEntry)objSession.GetAddressEntry(recipient.EntryID);            
                        MAPI.Field field = (MAPI.Field)((MAPI.Fields)addEntry.Fields).get_Item(PR_SMTP_ADDRESS, null);

                        return field.Value.ToString();
            */
            string PR_SMTP_ADDRESS =
                @"http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
            if (mail == null)
            {
                throw new ArgumentNullException();
            }
            if (mail.SenderEmailType == "EX")
            {
                OL.AddressEntry sender =
                    mail.Sender;
                if (sender != null)
                {
                    //Now we have an AddressEntry representing the Sender
                    if (sender.AddressEntryUserType ==OL.OlAddressEntryUserType.olExchangeUserAddressEntry
                        || sender.AddressEntryUserType ==OL.OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
                    {
                        //Use the ExchangeUser object PrimarySMTPAddress
                        OL.ExchangeUser exchUser =sender.GetExchangeUser();
                        if (exchUser != null)
                        {
                            return exchUser.PrimarySmtpAddress;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return sender.PropertyAccessor.GetProperty(PR_SMTP_ADDRESS) as string;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return mail.SenderEmailAddress;
            }
        }


        public void cmdMailGO_Click()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: MailGO Status button is clicked!");

            if (!this.m_installed) return;

            this.m_mailgo.Track.Debug("ACTIVATION: License is installed!");

            if (!this.m_activated && this.m_expired)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is not activated and expired!");
                if (this.InputNewLicenseId())
                {
                    this.m_timer_load.Start();
                }
            }
            else
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is activated or not expired!");
                if (this.m_status)
                {
                    this.SetOffMailGO();
                }
                else
                {
                    this.SetOnMailGO();
                }
            }
        }

        public void cmdOption_Click()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: MailGO Option button is clicked!");
            this.OnChangeOption();
        }

        private void GetOptionKeys(out string v_subkey, out string v_name)
        {
            v_subkey = @"SOFTWARE\Kodai";
            v_name = "OptionMailGO";
        }

        private void GetBootKeys(out string v_subkey, out string v_name)
        {
            v_subkey = @"SOFTWARE\Kodai";
            v_name = "BootMailGO";
        }

        private void GetValue(out string v_value, RegistryKey v_key, string v_subkey, string v_name)
        {
            RegistryKey t_regkey = v_key.OpenSubKey(v_subkey);
            if (t_regkey == null)
            {
                v_value = null;
            }
            else
            {
                v_value = (string)t_regkey.GetValue(v_name);
                t_regkey.Close();
            }
        }

        private void SetValue(RegistryKey v_key, string v_subkey, string v_name, string v_value)
        {
            RegistryKey t_regkey = v_key.CreateSubKey(v_subkey);
            t_regkey.SetValue(v_name, v_value);
            t_regkey.Close();
        }

        //private void GetValue(out string v_value, string v_subkey, string v_name)
        //{
        //    RegistryKey t_regkey = Registry.CurrentUser.CreateSubKey(v_subkey);
        //    v_value = (string)t_regkey.GetValue(v_name);
        //    t_regkey.Close();
        //}

        //private void SetValue(string v_subkey, string v_name, string v_value)
        //{
        //    RegistryKey t_regkey = Registry.CurrentUser.CreateSubKey(v_subkey);
        //    t_regkey.SetValue(v_name, v_value);
        //    t_regkey.Close();
        //}

        protected override void OnActivate()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Start activating license ...");

            bool t_request;
            bool t_activate;
            Model.IEmail t_email;
            ActivateLicenseWF.ActivateLicense(this.m_mailgo, this.m_license_id, out t_request, out t_activate, out t_email);

            if (t_request)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: Activation ID is requested!");
                t_email.Sender = this.GetSender(null);
                this.m_nocheck_email = t_email;
                this.SendEmail(t_email);
            }
            if (t_activate)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is activated!");
                this.m_timer_load.Start();
            }
        }

        protected override void OnCleanUp()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Start cleaning up ...");

            if (this.m_outlook != null)
            {
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(this.m_outlook);
                }
                finally
                {
                    this.m_outlook = null;
                }
            }
        }

        protected override void OnChangeOption()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Start changing option ...");

            OptionWF t_form;
            DateTime? t_deadline = null;

            if (!this.m_installed) return;

            this.m_mailgo.Track.Debug("ACTIVATION: License is installed!");

            if (!this.m_activated && !this.m_expired)
            {
                this.m_mailgo.Track.Debug("ACTIVATION: License is not activated and not expired!");
                t_deadline = this.m_expired_date;
            }

            t_form = new OptionWF(this.m_mailgo, t_deadline);
            t_form.ShowDialog();
        }

        protected override void OnLoadOption()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Loading option ...");

            string t_subkey;
            string t_name;
            string t_option;

            this.m_option.CheckAllAddress = true;
            this.m_option.CheckOnlyFirstAddress = false;
            this.m_option.CheckCCLine = true;
            this.m_option.CheckSameSuffix = true;
            this.m_status = false;
            // 2010/9/18
            this.m_option.CheckWord = false;
            this.m_option.CheckExcel = false;
            this.m_option.CheckPowerPoint = false;
            this.m_option.CheckText = false;
            //

            this.GetOptionKeys(out t_subkey, out t_name);
            this.GetValue(out t_option, Registry.CurrentUser, t_subkey, t_name);

            // 2009/5/27
            // [ORG]
            // if (t_option == null || t_option.Length < 5)
            
            if (t_option == null || t_option.Length < 9)
            {
                this.OnSaveOption();
            }
            else
            {
                this.m_option.CheckAllAddress = (t_option[0] == '1');
                this.m_option.CheckOnlyFirstAddress = (t_option[1] == '1');
                this.m_option.CheckCCLine = (t_option[2] == '1');
                this.m_option.CheckSameSuffix = (t_option[3] == '1');
                this.m_status = (t_option[4] == '1');
                // 2010/9/18
                this.m_option.CheckWord = (t_option[5] == '1');
                this.m_option.CheckExcel = (t_option[6] == '1');
                this.m_option.CheckPowerPoint = (t_option[7] == '1');
                this.m_option.CheckText = (t_option[8] == '1');
                //

            }
        }

        protected override void OnSaveOption()
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Start saving option ...");

            string t_subkey;
            string t_name;
            string t_option = "";

            this.GetOptionKeys(out t_subkey, out t_name);
            t_option += (this.m_option.CheckAllAddress ? '1' : '0');
            t_option += (this.m_option.CheckOnlyFirstAddress ? '1' : '0');
            t_option += (this.m_option.CheckCCLine ? '1' : '0');
            t_option += (this.m_option.CheckSameSuffix ? '1' : '0');
            t_option += (this.m_status ? '1' : '0');
            // 2010/9/18
            t_option += (this.m_option.CheckWord ? '1' : '0');
            t_option += (this.m_option.CheckExcel ? '1' : '0');
            t_option += (this.m_option.CheckPowerPoint ? '1' : '0');
            t_option += (this.m_option.CheckText ? '1' : '0');
            //            
            this.SetValue(Registry.CurrentUser, t_subkey, t_name, t_option);
        }

        protected override void OnBeforeSendEmail(DataDesign.MailGO.Model.IEmail v_email, out bool v_cancel)
        {
            this.m_mailgo.Track.Debug("ACTIVATION: Start checking email before sent ...");
            this.m_mailgo.Address.CheckEmail(v_email, out v_cancel);            
        }
    }
}
