using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DataDesign.MailGO.Unity.Setup
{
    internal partial class LicenseWF : Form
    {
        private Model.IMailGoPG m_mailgo;
        private string m_evaluation_license;

        public LicenseWF(Model.IMailGoPG v_mailgo)
        {
            this.m_mailgo = v_mailgo;
            this.m_evaluation_license = this.m_mailgo.License.GenerateLicenseID(true, 30);
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_mailgo.License.InstallLicense(this.txtLicenseID.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                this.m_mailgo.Track.Error(ex);
                this.ShowMessage(this.lblMsgInvalidLicense);
            }
        }

        private void ShowMessage(Label v_message)
        {
            MessageBox.Show(v_message.Text, this.lblMsgError.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LicenseWF_Load(object sender, EventArgs e)
        {
            bool t_installed;
            bool t_evaluated;
            bool t_activated;
            bool t_expired;
            DateTime? t_expired_date;
            string t_license_id;

            this.m_mailgo.License.CheckLicense(out t_installed, out t_evaluated, out t_activated, out t_expired, out t_expired_date, out t_license_id);

            if (t_installed && !t_activated && t_expired && t_evaluated)
            {
                this.chkEvaluation.Enabled = false;
                this.chkEvaluation.Checked = false;
                this.txtLicenseID.Text = "";
                return;
            }
            this.txtLicenseID.Text = this.m_evaluation_license;

            this.tmrActivate.Enabled = true;
        }

        private void chkEvaluation_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEvaluation.Checked)
            {
                this.txtLicenseID.Enabled = false;
                this.cmdOpen.Enabled = false;
                this.txtLicenseID.Text = this.m_evaluation_license;
            }
            else
            {
                this.txtLicenseID.Enabled = true;
                this.cmdOpen.Enabled = true;
                this.txtLicenseID.Text = "";
            }
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            this.ctlOpenFileDialog.ShowDialog(this);

            if (this.ctlOpenFileDialog.FileName == "") return;

            string t_license_id = File.ReadAllText(this.ctlOpenFileDialog.FileName);
            this.txtLicenseID.Text = t_license_id;
        }

        private void tmrActivate_Tick(object sender, EventArgs e)
        {
            this.tmrActivate.Enabled = false;
            this.Activate();
            this.BringToFront();
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }
    }
}