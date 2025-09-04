namespace BAS_Tools
{
    partial class AboutForm
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
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelContact = new System.Windows.Forms.Label();
            this.linkLabelContact = new System.Windows.Forms.LinkLabel();
            this.textBoxLicense = new System.Windows.Forms.TextBox();
            this.labelSpecialThanks = new System.Windows.Forms.Label();
            this.textBoxSpecialThanks = new System.Windows.Forms.TextBox();
            this.textBoxDisclaimer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.Location = new System.Drawing.Point(12, 9);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(94, 20);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "BAS Tools";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(13, 33);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(60, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Version 1.0";
            // 
            // labelContact
            // 
            this.labelContact.AutoSize = true;
            this.labelContact.Location = new System.Drawing.Point(13, 59);
            this.labelContact.Name = "labelContact";
            this.labelContact.Size = new System.Drawing.Size(47, 13);
            this.labelContact.TabIndex = 2;
            this.labelContact.Text = "Contact:";
            // 
            // linkLabelContact
            // 
            this.linkLabelContact.AutoSize = true;
            this.linkLabelContact.Location = new System.Drawing.Point(67, 59);
            this.linkLabelContact.Name = "linkLabelContact";
            this.linkLabelContact.Size = new System.Drawing.Size(123, 13);
            this.linkLabelContact.TabIndex = 3;
            this.linkLabelContact.TabStop = true;
            this.linkLabelContact.Text = "support@example.com";
            // 
            // textBoxLicense
            // 
            this.textBoxLicense.Location = new System.Drawing.Point(16, 88);
            this.textBoxLicense.Multiline = true;
            this.textBoxLicense.Name = "textBoxLicense";
            this.textBoxLicense.ReadOnly = true;
            this.textBoxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLicense.Size = new System.Drawing.Size(356, 100);
            this.textBoxLicense.TabIndex = 4;
            this.textBoxLicense.Text = "GNU GENERAL PUBLIC LICENSE\r\nVersion 3, 29 June 2007\r\n\r\nCopyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>\r\nEveryone is permitted to copy and distribute verbatim copies of this license document, but changing it is not allowed.\r\n\r\nThis program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.\r\n\r\nThis program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.\r\n\r\nYou should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.";
            // 
            // labelSpecialThanks
            // 
            this.labelSpecialThanks.AutoSize = true;
            this.labelSpecialThanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpecialThanks.Location = new System.Drawing.Point(13, 202);
            this.labelSpecialThanks.Name = "labelSpecialThanks";
            this.labelSpecialThanks.Size = new System.Drawing.Size(95, 13);
            this.labelSpecialThanks.TabIndex = 5;
            this.labelSpecialThanks.Text = "Special Thanks";
            // 
            // textBoxSpecialThanks
            // 
            this.textBoxSpecialThanks.Location = new System.Drawing.Point(16, 218);
            this.textBoxSpecialThanks.Multiline = true;
            this.textBoxSpecialThanks.Name = "textBoxSpecialThanks";
            this.textBoxSpecialThanks.ReadOnly = true;
            this.textBoxSpecialThanks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSpecialThanks.Size = new System.Drawing.Size(356, 100);
            this.textBoxSpecialThanks.TabIndex = 6;
            this.textBoxSpecialThanks.Text = "FluentModbus library by @h-qust\r\nc-sharp-stack by Morten Kvistgaard";
            // 
            // textBoxDisclaimer
            // 
            this.textBoxDisclaimer.Location = new System.Drawing.Point(16, 324);
            this.textBoxDisclaimer.Multiline = true;
            this.textBoxDisclaimer.Name = "textBoxDisclaimer";
            this.textBoxDisclaimer.ReadOnly = true;
            this.textBoxDisclaimer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDisclaimer.Size = new System.Drawing.Size(356, 100);
            this.textBoxDisclaimer.TabIndex = 7;
            this.textBoxDisclaimer.Text = "This software is provided \'as-is\', without any express or implied warranty. In no event will the authors be held liable for any damages arising from the use of this software.";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 436);
            this.Controls.Add(this.textBoxDisclaimer);
            this.Controls.Add(this.textBoxSpecialThanks);
            this.Controls.Add(this.labelSpecialThanks);
            this.Controls.Add(this.textBoxLicense);
            this.Controls.Add(this.linkLabelContact);
            this.Controls.Add(this.labelContact);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelAppName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About BAS Tools";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelContact;
        private System.Windows.Forms.LinkLabel linkLabelContact;
        private System.Windows.Forms.TextBox textBoxLicense;
        private System.Windows.Forms.Label labelSpecialThanks;
        private System.Windows.Forms.TextBox textBoxSpecialThanks;
        private System.Windows.Forms.TextBox textBoxDisclaimer;
    }
}

