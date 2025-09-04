namespace BAS_Tools.Licensing
{
    partial class LicenseForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.hardwareIdTextBox = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.licenseKeyTextBox = new System.Windows.Forms.TextBox();
            this.activateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hardware ID:";
            // 
            // hardwareIdTextBox
            // 
            this.hardwareIdTextBox.Location = new System.Drawing.Point(88, 12);
            this.hardwareIdTextBox.Name = "hardwareIdTextBox";
            this.hardwareIdTextBox.ReadOnly = true;
            this.hardwareIdTextBox.Size = new System.Drawing.Size(284, 20);
            this.hardwareIdTextBox.TabIndex = 1;
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(378, 10);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(75, 23);
            this.copyButton.TabIndex = 2;
            this.copyButton.Text = "Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "License Key:";
            // 
            // licenseKeyTextBox
            // 
            this.licenseKeyTextBox.Location = new System.Drawing.Point(88, 41);
            this.licenseKeyTextBox.Name = "licenseKeyTextBox";
            this.licenseKeyTextBox.Size = new System.Drawing.Size(284, 20);
            this.licenseKeyTextBox.TabIndex = 4;
            // 
            // activateButton
            // 
            this.activateButton.Location = new System.Drawing.Point(378, 39);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(75, 23);
            this.activateButton.TabIndex = 5;
            this.activateButton.Text = "Activate";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 76);
            this.Controls.Add(this.activateButton);
            this.Controls.Add(this.licenseKeyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.hardwareIdTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Software Activation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hardwareIdTextBox;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox licenseKeyTextBox;
        private System.Windows.Forms.Button activateButton;
    }
}

