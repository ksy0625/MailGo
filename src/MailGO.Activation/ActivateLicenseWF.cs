/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.23 Tran Dinh Thoai
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DataDesign.MailGO.Activation
{
    internal partial class ActivateLicenseWF : Form
    {
        private Model.IMailGoPG m_mailgo;
        private string m_license_id;
        private static Model.IEmail sm_request_email;

        public ActivateLicenseWF(Model.IMailGoPG v_mailgo, string v_license_id)
        {
            this.m_mailgo = v_mailgo;
            this.m_license_id = v_license_id;
            InitializeComponent();
        }

        private void ActivateLicenseWF_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            string t_request;
            this.m_mailgo.License.RequestActivationID(out t_request, this.m_license_id, this.m_mailgo.License.ReadHardware());
            this.txtActivationRequest.Text = t_request;
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            this.ctlOpenFileDialog.ShowDialog(this);

            if (this.ctlOpenFileDialog.FileName == "") return;

            string t_activation_id = File.ReadAllText(this.ctlOpenFileDialog.FileName);
            this.txtActivationID.Text = t_activation_id;
        }

        private void cmdRequest_Click(object sender, EventArgs e)
        {
            sm_request_email = new Model.MEmail();
            sm_request_email.Subject = this.lblTargetTitle.Text;
            sm_request_email.TO.Add(this.lblTargetEmail.Text);
            // 2011/10/10 Sekine
            sm_request_email.Body = this.txtActivationRequest.Text + " (3.0.3)";
            //sm_request_email.Body = this.txtActivationRequest.Text;
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        private void cmdActivate_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_mailgo.License.ActivateLicense(this.txtActivationID.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.m_mailgo.Track.Error(ex);
                this.ShowMessage(this.lblMsgInvalidActivation);
            }
        }

        private void ShowMessage(Label v_message)
        {
            MessageBox.Show(v_message.Text, this.lblMsgError.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ActivateLicense(Model.IMailGoPG v_mailgo, string v_license_id, out bool v_request, out bool v_activate, out Model.IEmail v_email)
        {
            v_request = false;
            v_activate = false;
            v_email = new Model.MEmail();

            v_mailgo.Track.Debug("Start showing activation license");

            ActivateLicenseWF t_form = new ActivateLicenseWF(v_mailgo, v_license_id);
            DialogResult t_result = t_form.ShowDialog();

            v_mailgo.Track.Debug("Dialog result : " + t_result.ToString());

            if (t_result == DialogResult.OK)
            {
                v_activate = true;
            }
            else if (t_result == DialogResult.Ignore)
            {
                v_request = true;
                v_email.TO.AddRange(sm_request_email.TO.ToArray());
                v_email.Subject = sm_request_email.Subject;
                v_email.Body = sm_request_email.Body;
            }

            v_mailgo.Track.Debug("Finish showing activation license");
        }
    }
}