namespace MainApp
{
    partial class ManualReadWriteForm
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
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.cmbObjectType = new System.Windows.Forms.ComboBox();
            this.lblInstance = new System.Windows.Forms.Label();
            this.txtInstance = new System.Windows.Forms.TextBox();
            this.lblProperty = new System.Windows.Forms.Label();
            this.cmbProperty = new System.Windows.Forms.ComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.lblManualInput = new System.Windows.Forms.Label();
            this.txtManualInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(110, 12);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(262, 21);
            this.cmbDevices.TabIndex = 0;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(12, 15);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 1;
            this.lblDevice.Text = "Device";
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Location = new System.Drawing.Point(12, 42);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(65, 13);
            this.lblObjectType.TabIndex = 3;
            this.lblObjectType.Text = "Object Type";
            // 
            // cmbObjectType
            // 
            this.cmbObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjectType.FormattingEnabled = true;
            this.cmbObjectType.Location = new System.Drawing.Point(110, 39);
            this.cmbObjectType.Name = "cmbObjectType";
            this.cmbObjectType.Size = new System.Drawing.Size(181, 21);
            this.cmbObjectType.TabIndex = 2;
            // 
            // lblInstance
            // 
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(12, 69);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(48, 13);
            this.lblInstance.TabIndex = 4;
            this.lblInstance.Text = "Instance";
            // 
            // txtInstance
            // 
            this.txtInstance.Location = new System.Drawing.Point(110, 66);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new System.Drawing.Size(262, 20);
            this.txtInstance.TabIndex = 5;
            // 
            // lblProperty
            // 
            this.lblProperty.AutoSize = true;
            this.lblProperty.Location = new System.Drawing.Point(12, 95);
            this.lblProperty.Name = "lblProperty";
            this.lblProperty.Size = new System.Drawing.Size(46, 13);
            this.lblProperty.TabIndex = 7;
            this.lblProperty.Text = "Property";
            // 
            // cmbProperty
            // 
            this.cmbProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProperty.FormattingEnabled = true;
            this.cmbProperty.Location = new System.Drawing.Point(110, 92);
            this.cmbProperty.Name = "cmbProperty";
            this.cmbProperty.Size = new System.Drawing.Size(262, 21);
            this.cmbProperty.TabIndex = 6;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(12, 122);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 8;
            this.lblValue.Text = "Value";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(110, 119);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(262, 20);
            this.txtValue.TabIndex = 9;
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(12, 148);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(38, 13);
            this.lblPriority.TabIndex = 11;
            this.lblPriority.Text = "Priority";
            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(110, 145);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(262, 21);
            this.cmbPriority.TabIndex = 10;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(110, 181);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 12;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(191, 181);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 13;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(297, 38);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(75, 23);
            this.btnManual.TabIndex = 14;
            this.btnManual.Text = "Manual";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // lblManualInput
            // 
            this.lblManualInput.AutoSize = true;
            this.lblManualInput.Location = new System.Drawing.Point(12, 69);
            this.lblManualInput.Name = "lblManualInput";
            this.lblManualInput.Size = new System.Drawing.Size(92, 13);
            this.lblManualInput.TabIndex = 15;
            this.lblManualInput.Text = "obj;inst;prop";
            this.lblManualInput.Visible = false;
            // 
            // txtManualInput
            // 
            this.txtManualInput.Location = new System.Drawing.Point(110, 66);
            this.txtManualInput.Name = "txtManualInput";
            this.txtManualInput.Size = new System.Drawing.Size(262, 20);
            this.txtManualInput.TabIndex = 16;
            this.txtManualInput.Visible = false;
            // 
            // ManualReadWriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 216);
            this.Controls.Add(this.txtManualInput);
            this.Controls.Add(this.lblManualInput);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblProperty);
            this.Controls.Add(this.cmbProperty);
            this.Controls.Add(this.txtInstance);
            this.Controls.Add(this.lblInstance);
            this.Controls.Add(this.lblObjectType);
            this.Controls.Add(this.cmbObjectType);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.cmbDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualReadWriteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manual Read/Write";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblObjectType;
        private System.Windows.Forms.ComboBox cmbObjectType;
        private System.Windows.Forms.Label lblInstance;
        private System.Windows.Forms.TextBox txtInstance;
        private System.Windows.Forms.Label lblProperty;
        private System.Windows.Forms.ComboBox cmbProperty;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Label lblManualInput;
        private System.Windows.Forms.TextBox txtManualInput;
    }
}