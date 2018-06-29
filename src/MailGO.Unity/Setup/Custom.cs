/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.24 Tran Dinh Thoai
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security.AccessControl;
using Microsoft.Win32;
using System.Reflection;
using System.IO;

namespace DataDesign.MailGO.Unity.Setup
{
    [RunInstaller(true)]
    public class Custom : Installer
    {   
        private Model.IMailGoPG m_mailgo = new MailGoPG();

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);

            this.SetPermission();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ja-JP");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");

            // A TRICKY WAY TO MAKE THE LICENSE WINDOW ON TOP - A WORKAROUND WAY APPLYING IN WINDOWS VISTA
            // Create a host form that is a TopMost window which will be the 
            // parent of the LicenseWF window.
            Form topmostForm = new Form();
            // We do not want anyone to see this window so position it off the 
            // visible screen and make it as small as possible
            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10,
                rect.Right + 10);
            topmostForm.Show();
            // Make this form the active form and make it TopMost
            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;

            LicenseWF t_form = new LicenseWF(this.m_mailgo);            
            t_form.ShowDialog(topmostForm);

            topmostForm.Dispose();

            this.m_mailgo.License.CheckCDO();
        }

        public override void Rollback(System.Collections.IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        private void SetPermission()
        {
            string t_subkey = @"SOFTWARE\Kodai";
            RegistryKey t_key = Registry.CurrentUser.CreateSubKey(t_subkey);
            RegistrySecurity t_regsec = t_key.GetAccessControl();

            t_regsec.AddAccessRule(new RegistryAccessRule("Everyone", RegistryRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
            t_key.SetAccessControl(t_regsec);

            string t_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectorySecurity t_dirsec = Directory.GetAccessControl(t_path);
            t_dirsec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(t_path, t_dirsec);
        }

        private class MailGoPG : Model.MMailGoPG
        {
            private const string c_lic_license = "aoekghoakwhgodkahgoekhgaowighke1234dfhe@343tdogh&68*964546dhgoqdkgha";

            protected override void OnCreateLicense()
            {
                License.Share.License = c_lic_license;
                this.m_license = License.Share.CreatePG(this);
            }
        }
    }
}
