namespace DataDesign.MailGO.Activation
{
    partial class ConfirmRequestWF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmRequestWF));
            this.cmdYes = new System.Windows.Forms.Button();
            this.cmdNo = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdYes
            // 
            this.cmdYes.AccessibleDescription = null;
            this.cmdYes.AccessibleName = null;
            resources.ApplyResources(this.cmdYes, "cmdYes");
            this.cmdYes.BackgroundImage = null;
            this.cmdYes.Font = null;
            this.cmdYes.Name = "cmdYes";
            this.cmdYes.UseVisualStyleBackColor = true;
            this.cmdYes.Click += new System.EventHandler(this.cmdYes_Click);
            // 
            // cmdNo
            // 
            this.cmdNo.AccessibleDescription = null;
            this.cmdNo.AccessibleName = null;
            resources.ApplyResources(this.cmdNo, "cmdNo");
            this.cmdNo.BackgroundImage = null;
            this.cmdNo.Font = null;
            this.cmdNo.Name = "cmdNo";
            this.cmdNo.UseVisualStyleBackColor = true;
            this.cmdNo.Click += new System.EventHandler(this.cmdNo_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AccessibleDescription = null;
            this.lblMessage.AccessibleName = null;
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Font = null;
            this.lblMessage.Name = "lblMessage";
            // 
            // ConfirmRequestWF
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.cmdNo);
            this.Controls.Add(this.cmdYes);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmRequestWF";
            this.Load += new System.EventHandler(this.ConfirmRequestWF_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdYes;
        private System.Windows.Forms.Button cmdNo;
        private System.Windows.Forms.Label lblMessage;
    }
}