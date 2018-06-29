namespace DataDesign.MailGO.Activation
{
    partial class NewLicenseWF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewLicenseWF));
            this.pnlCover = new System.Windows.Forms.Panel();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMsgError = new System.Windows.Forms.Label();
            this.lblMsgInvalidLicense = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.ctlOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlCover.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCover
            // 
            this.pnlCover.AccessibleDescription = null;
            this.pnlCover.AccessibleName = null;
            resources.ApplyResources(this.pnlCover, "pnlCover");
            this.pnlCover.BackgroundImage = null;
            this.pnlCover.Controls.Add(this.pnlMessage);
            this.pnlCover.Controls.Add(this.cmdCancel);
            this.pnlCover.Controls.Add(this.cmdOK);
            this.pnlCover.Controls.Add(this.cmdOpen);
            this.pnlCover.Controls.Add(this.txtLicense);
            this.pnlCover.Controls.Add(this.lblMessage);
            this.pnlCover.Font = null;
            this.pnlCover.Name = "pnlCover";
            // 
            // pnlMessage
            // 
            this.pnlMessage.AccessibleDescription = null;
            this.pnlMessage.AccessibleName = null;
            resources.ApplyResources(this.pnlMessage, "pnlMessage");
            this.pnlMessage.BackgroundImage = null;
            this.pnlMessage.Controls.Add(this.lblMsgError);
            this.pnlMessage.Controls.Add(this.lblMsgInvalidLicense);
            this.pnlMessage.Font = null;
            this.pnlMessage.Name = "pnlMessage";
            // 
            // lblMsgError
            // 
            this.lblMsgError.AccessibleDescription = null;
            this.lblMsgError.AccessibleName = null;
            resources.ApplyResources(this.lblMsgError, "lblMsgError");
            this.lblMsgError.Font = null;
            this.lblMsgError.Name = "lblMsgError";
            // 
            // lblMsgInvalidLicense
            // 
            this.lblMsgInvalidLicense.AccessibleDescription = null;
            this.lblMsgInvalidLicense.AccessibleName = null;
            resources.ApplyResources(this.lblMsgInvalidLicense, "lblMsgInvalidLicense");
            this.lblMsgInvalidLicense.Font = null;
            this.lblMsgInvalidLicense.Name = "lblMsgInvalidLicense";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AccessibleDescription = null;
            this.cmdCancel.AccessibleName = null;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.BackgroundImage = null;
            this.cmdCancel.Font = null;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.AccessibleDescription = null;
            this.cmdOK.AccessibleName = null;
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.BackgroundImage = null;
            this.cmdOK.Font = null;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdOpen
            // 
            this.cmdOpen.AccessibleDescription = null;
            this.cmdOpen.AccessibleName = null;
            resources.ApplyResources(this.cmdOpen, "cmdOpen");
            this.cmdOpen.BackgroundImage = null;
            this.cmdOpen.Font = null;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // txtLicense
            // 
            this.txtLicense.AccessibleDescription = null;
            this.txtLicense.AccessibleName = null;
            resources.ApplyResources(this.txtLicense, "txtLicense");
            this.txtLicense.BackgroundImage = null;
            this.txtLicense.Font = null;
            this.txtLicense.Name = "txtLicense";
            // 
            // lblMessage
            // 
            this.lblMessage.AccessibleDescription = null;
            this.lblMessage.AccessibleName = null;
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Font = null;
            this.lblMessage.Name = "lblMessage";
            // 
            // ctlOpenFileDialog
            // 
            resources.ApplyResources(this.ctlOpenFileDialog, "ctlOpenFileDialog");
            // 
            // NewLicenseWF
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.pnlCover);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLicenseWF";
            this.Load += new System.EventHandler(this.NewLicenseWF_Load);
            this.pnlCover.ResumeLayout(false);
            this.pnlCover.PerformLayout();
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCover;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.OpenFileDialog ctlOpenFileDialog;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Label lblMsgError;
        private System.Windows.Forms.Label lblMsgInvalidLicense;
    }
}