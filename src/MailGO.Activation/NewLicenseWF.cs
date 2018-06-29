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
    internal partial class NewLicenseWF : Form
    {
        private Model.IMailGoPG m_mailgo;

        public NewLicenseWF(Model.IMailGoPG v_mailgo)
        {
            this.m_mailgo = v_mailgo;
            InitializeComponent();
        }

        private void NewLicenseWF_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_mailgo.License.InstallLicense(this.txtLicense.Text);
                this.DialogResult = DialogResult.OK;
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            this.ctlOpenFileDialog.ShowDialog(this);

            if (this.ctlOpenFileDialog.FileName == "") return;

            string t_license_id = File.ReadAllText(this.ctlOpenFileDialog.FileName);
            this.txtLicense.Text = t_license_id;
        }
    }
}