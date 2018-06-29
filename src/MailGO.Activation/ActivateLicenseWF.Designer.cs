namespace DataDesign.MailGO.Activation
{
    partial class ActivateLicenseWF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivateLicenseWF));
            this.pnlCover = new System.Windows.Forms.Panel();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMsgError = new System.Windows.Forms.Label();
            this.lblMsgInvalidActivation = new System.Windows.Forms.Label();
            this.lblTargetTitle = new System.Windows.Forms.Label();
            this.lblTargetEmail = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.cmdRequest = new System.Windows.Forms.Button();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.txtActivationID = new System.Windows.Forms.TextBox();
            this.lblActivationID = new System.Windows.Forms.Label();
            this.cmdActivate = new System.Windows.Forms.Button();
            this.txtActivationRequest = new System.Windows.Forms.TextBox();
            this.lblActivationRequest = new System.Windows.Forms.Label();
            this.ctlOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlCover.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCover
            // 
            this.pnlCover.Controls.Add(this.pnlMessage);
            this.pnlCover.Controls.Add(this.lblTargetTitle);
            this.pnlCover.Controls.Add(this.lblTargetEmail);
            this.pnlCover.Controls.Add(this.lblNote);
            this.pnlCover.Controls.Add(this.lblLine);
            this.pnlCover.Controls.Add(this.cmdRequest);
            this.pnlCover.Controls.Add(this.cmdOpen);
            this.pnlCover.Controls.Add(this.txtActivationID);
            this.pnlCover.Controls.Add(this.lblActivationID);
            this.pnlCover.Controls.Add(this.cmdActivate);
            this.pnlCover.Controls.Add(this.txtActivationRequest);
            this.pnlCover.Controls.Add(this.lblActivationRequest);
            resources.ApplyResources(this.pnlCover, "pnlCover");
            this.pnlCover.Name = "pnlCover";
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.lblMsgError);
            this.pnlMessage.Controls.Add(this.lblMsgInvalidActivation);
            resources.ApplyResources(this.pnlMessage, "pnlMessage");
            this.pnlMessage.Name = "pnlMessage";
            // 
            // lblMsgError
            // 
            resources.ApplyResources(this.lblMsgError, "lblMsgError");
            this.lblMsgError.Name = "lblMsgError";
            // 
            // lblMsgInvalidActivation
            // 
            resources.ApplyResources(this.lblMsgInvalidActivation, "lblMsgInvalidActivation");
            this.lblMsgInvalidActivation.Name = "lblMsgInvalidActivation";
            // 
            // lblTargetTitle
            // 
            resources.ApplyResources(this.lblTargetTitle, "lblTargetTitle");
            this.lblTargetTitle.Name = "lblTargetTitle";
            // 
            // lblTargetEmail
            // 
            resources.ApplyResources(this.lblTargetEmail, "lblTargetEmail");
            this.lblTargetEmail.Name = "lblTargetEmail";
            // 
            // lblNote
            // 
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.Name = "lblNote";
            // 
            // lblLine
            // 
            resources.ApplyResources(this.lblLine, "lblLine");
            this.lblLine.BackColor = System.Drawing.SystemColors.Control;
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine.Name = "lblLine";
            // 
            // cmdRequest
            // 
            resources.ApplyResources(this.cmdRequest, "cmdRequest");
            this.cmdRequest.Name = "cmdRequest";
            this.cmdRequest.UseVisualStyleBackColor = true;
            this.cmdRequest.Click += new System.EventHandler(this.cmdRequest_Click);
            // 
            // cmdOpen
            // 
            resources.ApplyResources(this.cmdOpen, "cmdOpen");
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // txtActivationID
            // 
            this.txtActivationID.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtActivationID, "txtActivationID");
            this.txtActivationID.Name = "txtActivationID";
            // 
            // lblActivationID
            // 
            resources.ApplyResources(this.lblActivationID, "lblActivationID");
            this.lblActivationID.Name = "lblActivationID";
            // 
            // cmdActivate
            // 
            resources.ApplyResources(this.cmdActivate, "cmdActivate");
            this.cmdActivate.Name = "cmdActivate";
            this.cmdActivate.UseVisualStyleBackColor = true;
            this.cmdActivate.Click += new System.EventHandler(this.cmdActivate_Click);
            // 
            // txtActivationRequest
            // 
            this.txtActivationRequest.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtActivationRequest, "txtActivationRequest");
            this.txtActivationRequest.Name = "txtActivationRequest";
            this.txtActivationRequest.ReadOnly = true;
            // 
            // lblActivationRequest
            // 
            resources.ApplyResources(this.lblActivationRequest, "lblActivationRequest");
            this.lblActivationRequest.Name = "lblActivationRequest";
            // 
            // ctlOpenFileDialog
            // 
            resources.ApplyResources(this.ctlOpenFileDialog, "ctlOpenFileDialog");
            // 
            // ActivateLicenseWF
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivateLicenseWF";
            this.Load += new System.EventHandler(this.ActivateLicenseWF_Load);
            this.pnlCover.ResumeLayout(false);
            this.pnlCover.PerformLayout();
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCover;
        private System.Windows.Forms.TextBox txtActivationRequest;
        private System.Windows.Forms.Label lblActivationRequest;
        private System.Windows.Forms.TextBox txtActivationID;
        private System.Windows.Forms.Label lblActivationID;
        private System.Windows.Forms.Button cmdActivate;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.Button cmdRequest;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.OpenFileDialog ctlOpenFileDialog;
        private System.Windows.Forms.Label lblTargetTitle;
        private System.Windows.Forms.Label lblTargetEmail;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Label lblMsgError;
        private System.Windows.Forms.Label lblMsgInvalidActivation;
    }
}