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
            this.manualReadWriteButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.bottomPanelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftPanelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.browserFrame = new System.Windows.Forms.GroupBox();
            this.browserSplitContainer = new System.Windows.Forms.SplitContainer();
            this.deviceTreeView = new System.Windows.Forms.TreeView();
            this.objectTreeView = new System.Windows.Forms.TreeView();
            this.propertiesFrame = new System.Windows.Forms.GroupBox();
            this.propertiesDataGridView = new System.Windows.Forms.DataGridView();
            this.colProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pollingPanel = new System.Windows.Forms.Panel();
            this.writePriorityComboBox = new System.Windows.Forms.ComboBox();
            this.labelWritePriority = new System.Windows.Forms.Label();
            this.togglePollingButton = new System.Windows.Forms.Button();
            this.readIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.labelReadInterval = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.leftPanelSplitContainer)).BeginInit();
            this.leftPanelSplitContainer.Panel1.SuspendLayout();
            this.leftPanelSplitContainer.Panel2.SuspendLayout();
            this.leftPanelSplitContainer.SuspendLayout();
            this.browserFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).BeginInit();
            this.browserSplitContainer.Panel1.SuspendLayout();
            this.browserSplitContainer.Panel2.SuspendLayout();
            this.browserSplitContainer.SuspendLayout();
            this.propertiesFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).BeginInit();
            this.pollingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readIntervalNumericUpDown)).BeginInit();
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
            this.mainSplitContainer.SplitterDistance = 175;
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
            this.topPanelSplitContainer.Size = new System.Drawing.Size(818, 175);
            this.topPanelSplitContainer.SplitterDistance = 120;
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
            this.actionsFrame.Size = new System.Drawing.Size(818, 52);
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
            this.actionsLayout.Controls.Add(this.manualReadWriteButton);
            this.actionsLayout.Controls.Add(this.clearLogButton);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(2, 15);
            this.actionsLayout.Margin = new System.Windows.Forms.Padding(2);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.Size = new System.Drawing.Size(814, 35);
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
            // manualReadWriteButton
            // 
            this.manualReadWriteButton.AutoSize = true;
            this.manualReadWriteButton.Location = new System.Drawing.Point(359, 2);
            this.manualReadWriteButton.Margin = new System.Windows.Forms.Padding(2);
            this.manualReadWriteButton.Name = "manualReadWriteButton";
            this.manualReadWriteButton.Size = new System.Drawing.Size(110, 23);
            this.manualReadWriteButton.TabIndex = 5;
            this.manualReadWriteButton.Text = "Manual Read/Write";
            this.manualReadWriteButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.AutoSize = true;
            this.clearLogButton.Location = new System.Drawing.Point(473, 2);
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
            this.bottomPanelSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // bottomPanelSplitContainer.Panel1
            // 
            this.bottomPanelSplitContainer.Panel1.Controls.Add(this.leftPanelSplitContainer);
            // 
            // bottomPanelSplitContainer.Panel2
            // 
            this.bottomPanelSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.bottomPanelSplitContainer.Size = new System.Drawing.Size(818, 775);
            this.bottomPanelSplitContainer.SplitterDistance = 500;
            this.bottomPanelSplitContainer.SplitterWidth = 3;
            this.bottomPanelSplitContainer.TabIndex = 0;
            // 
            // leftPanelSplitContainer
            // 
            this.leftPanelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanelSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftPanelSplitContainer.Name = "leftPanelSplitContainer";
            // 
            // leftPanelSplitContainer.Panel1
            // 
            this.leftPanelSplitContainer.Panel1.Controls.Add(this.browserFrame);
            // 
            // leftPanelSplitContainer.Panel2
            // 
            this.leftPanelSplitContainer.Panel2.Controls.Add(this.propertiesFrame);
            this.leftPanelSplitContainer.Size = new System.Drawing.Size(818, 500);
            this.leftPanelSplitContainer.SplitterDistance = 400;
            this.leftPanelSplitContainer.TabIndex = 0;
            // 
            // browserFrame
            // 
            this.browserFrame.Controls.Add(this.browserSplitContainer);
            this.browserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserFrame.Location = new System.Drawing.Point(0, 0);
            this.browserFrame.Margin = new System.Windows.Forms.Padding(2);
            this.browserFrame.Name = "browserFrame";
            this.browserFrame.Padding = new System.Windows.Forms.Padding(2);
            this.browserFrame.Size = new System.Drawing.Size(400, 500);
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
            this.browserSplitContainer.Size = new System.Drawing.Size(396, 483);
            this.browserSplitContainer.SplitterDistance = 237;
            this.browserSplitContainer.SplitterWidth = 3;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(396, 237);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(396, 243);
            this.objectTreeView.TabIndex = 0;
            // 
            // propertiesFrame
            // 
            this.propertiesFrame.Controls.Add(this.propertiesDataGridView);
            this.propertiesFrame.Controls.Add(this.pollingPanel);
            this.propertiesFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesFrame.Location = new System.Drawing.Point(0, 0);
            this.propertiesFrame.Name = "propertiesFrame";
            this.propertiesFrame.Size = new System.Drawing.Size(414, 500);
            this.propertiesFrame.TabIndex = 0;
            this.propertiesFrame.TabStop = false;
            this.propertiesFrame.Text = "Object Properties";
            // 
            // propertiesDataGridView
            // 
            this.propertiesDataGridView.AllowUserToAddRows = false;
            this.propertiesDataGridView.AllowUserToDeleteRows = false;
            this.propertiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProperty,
            this.colValue});
            this.propertiesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesDataGridView.Location = new System.Drawing.Point(3, 45);
            this.propertiesDataGridView.Name = "propertiesDataGridView";
            this.propertiesDataGridView.Size = new System.Drawing.Size(408, 452);
            this.propertiesDataGridView.TabIndex = 1;
            // 
            // colProperty
            // 
            this.colProperty.HeaderText = "Property";
            this.colProperty.Name = "colProperty";
            this.colProperty.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            // 
            // pollingPanel
            // 
            this.pollingPanel.Controls.Add(this.writePriorityComboBox);
            this.pollingPanel.Controls.Add(this.labelWritePriority);
            this.pollingPanel.Controls.Add(this.togglePollingButton);
            this.pollingPanel.Controls.Add(this.readIntervalNumericUpDown);
            this.pollingPanel.Controls.Add(this.labelReadInterval);
            this.pollingPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pollingPanel.Location = new System.Drawing.Point(3, 16);
            this.pollingPanel.Name = "pollingPanel";
            this.pollingPanel.Size = new System.Drawing.Size(408, 29);
            this.pollingPanel.TabIndex = 0;
            // 
            // writePriorityComboBox
            // 
            this.writePriorityComboBox.FormattingEnabled = true;
            this.writePriorityComboBox.Items.AddRange(new object[] {
            "1 (Manual Life Safety)",
            "2 (Automatic Life Safety)",
            "3",
            "4",
            "5 (Critical Equipment Control)",
            "6 (Minimum On/Off)",
            "7",
            "8 (Manual Operator)",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16 (Default)"});
            this.writePriorityComboBox.Location = new System.Drawing.Point(300, 4);
            this.writePriorityComboBox.Name = "writePriorityComboBox";
            this.writePriorityComboBox.Size = new System.Drawing.Size(100, 21);
            this.writePriorityComboBox.TabIndex = 4;
            // 
            // labelWritePriority
            // 
            this.labelWritePriority.AutoSize = true;
            this.labelWritePriority.Location = new System.Drawing.Point(230, 7);
            this.labelWritePriority.Name = "labelWritePriority";
            this.labelWritePriority.Size = new System.Drawing.Size(64, 13);
            this.labelWritePriority.TabIndex = 3;
            this.labelWritePriority.Text = "Write Priority";
            // 
            // togglePollingButton
            // 
            this.togglePollingButton.Enabled = false;
            this.togglePollingButton.Location = new System.Drawing.Point(150, 3);
            this.togglePollingButton.Name = "togglePollingButton";
            this.togglePollingButton.Size = new System.Drawing.Size(75, 23);
            this.togglePollingButton.TabIndex = 2;
            this.togglePollingButton.Text = "Start Polling";
            this.togglePollingButton.UseVisualStyleBackColor = true;
            // 
            // readIntervalNumericUpDown
            // 
            this.readIntervalNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.readIntervalNumericUpDown.Location = new System.Drawing.Point(85, 5);
            this.readIntervalNumericUpDown.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.readIntervalNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.readIntervalNumericUpDown.Name = "readIntervalNumericUpDown";
            this.readIntervalNumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.readIntervalNumericUpDown.TabIndex = 1;
            this.readIntervalNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // labelReadInterval
            // 
            this.labelReadInterval.AutoSize = true;
            this.labelReadInterval.Location = new System.Drawing.Point(4, 7);
            this.labelReadInterval.Name = "labelReadInterval";
            this.labelReadInterval.Size = new System.Drawing.Size(75, 13);
            this.labelReadInterval.TabIndex = 0;
            this.labelReadInterval.Text = "Interval (ms)";
            // 
            // outputFrame
            // 
            this.outputFrame.Controls.Add(this.outputTextBox);
            this.outputFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFrame.Location = new System.Drawing.Point(0, 0);
            this.outputFrame.Margin = new System.Windows.Forms.Padding(2);
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Padding = new System.Windows.Forms.Padding(2);
            this.outputFrame.Size = new System.Drawing.Size(818, 272);
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
            this.outputTextBox.Size = new System.Drawing.Size(814, 255);
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
            this.leftPanelSplitContainer.Panel1.ResumeLayout(false);
            this.leftPanelSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftPanelSplitContainer)).EndInit();
            this.leftPanelSplitContainer.ResumeLayout(false);
            this.browserFrame.ResumeLayout(false);
            this.browserSplitContainer.Panel1.ResumeLayout(false);
            this.browserSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).EndInit();
            this.browserSplitContainer.ResumeLayout(false);
            this.propertiesFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).EndInit();
            this.pollingPanel.ResumeLayout(false);
            this.pollingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readIntervalNumericUpDown)).EndInit();
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
        private System.Windows.Forms.Button manualReadWriteButton;
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
        private System.Windows.Forms.SplitContainer leftPanelSplitContainer;
        private System.Windows.Forms.GroupBox propertiesFrame;
        private System.Windows.Forms.DataGridView propertiesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Panel pollingPanel;
        private System.Windows.Forms.Label labelReadInterval;
        private System.Windows.Forms.NumericUpDown readIntervalNumericUpDown;
        private System.Windows.Forms.Button togglePollingButton;
        private System.Windows.Forms.Label labelWritePriority;
        private System.Windows.Forms.ComboBox writePriorityComboBox;
    }
}