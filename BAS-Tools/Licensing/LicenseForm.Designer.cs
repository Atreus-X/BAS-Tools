namespace MainApp.Licensing
{
    partial class LicenseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHardwareId = new System.Windows.Forms.Label();
            this.txtHardwareId = new System.Windows.Forms.TextBox();
            this.lblLicenseKey = new System.Windows.Forms.Label();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHardwareId
            // 
            this.lblHardwareId.AutoSize = true;
            this.lblHardwareId.Location = new System.Drawing.Point(12, 15);
            this.lblHardwareId.Name = "lblHardwareId";
            this.lblHardwareId.Size = new System.Drawing.Size(71, 13);
            this.lblHardwareId.TabIndex = 0;
            this.lblHardwareId.Text = "Hardware ID:";
            // 
            // txtHardwareId
            // 
            this.txtHardwareId.Location = new System.Drawing.Point(89, 12);
            this.txtHardwareId.Name = "txtHardwareId";
            this.txtHardwareId.ReadOnly = true;
            this.txtHardwareId.Size = new System.Drawing.Size(283, 20);
            this.txtHardwareId.TabIndex = 3;
            // 
            // lblLicenseKey
            // 
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Location = new System.Drawing.Point(12, 41);
            this.lblLicenseKey.Name = "lblLicenseKey";
            this.lblLicenseKey.Size = new System.Drawing.Size(68, 13);
            this.lblLicenseKey.TabIndex = 2;
            this.lblLicenseKey.Text = "License Key:";
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Location = new System.Drawing.Point(89, 38);
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.Size = new System.Drawing.Size(283, 20);
            this.txtLicenseKey.TabIndex = 0;
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(216, 73);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(75, 23);
            this.btnActivate.TabIndex = 1;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 73);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // LicenseForm
            // 
            this.AcceptButton = this.btnActivate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtLicenseKey);
            this.Controls.Add(this.lblLicenseKey);
            this.Controls.Add(this.txtHardwareId);
            this.Controls.Add(this.lblHardwareId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activate Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHardwareId;
        private System.Windows.Forms.TextBox txtHardwareId;
        private System.Windows.Forms.Label lblLicenseKey;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnCancel;
    }
}
