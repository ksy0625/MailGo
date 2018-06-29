namespace DataDesign.MailGO.Unity.Setup
{
    partial class LicenseWF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseWF));
            this.pnlCover = new System.Windows.Forms.Panel();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMsgError = new System.Windows.Forms.Label();
            this.lblMsgInvalidLicense = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.chkEvaluation = new System.Windows.Forms.CheckBox();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.ctlOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tmrActivate = new System.Windows.Forms.Timer(this.components);
            this.pnlCover.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCover
            // 
            this.pnlCover.Controls.Add(this.pnlMessage);
            this.pnlCover.Controls.Add(this.cmdOK);
            this.pnlCover.Controls.Add(this.chkEvaluation);
            this.pnlCover.Controls.Add(this.cmdOpen);
            this.pnlCover.Controls.Add(this.txtLicenseID);
            this.pnlCover.Controls.Add(this.lblLicenseID);
            resources.ApplyResources(this.pnlCover, "pnlCover");
            this.pnlCover.Name = "pnlCover";
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.lblMsgError);
            this.pnlMessage.Controls.Add(this.lblMsgInvalidLicense);
            resources.ApplyResources(this.pnlMessage, "pnlMessage");
            this.pnlMessage.Name = "pnlMessage";
            // 
            // lblMsgError
            // 
            resources.ApplyResources(this.lblMsgError, "lblMsgError");
            this.lblMsgError.Name = "lblMsgError";
            // 
            // lblMsgInvalidLicense
            // 
            resources.ApplyResources(this.lblMsgInvalidLicense, "lblMsgInvalidLicense");
            this.lblMsgInvalidLicense.Name = "lblMsgInvalidLicense";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // chkEvaluation
            // 
            resources.ApplyResources(this.chkEvaluation, "chkEvaluation");
            this.chkEvaluation.Checked = true;
            this.chkEvaluation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEvaluation.Name = "chkEvaluation";
            this.chkEvaluation.UseVisualStyleBackColor = true;
            this.chkEvaluation.CheckedChanged += new System.EventHandler(this.chkEvaluation_CheckedChanged);
            // 
            // cmdOpen
            // 
            resources.ApplyResources(this.cmdOpen, "cmdOpen");
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtLicenseID, "txtLicenseID");
            this.txtLicenseID.Name = "txtLicenseID";
            // 
            // lblLicenseID
            // 
            resources.ApplyResources(this.lblLicenseID, "lblLicenseID");
            this.lblLicenseID.Name = "lblLicenseID";
            // 
            // ctlOpenFileDialog
            // 
            resources.ApplyResources(this.ctlOpenFileDialog, "ctlOpenFileDialog");
            // 
            // tmrActivate
            // 
            this.tmrActivate.Tick += new System.EventHandler(this.tmrActivate_Tick);
            // 
            // LicenseWF
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseWF";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LicenseWF_Load);
            this.pnlCover.ResumeLayout(false);
            this.pnlCover.PerformLayout();
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCover;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.CheckBox chkEvaluation;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.OpenFileDialog ctlOpenFileDialog;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Label lblMsgInvalidLicense;
        private System.Windows.Forms.Label lblMsgError;
        private System.Windows.Forms.Timer tmrActivate;
    }
}