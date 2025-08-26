namespace MainApp.Configuration
{
    partial class BACnet_IP
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
            this.browserFrame = new System.Windows.Forms.GroupBox();
            this.browserSplitContainer = new System.Windows.Forms.SplitContainer();
            this.deviceTreeView = new System.Windows.Forms.TreeView();
            this.objectTreeView = new System.Windows.Forms.TreeView();
            this.outputFrame = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.actionsFrame = new System.Windows.Forms.GroupBox();
            this.actionsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.discoverButton = new System.Windows.Forms.Button();
            this.pingButton = new System.Windows.Forms.Button();
            this.discoverObjectsButton = new System.Windows.Forms.Button();
            this.readPropertyButton = new System.Windows.Forms.Button();
            this.writePropertyButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.ipFrame = new System.Windows.Forms.GroupBox();
            this.ipLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelTargetIP = new System.Windows.Forms.Label();
            this.ipAddressComboBox = new System.Windows.Forms.ComboBox();
            this.labelInstance = new System.Windows.Forms.Label();
            this.instanceNumberComboBox = new System.Windows.Forms.ComboBox();
            this.labelLocalInterface = new System.Windows.Forms.Label();
            this.interfaceComboBox = new System.Windows.Forms.ComboBox();
            this.labelBbmdIp = new System.Windows.Forms.Label();
            this.bbmdIpComboBox = new System.Windows.Forms.ComboBox();
            this.labelUdpPort = new System.Windows.Forms.Label();
            this.ipPortComboBox = new System.Windows.Forms.ComboBox();
            this.labelApduTimeout = new System.Windows.Forms.Label();
            this.apduTimeoutComboBox = new System.Windows.Forms.ComboBox();
            this.labelBbmdTtl = new System.Windows.Forms.Label();
            this.bbmdTtlComboBox = new System.Windows.Forms.ComboBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.browserFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).BeginInit();
            this.browserSplitContainer.Panel1.SuspendLayout();
            this.browserSplitContainer.Panel2.SuspendLayout();
            this.browserSplitContainer.SuspendLayout();
            this.outputFrame.SuspendLayout();
            this.actionsFrame.SuspendLayout();
            this.actionsLayout.SuspendLayout();
            this.ipFrame.SuspendLayout();
            this.ipLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.mainSplitContainer);
            this.mainPanel.Controls.Add(this.actionsFrame);
            this.mainPanel.Controls.Add(this.ipFrame);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(8);
            this.mainPanel.Size = new System.Drawing.Size(600, 488);
            this.mainPanel.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(8, 195);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.browserFrame);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.mainSplitContainer.Size = new System.Drawing.Size(584, 285);
            this.mainSplitContainer.SplitterDistance = 336;
            this.mainSplitContainer.SplitterWidth = 3;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // browserFrame
            // 
            this.browserFrame.Controls.Add(this.browserSplitContainer);
            this.browserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserFrame.Location = new System.Drawing.Point(0, 0);
            this.browserFrame.Margin = new System.Windows.Forms.Padding(2);
            this.browserFrame.Name = "browserFrame";
            this.browserFrame.Padding = new System.Windows.Forms.Padding(8);
            this.browserFrame.Size = new System.Drawing.Size(336, 285);
            this.browserFrame.TabIndex = 0;
            this.browserFrame.TabStop = false;
            this.browserFrame.Text = "Device & Object Browser";
            // 
            // browserSplitContainer
            // 
            this.browserSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserSplitContainer.Location = new System.Drawing.Point(8, 21);
            this.browserSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.browserSplitContainer.Name = "browserSplitContainer";
            this.browserSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // browserSplitContainer.Panel1
            // 
            this.browserSplitContainer.Panel1.Controls.Add(this.deviceTreeView);
            // 
            // browserSplitContainer.Panel2
            // 
            this.browserSplitContainer.Panel2.Controls.Add(this.objectTreeView);
            this.browserSplitContainer.Size = new System.Drawing.Size(320, 256);
            this.browserSplitContainer.SplitterDistance = 123;
            this.browserSplitContainer.SplitterWidth = 3;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.HideSelection = false;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(320, 123);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.HideSelection = false;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(320, 130);
            this.objectTreeView.TabIndex = 0;
            // 
            // outputFrame
            // 
            this.outputFrame.Controls.Add(this.outputTextBox);
            this.outputFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFrame.Location = new System.Drawing.Point(0, 0);
            this.outputFrame.Margin = new System.Windows.Forms.Padding(2);
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Padding = new System.Windows.Forms.Padding(8);
            this.outputFrame.Size = new System.Drawing.Size(245, 285);
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
            this.outputTextBox.Size = new System.Drawing.Size(229, 256);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // actionsFrame
            // 
            this.actionsFrame.Controls.Add(this.actionsLayout);
            this.actionsFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionsFrame.Location = new System.Drawing.Point(8, 138);
            this.actionsFrame.Margin = new System.Windows.Forms.Padding(2);
            this.actionsFrame.Name = "actionsFrame";
            this.actionsFrame.Padding = new System.Windows.Forms.Padding(8);
            this.actionsFrame.Size = new System.Drawing.Size(584, 57);
            this.actionsFrame.TabIndex = 1;
            this.actionsFrame.TabStop = false;
            this.actionsFrame.Text = "Actions";
            // 
            // actionsLayout
            // 
            this.actionsLayout.Controls.Add(this.discoverButton);
            this.actionsLayout.Controls.Add(this.pingButton);
            this.actionsLayout.Controls.Add(this.discoverObjectsButton);
            this.actionsLayout.Controls.Add(this.readPropertyButton);
            this.actionsLayout.Controls.Add(this.writePropertyButton);
            this.actionsLayout.Controls.Add(this.clearLogButton);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(8, 21);
            this.actionsLayout.Margin = new System.Windows.Forms.Padding(2);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.Size = new System.Drawing.Size(568, 28);
            this.actionsLayout.TabIndex = 0;
            // 
            // discoverButton
            // 
            this.discoverButton.Location = new System.Drawing.Point(2, 2);
            this.discoverButton.Margin = new System.Windows.Forms.Padding(2);
            this.discoverButton.Name = "discoverButton";
            this.discoverButton.Size = new System.Drawing.Size(90, 19);
            this.discoverButton.TabIndex = 0;
            this.discoverButton.Text = "Discover Devices";
            this.discoverButton.UseVisualStyleBackColor = true;
            // 
            // pingButton
            // 
            this.pingButton.Location = new System.Drawing.Point(96, 2);
            this.pingButton.Margin = new System.Windows.Forms.Padding(2);
            this.pingButton.Name = "pingButton";
            this.pingButton.Size = new System.Drawing.Size(90, 19);
            this.pingButton.TabIndex = 1;
            this.pingButton.Text = "Ping Device";
            this.pingButton.UseVisualStyleBackColor = true;
            // 
            // discoverObjectsButton
            // 
            this.discoverObjectsButton.Location = new System.Drawing.Point(190, 2);
            this.discoverObjectsButton.Margin = new System.Windows.Forms.Padding(2);
            this.discoverObjectsButton.Name = "discoverObjectsButton";
            this.discoverObjectsButton.Size = new System.Drawing.Size(90, 19);
            this.discoverObjectsButton.TabIndex = 2;
            this.discoverObjectsButton.Text = "Discover Objects";
            this.discoverObjectsButton.UseVisualStyleBackColor = true;
            // 
            // readPropertyButton
            // 
            this.readPropertyButton.Location = new System.Drawing.Point(284, 2);
            this.readPropertyButton.Margin = new System.Windows.Forms.Padding(2);
            this.readPropertyButton.Name = "readPropertyButton";
            this.readPropertyButton.Size = new System.Drawing.Size(90, 19);
            this.readPropertyButton.TabIndex = 3;
            this.readPropertyButton.Text = "Read Property";
            this.readPropertyButton.UseVisualStyleBackColor = true;
            // 
            // writePropertyButton
            // 
            this.writePropertyButton.Enabled = false;
            this.writePropertyButton.Location = new System.Drawing.Point(378, 2);
            this.writePropertyButton.Margin = new System.Windows.Forms.Padding(2);
            this.writePropertyButton.Name = "writePropertyButton";
            this.writePropertyButton.Size = new System.Drawing.Size(90, 19);
            this.writePropertyButton.TabIndex = 4;
            this.writePropertyButton.Text = "Write Property";
            this.writePropertyButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(472, 2);
            this.clearLogButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(90, 19);
            this.clearLogButton.TabIndex = 5;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // ipFrame
            // 
            this.ipFrame.Controls.Add(this.ipLayout);
            this.ipFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipFrame.Location = new System.Drawing.Point(8, 8);
            this.ipFrame.Margin = new System.Windows.Forms.Padding(2);
            this.ipFrame.Name = "ipFrame";
            this.ipFrame.Padding = new System.Windows.Forms.Padding(8);
            this.ipFrame.Size = new System.Drawing.Size(584, 130);
            this.ipFrame.TabIndex = 0;
            this.ipFrame.TabStop = false;
            this.ipFrame.Text = "BACnet/IP Configuration";
            // 
            // ipLayout
            // 
            this.ipLayout.ColumnCount = 4;
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ipLayout.Controls.Add(this.labelTargetIP, 0, 0);
            this.ipLayout.Controls.Add(this.ipAddressComboBox, 1, 0);
            this.ipLayout.Controls.Add(this.labelInstance, 2, 0);
            this.ipLayout.Controls.Add(this.instanceNumberComboBox, 3, 0);
            this.ipLayout.Controls.Add(this.labelLocalInterface, 0, 1);
            this.ipLayout.Controls.Add(this.interfaceComboBox, 1, 1);
            this.ipLayout.Controls.Add(this.labelBbmdIp, 0, 2);
            this.ipLayout.Controls.Add(this.bbmdIpComboBox, 1, 2);
            this.ipLayout.Controls.Add(this.labelUdpPort, 2, 2);
            this.ipLayout.Controls.Add(this.ipPortComboBox, 3, 2);
            this.ipLayout.Controls.Add(this.labelApduTimeout, 0, 3);
            this.ipLayout.Controls.Add(this.apduTimeoutComboBox, 1, 3);
            this.ipLayout.Controls.Add(this.labelBbmdTtl, 2, 3);
            this.ipLayout.Controls.Add(this.bbmdTtlComboBox, 3, 3);
            this.ipLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipLayout.Location = new System.Drawing.Point(8, 21);
            this.ipLayout.Margin = new System.Windows.Forms.Padding(2);
            this.ipLayout.Name = "ipLayout";
            this.ipLayout.RowCount = 4;
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ipLayout.Size = new System.Drawing.Size(568, 101);
            this.ipLayout.TabIndex = 0;
            // 
            // labelTargetIP
            // 
            this.labelTargetIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTargetIP.AutoSize = true;
            this.labelTargetIP.Location = new System.Drawing.Point(2, 0);
            this.labelTargetIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTargetIP.Name = "labelTargetIP";
            this.labelTargetIP.Size = new System.Drawing.Size(78, 25);
            this.labelTargetIP.TabIndex = 0;
            this.labelTargetIP.Text = "Target Device IP:";
            // 
            // ipAddressComboBox
            // 
            this.ipAddressComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipAddressComboBox.FormattingEnabled = true;
            this.ipAddressComboBox.Location = new System.Drawing.Point(92, 2);
            this.ipAddressComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.ipAddressComboBox.Name = "ipAddressComboBox";
            this.ipAddressComboBox.Size = new System.Drawing.Size(186, 21);
            this.ipAddressComboBox.TabIndex = 1;
            // 
            // labelInstance
            // 
            this.labelInstance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelInstance.AutoSize = true;
            this.labelInstance.Location = new System.Drawing.Point(282, 0);
            this.labelInstance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInstance.Name = "labelInstance";
            this.labelInstance.Size = new System.Drawing.Size(85, 25);
            this.labelInstance.TabIndex = 2;
            this.labelInstance.Text = "Target Instance #:";
            // 
            // instanceNumberComboBox
            // 
            this.instanceNumberComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instanceNumberComboBox.FormattingEnabled = true;
            this.instanceNumberComboBox.Location = new System.Drawing.Point(380, 2);
            this.instanceNumberComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.instanceNumberComboBox.Name = "instanceNumberComboBox";
            this.instanceNumberComboBox.Size = new System.Drawing.Size(186, 21);
            this.instanceNumberComboBox.TabIndex = 3;
            // 
            // labelLocalInterface
            // 
            this.labelLocalInterface.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLocalInterface.AutoSize = true;
            this.labelLocalInterface.Location = new System.Drawing.Point(2, 31);
            this.labelLocalInterface.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLocalInterface.Name = "labelLocalInterface";
            this.labelLocalInterface.Size = new System.Drawing.Size(81, 13);
            this.labelLocalInterface.TabIndex = 4;
            this.labelLocalInterface.Text = "Local Interface:";
            // 
            // interfaceComboBox
            // 
            this.ipLayout.SetColumnSpan(this.interfaceComboBox, 3);
            this.interfaceComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.interfaceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interfaceComboBox.FormattingEnabled = true;
            this.interfaceComboBox.Location = new System.Drawing.Point(92, 27);
            this.interfaceComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.interfaceComboBox.Name = "interfaceComboBox";
            this.interfaceComboBox.Size = new System.Drawing.Size(474, 21);
            this.interfaceComboBox.TabIndex = 5;
            // 
            // labelBbmdIp
            // 
            this.labelBbmdIp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBbmdIp.AutoSize = true;
            this.labelBbmdIp.Location = new System.Drawing.Point(2, 56);
            this.labelBbmdIp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBbmdIp.Name = "labelBbmdIp";
            this.labelBbmdIp.Size = new System.Drawing.Size(54, 13);
            this.labelBbmdIp.TabIndex = 6;
            this.labelBbmdIp.Text = "BBMD IP:";
            // 
            // bbmdIpComboBox
            // 
            this.bbmdIpComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bbmdIpComboBox.FormattingEnabled = true;
            this.bbmdIpComboBox.Location = new System.Drawing.Point(92, 52);
            this.bbmdIpComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.bbmdIpComboBox.Name = "bbmdIpComboBox";
            this.bbmdIpComboBox.Size = new System.Drawing.Size(186, 21);
            this.bbmdIpComboBox.TabIndex = 7;
            // 
            // labelUdpPort
            // 
            this.labelUdpPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUdpPort.AutoSize = true;
            this.labelUdpPort.Location = new System.Drawing.Point(282, 56);
            this.labelUdpPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUdpPort.Name = "labelUdpPort";
            this.labelUdpPort.Size = new System.Drawing.Size(84, 13);
            this.labelUdpPort.TabIndex = 8;
            this.labelUdpPort.Text = "Local UDP Port:";
            // 
            // ipPortComboBox
            // 
            this.ipPortComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipPortComboBox.FormattingEnabled = true;
            this.ipPortComboBox.Location = new System.Drawing.Point(380, 52);
            this.ipPortComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.ipPortComboBox.Name = "ipPortComboBox";
            this.ipPortComboBox.Size = new System.Drawing.Size(186, 21);
            this.ipPortComboBox.TabIndex = 9;
            // 
            // labelApduTimeout
            // 
            this.labelApduTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelApduTimeout.AutoSize = true;
            this.labelApduTimeout.Location = new System.Drawing.Point(2, 75);
            this.labelApduTimeout.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelApduTimeout.Name = "labelApduTimeout";
            this.labelApduTimeout.Size = new System.Drawing.Size(81, 26);
            this.labelApduTimeout.TabIndex = 10;
            this.labelApduTimeout.Text = "APDU Timeout (ms):";
            // 
            // apduTimeoutComboBox
            // 
            this.apduTimeoutComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apduTimeoutComboBox.FormattingEnabled = true;
            this.apduTimeoutComboBox.Location = new System.Drawing.Point(92, 77);
            this.apduTimeoutComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.apduTimeoutComboBox.Name = "apduTimeoutComboBox";
            this.apduTimeoutComboBox.Size = new System.Drawing.Size(186, 21);
            this.apduTimeoutComboBox.TabIndex = 11;
            // 
            // labelBbmdTtl
            // 
            this.labelBbmdTtl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBbmdTtl.AutoSize = true;
            this.labelBbmdTtl.Location = new System.Drawing.Point(282, 81);
            this.labelBbmdTtl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBbmdTtl.Name = "labelBbmdTtl";
            this.labelBbmdTtl.Size = new System.Drawing.Size(78, 13);
            this.labelBbmdTtl.TabIndex = 12;
            this.labelBbmdTtl.Text = "BBMD TTL (s):";
            // 
            // bbmdTtlComboBox
            // 
            this.bbmdTtlComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bbmdTtlComboBox.FormattingEnabled = true;
            this.bbmdTtlComboBox.Location = new System.Drawing.Point(380, 77);
            this.bbmdTtlComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.bbmdTtlComboBox.Name = "bbmdTtlComboBox";
            this.bbmdTtlComboBox.Size = new System.Drawing.Size(186, 21);
            this.bbmdTtlComboBox.TabIndex = 13;
            // 
            // BACnet_IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BACnet_IP";
            this.Size = new System.Drawing.Size(600, 488);
            this.mainPanel.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.browserFrame.ResumeLayout(false);
            this.browserSplitContainer.Panel1.ResumeLayout(false);
            this.browserSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).EndInit();
            this.browserSplitContainer.ResumeLayout(false);
            this.outputFrame.ResumeLayout(false);
            this.actionsFrame.ResumeLayout(false);
            this.actionsLayout.ResumeLayout(false);
            this.ipFrame.ResumeLayout(false);
            this.ipLayout.ResumeLayout(false);
            this.ipLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox ipFrame;
        private System.Windows.Forms.TableLayoutPanel ipLayout;
        private System.Windows.Forms.Label labelTargetIP;
        private System.Windows.Forms.ComboBox ipAddressComboBox;
        private System.Windows.Forms.Label labelInstance;
        private System.Windows.Forms.ComboBox instanceNumberComboBox;
        private System.Windows.Forms.Label labelLocalInterface;
        private System.Windows.Forms.ComboBox interfaceComboBox;
        private System.Windows.Forms.Label labelBbmdIp;
        private System.Windows.Forms.ComboBox bbmdIpComboBox;
        private System.Windows.Forms.Label labelUdpPort;
        private System.Windows.Forms.ComboBox ipPortComboBox;
        private System.Windows.Forms.Label labelApduTimeout;
        private System.Windows.Forms.ComboBox apduTimeoutComboBox;
        private System.Windows.Forms.Label labelBbmdTtl;
        private System.Windows.Forms.ComboBox bbmdTtlComboBox;
        private System.Windows.Forms.GroupBox actionsFrame;
        private System.Windows.Forms.FlowLayoutPanel actionsLayout;
        private System.Windows.Forms.Button discoverButton;
        private System.Windows.Forms.Button pingButton;
        private System.Windows.Forms.Button discoverObjectsButton;
        private System.Windows.Forms.Button readPropertyButton;
        private System.Windows.Forms.Button writePropertyButton;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.GroupBox browserFrame;
        private System.Windows.Forms.SplitContainer browserSplitContainer;
        private System.Windows.Forms.TreeView deviceTreeView;
        private System.Windows.Forms.TreeView objectTreeView;
        private System.Windows.Forms.GroupBox outputFrame;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Button clearLogButton;
    }
}