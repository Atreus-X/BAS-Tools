namespace MainApp.Configuration
{
    partial class BACnet_MSTP_Local
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
            this.topPanelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mstpFrame = new System.Windows.Forms.GroupBox();
            this.mstpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.serialPortComboBox = new System.Windows.Forms.ComboBox();
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.maxMastersComboBox = new System.Windows.Forms.ComboBox();
            this.maxInfoFramesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.actionsFrame = new System.Windows.Forms.GroupBox();
            this.actionsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.startDiscoveryButton = new System.Windows.Forms.Button();
            this.cancelDiscoveryButton = new System.Windows.Forms.Button();
            this.discoveryStatusLabel = new System.Windows.Forms.Label();
            this.pingButton = new System.Windows.Forms.Button();
            this.discoverObjectsButton = new System.Windows.Forms.Button();
            this.readPropertyButton = new System.Windows.Forms.Button();
            this.writePropertyButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.bottomPanelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.browserFrame = new System.Windows.Forms.GroupBox();
            this.browserSplitContainer = new System.Windows.Forms.SplitContainer();
            this.deviceTreeView = new System.Windows.Forms.TreeView();
            this.objectTreeView = new System.Windows.Forms.TreeView();
            this.outputFrame = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topPanelSplitContainer)).BeginInit();
            this.topPanelSplitContainer.Panel1.SuspendLayout();
            this.topPanelSplitContainer.Panel2.SuspendLayout();
            this.topPanelSplitContainer.SuspendLayout();
            this.mstpFrame.SuspendLayout();
            this.mstpLayout.SuspendLayout();
            this.actionsFrame.SuspendLayout();
            this.actionsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPanelSplitContainer)).BeginInit();
            this.bottomPanelSplitContainer.Panel1.SuspendLayout();
            this.bottomPanelSplitContainer.Panel2.SuspendLayout();
            this.bottomPanelSplitContainer.SuspendLayout();
            this.browserFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).BeginInit();
            this.browserSplitContainer.Panel1.SuspendLayout();
            this.browserSplitContainer.Panel2.SuspendLayout();
            this.browserSplitContainer.SuspendLayout();
            this.outputFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.mainSplitContainer);
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
            this.mainSplitContainer.Location = new System.Drawing.Point(8, 8);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.topPanelSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.bottomPanelSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(818, 953);
            this.mainSplitContainer.SplitterDistance = 225;
            this.mainSplitContainer.SplitterWidth = 3;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // topPanelSplitContainer
            // 
            this.topPanelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.topPanelSplitContainer.IsSplitterFixed = true;
            this.topPanelSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.topPanelSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.topPanelSplitContainer.Name = "topPanelSplitContainer";
            this.topPanelSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topPanelSplitContainer.Panel1
            // 
            this.topPanelSplitContainer.Panel1.Controls.Add(this.mstpFrame);
            // 
            // topPanelSplitContainer.Panel2
            // 
            this.topPanelSplitContainer.Panel2.Controls.Add(this.actionsFrame);
            this.topPanelSplitContainer.Size = new System.Drawing.Size(818, 410);
            this.topPanelSplitContainer.SplitterDistance = 337;
            this.topPanelSplitContainer.SplitterWidth = 3;
            this.topPanelSplitContainer.TabIndex = 0;
            // 
            // mstpFrame
            // 
            this.mstpFrame.AutoSize = true;
            this.mstpFrame.Controls.Add(this.mstpLayout);
            this.mstpFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.mstpFrame.Location = new System.Drawing.Point(0, 0);
            this.mstpFrame.Margin = new System.Windows.Forms.Padding(2);
            this.mstpFrame.Name = "mstpFrame";
            this.mstpFrame.Padding = new System.Windows.Forms.Padding(8);
            this.mstpFrame.Size = new System.Drawing.Size(818, 131);
            this.mstpFrame.TabIndex = 0;
            this.mstpFrame.TabStop = false;
            this.mstpFrame.Text = "BACnet MS/TP Local Configuration";
            // 
            // mstpLayout
            // 
            this.mstpLayout.AutoSize = true;
            this.mstpLayout.ColumnCount = 4;
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mstpLayout.Controls.Add(this.serialPortComboBox, 1, 0);
            this.mstpLayout.Controls.Add(this.baudRateComboBox, 1, 1);
            this.mstpLayout.Controls.Add(this.maxMastersComboBox, 3, 1);
            this.mstpLayout.Controls.Add(this.maxInfoFramesComboBox, 1, 2);
            this.mstpLayout.Controls.Add(this.label1, 0, 0);
            this.mstpLayout.Controls.Add(this.label3, 0, 1);
            this.mstpLayout.Controls.Add(this.label4, 2, 1);
            this.mstpLayout.Controls.Add(this.label5, 0, 2);
            this.mstpLayout.Controls.Add(this.connectButton, 1, 3);
            this.mstpLayout.Controls.Add(this.disconnectButton, 3, 3);
            this.mstpLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mstpLayout.Location = new System.Drawing.Point(8, 21);
            this.mstpLayout.Margin = new System.Windows.Forms.Padding(2);
            this.mstpLayout.Name = "mstpLayout";
            this.mstpLayout.RowCount = 4;
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mstpLayout.Size = new System.Drawing.Size(802, 102);
            this.mstpLayout.TabIndex = 0;
            // 
            // serialPortComboBox
            // 
            this.serialPortComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serialPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialPortComboBox.FormattingEnabled = true;
            this.serialPortComboBox.Location = new System.Drawing.Point(94, 2);
            this.serialPortComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.serialPortComboBox.Name = "serialPortComboBox";
            this.serialPortComboBox.Size = new System.Drawing.Size(314, 21);
            this.serialPortComboBox.TabIndex = 0;
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Location = new System.Drawing.Point(94, 27);
            this.baudRateComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(314, 21);
            this.baudRateComboBox.TabIndex = 2;
            // 
            // maxMastersComboBox
            // 
            this.maxMastersComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxMastersComboBox.FormattingEnabled = true;
            this.maxMastersComboBox.Location = new System.Drawing.Point(486, 27);
            this.maxMastersComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.maxMastersComboBox.Name = "maxMastersComboBox";
            this.maxMastersComboBox.Size = new System.Drawing.Size(314, 21);
            this.maxMastersComboBox.TabIndex = 3;
            // 
            // maxInfoFramesComboBox
            // 
            this.maxInfoFramesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxInfoFramesComboBox.FormattingEnabled = true;
            this.maxInfoFramesComboBox.Location = new System.Drawing.Point(94, 52);
            this.maxInfoFramesComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.maxInfoFramesComboBox.Name = "maxInfoFramesComboBox";
            this.maxInfoFramesComboBox.Size = new System.Drawing.Size(314, 21);
            this.maxInfoFramesComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Serial Port:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Baud Rate:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Max Masters:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 56);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Max Info Frames:";
            // 
            // connectButton
            // 
            this.connectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectButton.AutoSize = true;
            this.connectButton.Location = new System.Drawing.Point(213, 77);
            this.connectButton.Margin = new System.Windows.Forms.Padding(2);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 10;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.disconnectButton.AutoSize = true;
            this.disconnectButton.Location = new System.Drawing.Point(605, 77);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(2);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 11;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            // 
            // actionsFrame
            // 
            this.actionsFrame.AutoSize = true;
            this.actionsFrame.Controls.Add(this.actionsLayout);
            this.actionsFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsFrame.Location = new System.Drawing.Point(0, 0);
            this.actionsFrame.Margin = new System.Windows.Forms.Padding(2);
            this.actionsFrame.Name = "actionsFrame";
            this.actionsFrame.Padding = new System.Windows.Forms.Padding(2);
            this.actionsFrame.Size = new System.Drawing.Size(818, 70);
            this.actionsFrame.TabIndex = 1;
            this.actionsFrame.TabStop = false;
            this.actionsFrame.Text = "Actions";
            // 
            // actionsLayout
            // 
            this.actionsLayout.AutoSize = true;
            this.actionsLayout.Controls.Add(this.startDiscoveryButton);
            this.actionsLayout.Controls.Add(this.cancelDiscoveryButton);
            this.actionsLayout.Controls.Add(this.discoveryStatusLabel);
            this.actionsLayout.Controls.Add(this.pingButton);
            this.actionsLayout.Controls.Add(this.discoverObjectsButton);
            this.actionsLayout.Controls.Add(this.readPropertyButton);
            this.actionsLayout.Controls.Add(this.writePropertyButton);
            this.actionsLayout.Controls.Add(this.clearLogButton);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(2, 15);
            this.actionsLayout.Margin = new System.Windows.Forms.Padding(2);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.Size = new System.Drawing.Size(814, 53);
            this.actionsLayout.TabIndex = 0;
            // 
            // startDiscoveryButton
            // 
            this.startDiscoveryButton.AutoSize = true;
            this.startDiscoveryButton.Location = new System.Drawing.Point(2, 2);
            this.startDiscoveryButton.Margin = new System.Windows.Forms.Padding(2);
            this.startDiscoveryButton.Name = "startDiscoveryButton";
            this.startDiscoveryButton.Size = new System.Drawing.Size(59, 23);
            this.startDiscoveryButton.TabIndex = 0;
            this.startDiscoveryButton.Text = "Discover";
            this.startDiscoveryButton.UseVisualStyleBackColor = true;
            // 
            // cancelDiscoveryButton
            // 
            this.cancelDiscoveryButton.AutoSize = true;
            this.cancelDiscoveryButton.Location = new System.Drawing.Point(65, 2);
            this.cancelDiscoveryButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelDiscoveryButton.Name = "cancelDiscoveryButton";
            this.cancelDiscoveryButton.Size = new System.Drawing.Size(56, 23);
            this.cancelDiscoveryButton.TabIndex = 1;
            this.cancelDiscoveryButton.Text = "Cancel";
            this.cancelDiscoveryButton.UseVisualStyleBackColor = true;
            this.cancelDiscoveryButton.Visible = false;
            // 
            // discoveryStatusLabel
            // 
            this.discoveryStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.discoveryStatusLabel.AutoSize = true;
            this.discoveryStatusLabel.Location = new System.Drawing.Point(125, 7);
            this.discoveryStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.discoveryStatusLabel.Name = "discoveryStatusLabel";
            this.discoveryStatusLabel.Size = new System.Drawing.Size(49, 13);
            this.discoveryStatusLabel.TabIndex = 2;
            this.discoveryStatusLabel.Text = "Found: 0";
            this.discoveryStatusLabel.Visible = false;
            // 
            // pingButton
            // 
            this.pingButton.AutoSize = true;
            this.pingButton.Location = new System.Drawing.Point(178, 2);
            this.pingButton.Margin = new System.Windows.Forms.Padding(2);
            this.pingButton.Name = "pingButton";
            this.pingButton.Size = new System.Drawing.Size(75, 23);
            this.pingButton.TabIndex = 3;
            this.pingButton.Text = "Ping Device";
            this.pingButton.UseVisualStyleBackColor = true;
            // 
            // discoverObjectsButton
            // 
            this.discoverObjectsButton.AutoSize = true;
            this.discoverObjectsButton.Location = new System.Drawing.Point(257, 2);
            this.discoverObjectsButton.Margin = new System.Windows.Forms.Padding(2);
            this.discoverObjectsButton.Name = "discoverObjectsButton";
            this.discoverObjectsButton.Size = new System.Drawing.Size(98, 23);
            this.discoverObjectsButton.TabIndex = 4;
            this.discoverObjectsButton.Text = "Discover Objects";
            this.discoverObjectsButton.UseVisualStyleBackColor = true;
            // 
            // readPropertyButton
            // 
            this.readPropertyButton.AutoSize = true;
            this.readPropertyButton.Location = new System.Drawing.Point(359, 2);
            this.readPropertyButton.Margin = new System.Windows.Forms.Padding(2);
            this.readPropertyButton.Name = "readPropertyButton";
            this.readPropertyButton.Size = new System.Drawing.Size(85, 23);
            this.readPropertyButton.TabIndex = 5;
            this.readPropertyButton.Text = "Read Property";
            this.readPropertyButton.UseVisualStyleBackColor = true;
            // 
            // writePropertyButton
            // 
            this.writePropertyButton.AutoSize = true;
            this.writePropertyButton.Location = new System.Drawing.Point(448, 2);
            this.writePropertyButton.Margin = new System.Windows.Forms.Padding(2);
            this.writePropertyButton.Name = "writePropertyButton";
            this.writePropertyButton.Size = new System.Drawing.Size(84, 23);
            this.writePropertyButton.TabIndex = 6;
            this.writePropertyButton.Text = "Write Property";
            this.writePropertyButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.AutoSize = true;
            this.clearLogButton.Location = new System.Drawing.Point(536, 2);
            this.clearLogButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(68, 23);
            this.clearLogButton.TabIndex = 7;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // bottomPanelSplitContainer
            // 
            this.bottomPanelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanelSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomPanelSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanelSplitContainer.Name = "bottomPanelSplitContainer";
            // 
            // bottomPanelSplitContainer.Panel1
            // 
            this.bottomPanelSplitContainer.Panel1.Controls.Add(this.browserFrame);
            // 
            // bottomPanelSplitContainer.Panel2
            // 
            this.bottomPanelSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.bottomPanelSplitContainer.Size = new System.Drawing.Size(818, 540);
            this.bottomPanelSplitContainer.SplitterDistance = 409;
            this.bottomPanelSplitContainer.SplitterWidth = 3;
            this.bottomPanelSplitContainer.TabIndex = 0;
            // 
            // browserFrame
            // 
            this.browserFrame.Controls.Add(this.browserSplitContainer);
            this.browserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserFrame.Location = new System.Drawing.Point(0, 0);
            this.browserFrame.Margin = new System.Windows.Forms.Padding(2);
            this.browserFrame.Name = "browserFrame";
            this.browserFrame.Padding = new System.Windows.Forms.Padding(2);
            this.browserFrame.Size = new System.Drawing.Size(409, 540);
            this.browserFrame.TabIndex = 0;
            this.browserFrame.TabStop = false;
            this.browserFrame.Text = "Device & Object Browser";
            // 
            // browserSplitContainer
            // 
            this.browserSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserSplitContainer.Location = new System.Drawing.Point(2, 15);
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
            this.browserSplitContainer.Size = new System.Drawing.Size(405, 523);
            this.browserSplitContainer.SplitterDistance = 257;
            this.browserSplitContainer.SplitterWidth = 3;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(405, 257);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(405, 263);
            this.objectTreeView.TabIndex = 0;
            // 
            // outputFrame
            // 
            this.outputFrame.Controls.Add(this.outputTextBox);
            this.outputFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFrame.Location = new System.Drawing.Point(0, 0);
            this.outputFrame.Margin = new System.Windows.Forms.Padding(2);
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Padding = new System.Windows.Forms.Padding(2);
            this.outputFrame.Size = new System.Drawing.Size(406, 540);
            this.outputFrame.TabIndex = 0;
            this.outputFrame.TabStop = false;
            this.outputFrame.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(2, 15);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(402, 523);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // BACnet_MSTP_Local
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BACnet_MSTP_Local";
            this.Size = new System.Drawing.Size(834, 969);
            this.mainPanel.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.topPanelSplitContainer.Panel1.ResumeLayout(false);
            this.topPanelSplitContainer.Panel1.PerformLayout();
            this.topPanelSplitContainer.Panel2.ResumeLayout(false);
            this.topPanelSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topPanelSplitContainer)).EndInit();
            this.topPanelSplitContainer.ResumeLayout(false);
            this.mstpFrame.ResumeLayout(false);
            this.mstpFrame.PerformLayout();
            this.mstpLayout.ResumeLayout(false);
            this.mstpLayout.PerformLayout();
            this.actionsFrame.ResumeLayout(false);
            this.actionsFrame.PerformLayout();
            this.actionsLayout.ResumeLayout(false);
            this.actionsLayout.PerformLayout();
            this.bottomPanelSplitContainer.Panel1.ResumeLayout(false);
            this.bottomPanelSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomPanelSplitContainer)).EndInit();
            this.bottomPanelSplitContainer.ResumeLayout(false);
            this.browserFrame.ResumeLayout(false);
            this.browserSplitContainer.Panel1.ResumeLayout(false);
            this.browserSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).EndInit();
            this.browserSplitContainer.ResumeLayout(false);
            this.outputFrame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox mstpFrame;
        private System.Windows.Forms.TableLayoutPanel mstpLayout;
        private System.Windows.Forms.ComboBox serialPortComboBox;
        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.ComboBox maxMastersComboBox;
        private System.Windows.Forms.ComboBox maxInfoFramesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.GroupBox actionsFrame;
        private System.Windows.Forms.FlowLayoutPanel actionsLayout;
        private System.Windows.Forms.Button startDiscoveryButton;
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
        private System.Windows.Forms.Button cancelDiscoveryButton;
        private System.Windows.Forms.Label discoveryStatusLabel;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.SplitContainer topPanelSplitContainer;
        private System.Windows.Forms.SplitContainer bottomPanelSplitContainer;
    }
}