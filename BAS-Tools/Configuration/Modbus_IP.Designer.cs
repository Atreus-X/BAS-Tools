namespace MainApp.Configuration
{
    partial class Modbus_IP
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.actionsFrame = new System.Windows.Forms.GroupBox();
            this.actionsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelStartAddress = new System.Windows.Forms.Label();
            this.startAddressTextBox = new System.Windows.Forms.TextBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.labelWriteValue = new System.Windows.Forms.Label();
            this.writeValueTextBox = new System.Windows.Forms.TextBox();
            this.readCoilsButton = new System.Windows.Forms.Button();
            this.readDiscreteInputsButton = new System.Windows.Forms.Button();
            this.readHoldingRegistersButton = new System.Windows.Forms.Button();
            this.readInputRegistersButton = new System.Windows.Forms.Button();
            this.writeSingleCoilButton = new System.Windows.Forms.Button();
            this.writeSingleRegisterButton = new System.Windows.Forms.Button();
            this.writeMultipleCoilsButton = new System.Windows.Forms.Button();
            this.writeMultipleRegistersButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.outputFrame = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.configFrame = new System.Windows.Forms.GroupBox();
            this.configLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.ipAddressComboBox = new System.Windows.Forms.ComboBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.labelUnitId = new System.Windows.Forms.Label();
            this.unitIdComboBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.actionsFrame.SuspendLayout();
            this.actionsLayout.SuspendLayout();
            this.outputFrame.SuspendLayout();
            this.configFrame.SuspendLayout();
            this.configLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.mainSplitContainer);
            this.mainPanel.Controls.Add(this.configFrame);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(8);
            this.mainPanel.Size = new System.Drawing.Size(834, 969);
            this.mainPanel.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(8, 89);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.actionsFrame);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.mainSplitContainer.Size = new System.Drawing.Size(818, 872);
            this.mainSplitContainer.SplitterDistance = 470;
            this.mainSplitContainer.SplitterWidth = 3;
            this.mainSplitContainer.TabIndex = 1;
            // 
            // actionsFrame
            // 
            this.actionsFrame.Controls.Add(this.actionsLayout);
            this.actionsFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsFrame.Location = new System.Drawing.Point(0, 0);
            this.actionsFrame.Margin = new System.Windows.Forms.Padding(2);
            this.actionsFrame.Name = "actionsFrame";
            this.actionsFrame.Padding = new System.Windows.Forms.Padding(8);
            this.actionsFrame.Size = new System.Drawing.Size(470, 872);
            this.actionsFrame.TabIndex = 0;
            this.actionsFrame.TabStop = false;
            this.actionsFrame.Text = "Modbus Actions";
            // 
            // actionsLayout
            // 
            this.actionsLayout.ColumnCount = 2;
            this.actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.actionsLayout.Controls.Add(this.labelStartAddress, 0, 0);
            this.actionsLayout.Controls.Add(this.startAddressTextBox, 1, 0);
            this.actionsLayout.Controls.Add(this.labelQuantity, 0, 1);
            this.actionsLayout.Controls.Add(this.quantityTextBox, 1, 1);
            this.actionsLayout.Controls.Add(this.labelWriteValue, 0, 2);
            this.actionsLayout.Controls.Add(this.writeValueTextBox, 1, 2);
            this.actionsLayout.Controls.Add(this.readCoilsButton, 0, 3);
            this.actionsLayout.Controls.Add(this.readDiscreteInputsButton, 1, 3);
            this.actionsLayout.Controls.Add(this.readHoldingRegistersButton, 0, 4);
            this.actionsLayout.Controls.Add(this.readInputRegistersButton, 1, 4);
            this.actionsLayout.Controls.Add(this.writeSingleCoilButton, 0, 5);
            this.actionsLayout.Controls.Add(this.writeSingleRegisterButton, 1, 5);
            this.actionsLayout.Controls.Add(this.writeMultipleCoilsButton, 0, 6);
            this.actionsLayout.Controls.Add(this.writeMultipleRegistersButton, 1, 6);
            this.actionsLayout.Controls.Add(this.clearLogButton, 0, 7);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(8, 21);
            this.actionsLayout.Margin = new System.Windows.Forms.Padding(2);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.RowCount = 8;
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.actionsLayout.Size = new System.Drawing.Size(454, 843);
            this.actionsLayout.TabIndex = 0;
            // 
            // labelStartAddress
            // 
            this.labelStartAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelStartAddress.AutoSize = true;
            this.labelStartAddress.Location = new System.Drawing.Point(2, 5);
            this.labelStartAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStartAddress.Name = "labelStartAddress";
            this.labelStartAddress.Size = new System.Drawing.Size(73, 13);
            this.labelStartAddress.TabIndex = 0;
            this.labelStartAddress.Text = "Start Address:";
            // 
            // startAddressTextBox
            // 
            this.startAddressTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startAddressTextBox.Location = new System.Drawing.Point(229, 2);
            this.startAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.startAddressTextBox.Name = "startAddressTextBox";
            this.startAddressTextBox.Size = new System.Drawing.Size(223, 20);
            this.startAddressTextBox.TabIndex = 1;
            // 
            // labelQuantity
            // 
            this.labelQuantity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(2, 29);
            this.labelQuantity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(49, 13);
            this.labelQuantity.TabIndex = 2;
            this.labelQuantity.Text = "Quantity:";
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quantityTextBox.Location = new System.Drawing.Point(229, 26);
            this.quantityTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(223, 20);
            this.quantityTextBox.TabIndex = 3;
            // 
            // labelWriteValue
            // 
            this.labelWriteValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelWriteValue.AutoSize = true;
            this.labelWriteValue.Location = new System.Drawing.Point(2, 53);
            this.labelWriteValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWriteValue.Name = "labelWriteValue";
            this.labelWriteValue.Size = new System.Drawing.Size(76, 13);
            this.labelWriteValue.TabIndex = 4;
            this.labelWriteValue.Text = "Write Value(s):";
            // 
            // writeValueTextBox
            // 
            this.writeValueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeValueTextBox.Location = new System.Drawing.Point(229, 50);
            this.writeValueTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.writeValueTextBox.Name = "writeValueTextBox";
            this.writeValueTextBox.Size = new System.Drawing.Size(223, 20);
            this.writeValueTextBox.TabIndex = 5;
            // 
            // readCoilsButton
            // 
            this.readCoilsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readCoilsButton.Location = new System.Drawing.Point(2, 74);
            this.readCoilsButton.Margin = new System.Windows.Forms.Padding(2);
            this.readCoilsButton.Name = "readCoilsButton";
            this.readCoilsButton.Size = new System.Drawing.Size(223, 182);
            this.readCoilsButton.TabIndex = 6;
            this.readCoilsButton.Text = "Read Coils";
            this.readCoilsButton.UseVisualStyleBackColor = true;
            // 
            // readDiscreteInputsButton
            // 
            this.readDiscreteInputsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readDiscreteInputsButton.Location = new System.Drawing.Point(229, 74);
            this.readDiscreteInputsButton.Margin = new System.Windows.Forms.Padding(2);
            this.readDiscreteInputsButton.Name = "readDiscreteInputsButton";
            this.readDiscreteInputsButton.Size = new System.Drawing.Size(223, 182);
            this.readDiscreteInputsButton.TabIndex = 7;
            this.readDiscreteInputsButton.Text = "Read Discrete Inputs";
            this.readDiscreteInputsButton.UseVisualStyleBackColor = true;
            // 
            // readHoldingRegistersButton
            // 
            this.readHoldingRegistersButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readHoldingRegistersButton.Location = new System.Drawing.Point(2, 260);
            this.readHoldingRegistersButton.Margin = new System.Windows.Forms.Padding(2);
            this.readHoldingRegistersButton.Name = "readHoldingRegistersButton";
            this.readHoldingRegistersButton.Size = new System.Drawing.Size(223, 182);
            this.readHoldingRegistersButton.TabIndex = 8;
            this.readHoldingRegistersButton.Text = "Read Holding Registers";
            this.readHoldingRegistersButton.UseVisualStyleBackColor = true;
            // 
            // readInputRegistersButton
            // 
            this.readInputRegistersButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readInputRegistersButton.Location = new System.Drawing.Point(229, 260);
            this.readInputRegistersButton.Margin = new System.Windows.Forms.Padding(2);
            this.readInputRegistersButton.Name = "readInputRegistersButton";
            this.readInputRegistersButton.Size = new System.Drawing.Size(223, 182);
            this.readInputRegistersButton.TabIndex = 9;
            this.readInputRegistersButton.Text = "Read Input Registers";
            this.readInputRegistersButton.UseVisualStyleBackColor = true;
            // 
            // writeSingleCoilButton
            // 
            this.writeSingleCoilButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeSingleCoilButton.Location = new System.Drawing.Point(2, 446);
            this.writeSingleCoilButton.Margin = new System.Windows.Forms.Padding(2);
            this.writeSingleCoilButton.Name = "writeSingleCoilButton";
            this.writeSingleCoilButton.Size = new System.Drawing.Size(223, 182);
            this.writeSingleCoilButton.TabIndex = 10;
            this.writeSingleCoilButton.Text = "Write Single Coil";
            this.writeSingleCoilButton.UseVisualStyleBackColor = true;
            // 
            // writeSingleRegisterButton
            // 
            this.writeSingleRegisterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeSingleRegisterButton.Location = new System.Drawing.Point(229, 446);
            this.writeSingleRegisterButton.Margin = new System.Windows.Forms.Padding(2);
            this.writeSingleRegisterButton.Name = "writeSingleRegisterButton";
            this.writeSingleRegisterButton.Size = new System.Drawing.Size(223, 182);
            this.writeSingleRegisterButton.TabIndex = 11;
            this.writeSingleRegisterButton.Text = "Write Single Register";
            this.writeSingleRegisterButton.UseVisualStyleBackColor = true;
            // 
            // writeMultipleCoilsButton
            // 
            this.writeMultipleCoilsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeMultipleCoilsButton.Location = new System.Drawing.Point(2, 632);
            this.writeMultipleCoilsButton.Margin = new System.Windows.Forms.Padding(2);
            this.writeMultipleCoilsButton.Name = "writeMultipleCoilsButton";
            this.writeMultipleCoilsButton.Size = new System.Drawing.Size(223, 182);
            this.writeMultipleCoilsButton.TabIndex = 12;
            this.writeMultipleCoilsButton.Text = "Write Multiple Coils";
            this.writeMultipleCoilsButton.UseVisualStyleBackColor = true;
            // 
            // writeMultipleRegistersButton
            // 
            this.writeMultipleRegistersButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeMultipleRegistersButton.Location = new System.Drawing.Point(229, 632);
            this.writeMultipleRegistersButton.Margin = new System.Windows.Forms.Padding(2);
            this.writeMultipleRegistersButton.Name = "writeMultipleRegistersButton";
            this.writeMultipleRegistersButton.Size = new System.Drawing.Size(223, 182);
            this.writeMultipleRegistersButton.TabIndex = 13;
            this.writeMultipleRegistersButton.Text = "Write Multiple Registers";
            this.writeMultipleRegistersButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.actionsLayout.SetColumnSpan(this.clearLogButton, 2);
            this.clearLogButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearLogButton.Location = new System.Drawing.Point(2, 818);
            this.clearLogButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(450, 23);
            this.clearLogButton.TabIndex = 14;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // outputFrame
            // 
            this.outputFrame.Controls.Add(this.outputTextBox);
            this.outputFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFrame.Location = new System.Drawing.Point(0, 0);
            this.outputFrame.Margin = new System.Windows.Forms.Padding(2);
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Padding = new System.Windows.Forms.Padding(8);
            this.outputFrame.Size = new System.Drawing.Size(345, 872);
            this.outputFrame.TabIndex = 0;
            this.outputFrame.TabStop = false;
            this.outputFrame.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.outputTextBox.Location = new System.Drawing.Point(8, 21);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(329, 843);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // configFrame
            // 
            this.configFrame.Controls.Add(this.configLayout);
            this.configFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.configFrame.Location = new System.Drawing.Point(8, 8);
            this.configFrame.Margin = new System.Windows.Forms.Padding(2);
            this.configFrame.Name = "configFrame";
            this.configFrame.Padding = new System.Windows.Forms.Padding(8);
            this.configFrame.Size = new System.Drawing.Size(818, 81);
            this.configFrame.TabIndex = 0;
            this.configFrame.TabStop = false;
            this.configFrame.Text = "Modbus TCP/IP Configuration";
            // 
            // configLayout
            // 
            this.configLayout.ColumnCount = 4;
            this.configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.configLayout.Controls.Add(this.labelIpAddress, 0, 0);
            this.configLayout.Controls.Add(this.ipAddressComboBox, 1, 0);
            this.configLayout.Controls.Add(this.labelPort, 2, 0);
            this.configLayout.Controls.Add(this.portComboBox, 3, 0);
            this.configLayout.Controls.Add(this.labelUnitId, 0, 1);
            this.configLayout.Controls.Add(this.unitIdComboBox, 1, 1);
            this.configLayout.Controls.Add(this.connectButton, 2, 1);
            this.configLayout.Controls.Add(this.disconnectButton, 3, 1);
            this.configLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configLayout.Location = new System.Drawing.Point(8, 21);
            this.configLayout.Margin = new System.Windows.Forms.Padding(2);
            this.configLayout.Name = "configLayout";
            this.configLayout.RowCount = 2;
            this.configLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.configLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.configLayout.Size = new System.Drawing.Size(802, 52);
            this.configLayout.TabIndex = 0;
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(2, 6);
            this.labelIpAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(61, 13);
            this.labelIpAddress.TabIndex = 0;
            this.labelIpAddress.Text = "IP Address:";
            // 
            // ipAddressComboBox
            // 
            this.ipAddressComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipAddressComboBox.FormattingEnabled = true;
            this.ipAddressComboBox.Location = new System.Drawing.Point(77, 2);
            this.ipAddressComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.ipAddressComboBox.Name = "ipAddressComboBox";
            this.ipAddressComboBox.Size = new System.Drawing.Size(329, 21);
            this.ipAddressComboBox.TabIndex = 1;
            // 
            // labelPort
            // 
            this.labelPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(410, 6);
            this.labelPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "Port:";
            // 
            // portComboBox
            // 
            this.portComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(470, 2);
            this.portComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(330, 21);
            this.portComboBox.TabIndex = 3;
            // 
            // labelUnitId
            // 
            this.labelUnitId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUnitId.AutoSize = true;
            this.labelUnitId.Location = new System.Drawing.Point(2, 32);
            this.labelUnitId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUnitId.Name = "labelUnitId";
            this.labelUnitId.Size = new System.Drawing.Size(43, 13);
            this.labelUnitId.TabIndex = 4;
            this.labelUnitId.Text = "Unit ID:";
            // 
            // unitIdComboBox
            // 
            this.unitIdComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unitIdComboBox.FormattingEnabled = true;
            this.unitIdComboBox.Location = new System.Drawing.Point(77, 28);
            this.unitIdComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.unitIdComboBox.Name = "unitIdComboBox";
            this.unitIdComboBox.Size = new System.Drawing.Size(329, 21);
            this.unitIdComboBox.TabIndex = 5;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(410, 28);
            this.connectButton.Margin = new System.Windows.Forms.Padding(2);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(56, 20);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(470, 28);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(2);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(60, 20);
            this.disconnectButton.TabIndex = 7;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            // 
            // Modbus_IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Modbus_IP";
            this.Size = new System.Drawing.Size(834, 969);
            this.mainPanel.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.actionsFrame.ResumeLayout(false);
            this.actionsLayout.ResumeLayout(false);
            this.actionsLayout.PerformLayout();
            this.outputFrame.ResumeLayout(false);
            this.configFrame.ResumeLayout(false);
            this.configLayout.ResumeLayout(false);
            this.configLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox configFrame;
        private System.Windows.Forms.TableLayoutPanel configLayout;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.ComboBox ipAddressComboBox;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Label labelUnitId;
        private System.Windows.Forms.ComboBox unitIdComboBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.GroupBox actionsFrame;
        private System.Windows.Forms.TableLayoutPanel actionsLayout;
        private System.Windows.Forms.Label labelStartAddress;
        private System.Windows.Forms.TextBox startAddressTextBox;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.Label labelWriteValue;
        private System.Windows.Forms.TextBox writeValueTextBox;
        private System.Windows.Forms.Button readCoilsButton;
        private System.Windows.Forms.Button readDiscreteInputsButton;
        private System.Windows.Forms.Button readHoldingRegistersButton;
        private System.Windows.Forms.Button readInputRegistersButton;
        private System.Windows.Forms.Button writeSingleCoilButton;
        private System.Windows.Forms.Button writeSingleRegisterButton;
        private System.Windows.Forms.Button writeMultipleCoilsButton;
        private System.Windows.Forms.Button writeMultipleRegistersButton;
        private System.Windows.Forms.GroupBox outputFrame;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Button clearLogButton;
    }
}