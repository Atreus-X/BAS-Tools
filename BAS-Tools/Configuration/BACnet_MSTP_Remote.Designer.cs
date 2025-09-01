namespace MainApp.Configuration
{
    partial class BACnet_MSTP_Remote
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
            this.remoteLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.localInterfaceComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.bbmdIpComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bbmdPortComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bbmdTtlComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.apduTimeoutComboBox = new System.Windows.Forms.ComboBox();
            this.networkNumberGroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.anyNetworkRadioButton = new System.Windows.Forms.RadioButton();
            this.localNetworkRadioButton = new System.Windows.Forms.RadioButton();
            this.listNetworkRadioButton = new System.Windows.Forms.RadioButton();
            this.networkNumberComboBox = new System.Windows.Forms.ComboBox();
            this.rediscoverPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.rediscoverDeviceCheckBox = new System.Windows.Forms.CheckBox();
            this.rediscoverObjectCheckBox = new System.Windows.Forms.CheckBox();
            this.actionsFrame = new System.Windows.Forms.GroupBox();
            this.actionsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.startDiscoveryButton = new System.Windows.Forms.Button();
            this.cancelDiscoveryButton = new System.Windows.Forms.Button();
            this.discoveryStatusLabel = new System.Windows.Forms.Label();
            this.discoverObjectsButton = new System.Windows.Forms.Button();
            this.readPropertyButton = new System.Windows.Forms.Button();
            this.writePropertyButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.cancelActionButton = new System.Windows.Forms.Button();
            this.bottomPanelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.browserFrame = new System.Windows.Forms.GroupBox();
            this.browserSplitContainer = new System.Windows.Forms.SplitContainer();
            this.deviceTreeView = new System.Windows.Forms.TreeView();
            this.objectTreeView = new System.Windows.Forms.TreeView();
            this.browserButtonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.expandAllButton = new System.Windows.Forms.Button();
            this.collapseAllButton = new System.Windows.Forms.Button();
            this.clearBrowserButton = new System.Windows.Forms.Button();
            this.objectDiscoveryProgressBar = new System.Windows.Forms.ProgressBar();
            this.objectCountLabel = new System.Windows.Forms.Label();
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
            this.remoteLayout.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.networkNumberGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.rediscoverPanel.SuspendLayout();
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
            this.browserButtonsPanel.SuspendLayout();
            this.outputFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.mainSplitContainer);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(8);
            this.mainPanel.Size = new System.Drawing.Size(834, 939);
            this.mainPanel.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSplitContainer.IsSplitterFixed = true;
            this.mainSplitContainer.Location = new System.Drawing.Point(8, 8);
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
            this.mainSplitContainer.Size = new System.Drawing.Size(818, 923);
            this.mainSplitContainer.SplitterDistance = 195;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // topPanelSplitContainer
            // 
            this.topPanelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.topPanelSplitContainer.IsSplitterFixed = true;
            this.topPanelSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.topPanelSplitContainer.Name = "topPanelSplitContainer";
            this.topPanelSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topPanelSplitContainer.Panel1
            // 
            this.topPanelSplitContainer.Panel1.Controls.Add(this.mstpFrame);
            // 
            // topPanelSplitContainer.Panel2
            // 
            this.topPanelSplitContainer.Panel2.Controls.Add(this.rediscoverPanel);
            this.topPanelSplitContainer.Panel2.Controls.Add(this.actionsFrame);
            this.topPanelSplitContainer.Size = new System.Drawing.Size(818, 195);
            this.topPanelSplitContainer.SplitterDistance = 126;
            this.topPanelSplitContainer.TabIndex = 0;
            // 
            // mstpFrame
            // 
            this.mstpFrame.AutoSize = true;
            this.mstpFrame.Controls.Add(this.remoteLayout);
            this.mstpFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.mstpFrame.Location = new System.Drawing.Point(0, 0);
            this.mstpFrame.Name = "mstpFrame";
            this.mstpFrame.Padding = new System.Windows.Forms.Padding(8);
            this.mstpFrame.Size = new System.Drawing.Size(818, 126);
            this.mstpFrame.TabIndex = 0;
            this.mstpFrame.TabStop = false;
            this.mstpFrame.Text = "BACnet MS/TP Remote (BBMD) Configuration";
            // 
            // remoteLayout
            // 
            this.remoteLayout.AutoSize = true;
            this.remoteLayout.ColumnCount = 2;
            this.remoteLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.remoteLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.remoteLayout.Controls.Add(this.label6, 0, 0);
            this.remoteLayout.Controls.Add(this.localInterfaceComboBox, 1, 0);
            this.remoteLayout.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.remoteLayout.Controls.Add(this.networkNumberGroupBox, 0, 2);
            this.remoteLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteLayout.Location = new System.Drawing.Point(8, 21);
            this.remoteLayout.Name = "remoteLayout";
            this.remoteLayout.RowCount = 3;
            this.remoteLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.remoteLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.remoteLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.remoteLayout.Size = new System.Drawing.Size(802, 97);
            this.remoteLayout.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Local Interface:";
            // 
            // localInterfaceComboBox
            // 
            this.localInterfaceComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localInterfaceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.localInterfaceComboBox.FormattingEnabled = true;
            this.localInterfaceComboBox.Location = new System.Drawing.Point(90, 3);
            this.localInterfaceComboBox.Name = "localInterfaceComboBox";
            this.localInterfaceComboBox.Size = new System.Drawing.Size(709, 21);
            this.localInterfaceComboBox.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.remoteLayout.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Controls.Add(this.bbmdIpComboBox);
            this.flowLayoutPanel2.Controls.Add(this.label8);
            this.flowLayoutPanel2.Controls.Add(this.bbmdPortComboBox);
            this.flowLayoutPanel2.Controls.Add(this.label9);
            this.flowLayoutPanel2.Controls.Add(this.bbmdTtlComboBox);
            this.flowLayoutPanel2.Controls.Add(this.label10);
            this.flowLayoutPanel2.Controls.Add(this.apduTimeoutComboBox);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(802, 25);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "BBMD IP:";
            // 
            // bbmdIpComboBox
            // 
            this.bbmdIpComboBox.FormattingEnabled = true;
            this.bbmdIpComboBox.Location = new System.Drawing.Point(63, 3);
            this.bbmdIpComboBox.Name = "bbmdIpComboBox";
            this.bbmdIpComboBox.Size = new System.Drawing.Size(135, 21);
            this.bbmdIpComboBox.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "BBMD Port:";
            // 
            // bbmdPortComboBox
            // 
            this.bbmdPortComboBox.FormattingEnabled = true;
            this.bbmdPortComboBox.Location = new System.Drawing.Point(273, 3);
            this.bbmdPortComboBox.Name = "bbmdPortComboBox";
            this.bbmdPortComboBox.Size = new System.Drawing.Size(83, 21);
            this.bbmdPortComboBox.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(362, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "BBMD TTL (s):";
            // 
            // bbmdTtlComboBox
            // 
            this.bbmdTtlComboBox.FormattingEnabled = true;
            this.bbmdTtlComboBox.Location = new System.Drawing.Point(446, 3);
            this.bbmdTtlComboBox.Name = "bbmdTtlComboBox";
            this.bbmdTtlComboBox.Size = new System.Drawing.Size(75, 21);
            this.bbmdTtlComboBox.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(527, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "APDU Timeout (ms):";
            // 
            // apduTimeoutComboBox
            // 
            this.apduTimeoutComboBox.FormattingEnabled = true;
            this.apduTimeoutComboBox.Location = new System.Drawing.Point(636, 3);
            this.apduTimeoutComboBox.Name = "apduTimeoutComboBox";
            this.apduTimeoutComboBox.Size = new System.Drawing.Size(83, 21);
            this.apduTimeoutComboBox.TabIndex = 9;
            // 
            // networkNumberGroupBox
            // 
            this.remoteLayout.SetColumnSpan(this.networkNumberGroupBox, 2);
            this.networkNumberGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.networkNumberGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.networkNumberGroupBox.Location = new System.Drawing.Point(3, 55);
            this.networkNumberGroupBox.Name = "networkNumberGroupBox";
            this.networkNumberGroupBox.Size = new System.Drawing.Size(796, 39);
            this.networkNumberGroupBox.TabIndex = 10;
            this.networkNumberGroupBox.TabStop = false;
            this.networkNumberGroupBox.Text = "Network Filter";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.anyNetworkRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.localNetworkRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.listNetworkRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.networkNumberComboBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(790, 20);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // anyNetworkRadioButton
            // 
            this.anyNetworkRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.anyNetworkRadioButton.AutoSize = true;
            this.anyNetworkRadioButton.Checked = true;
            this.anyNetworkRadioButton.Location = new System.Drawing.Point(3, 3);
            this.anyNetworkRadioButton.Name = "anyNetworkRadioButton";
            this.anyNetworkRadioButton.Size = new System.Drawing.Size(43, 17);
            this.anyNetworkRadioButton.TabIndex = 0;
            this.anyNetworkRadioButton.TabStop = true;
            this.anyNetworkRadioButton.Text = "Any";
            this.anyNetworkRadioButton.UseVisualStyleBackColor = true;
            // 
            // localNetworkRadioButton
            // 
            this.localNetworkRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.localNetworkRadioButton.AutoSize = true;
            this.localNetworkRadioButton.Location = new System.Drawing.Point(52, 3);
            this.localNetworkRadioButton.Name = "localNetworkRadioButton";
            this.localNetworkRadioButton.Size = new System.Drawing.Size(51, 17);
            this.localNetworkRadioButton.TabIndex = 1;
            this.localNetworkRadioButton.Text = "Local";
            this.localNetworkRadioButton.UseVisualStyleBackColor = true;
            // 
            // listNetworkRadioButton
            // 
            this.listNetworkRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.listNetworkRadioButton.AutoSize = true;
            this.listNetworkRadioButton.Location = new System.Drawing.Point(109, 3);
            this.listNetworkRadioButton.Name = "listNetworkRadioButton";
            this.listNetworkRadioButton.Size = new System.Drawing.Size(44, 17);
            this.listNetworkRadioButton.TabIndex = 2;
            this.listNetworkRadioButton.Text = "List:";
            this.listNetworkRadioButton.UseVisualStyleBackColor = true;
            // 
            // networkNumberComboBox
            // 
            this.networkNumberComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.networkNumberComboBox.FormattingEnabled = true;
            this.networkNumberComboBox.Location = new System.Drawing.Point(159, 3);
            this.networkNumberComboBox.Name = "networkNumberComboBox";
            this.networkNumberComboBox.Size = new System.Drawing.Size(188, 21);
            this.networkNumberComboBox.TabIndex = 3;
            this.networkNumberComboBox.Visible = false;
            // 
            // rediscoverPanel
            // 
            this.rediscoverPanel.AutoSize = true;
            this.rediscoverPanel.Controls.Add(this.rediscoverDeviceCheckBox);
            this.rediscoverPanel.Controls.Add(this.rediscoverObjectCheckBox);
            this.rediscoverPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rediscoverPanel.Location = new System.Drawing.Point(0, 46);
            this.rediscoverPanel.Name = "rediscoverPanel";
            this.rediscoverPanel.Padding = new System.Windows.Forms.Padding(2);
            this.rediscoverPanel.Size = new System.Drawing.Size(818, 23);
            this.rediscoverPanel.TabIndex = 2;
            // 
            // rediscoverDeviceCheckBox
            // 
            this.rediscoverDeviceCheckBox.AutoSize = true;
            this.rediscoverDeviceCheckBox.Location = new System.Drawing.Point(5, 5);
            this.rediscoverDeviceCheckBox.Name = "rediscoverDeviceCheckBox";
            this.rediscoverDeviceCheckBox.Size = new System.Drawing.Size(115, 17);
            this.rediscoverDeviceCheckBox.TabIndex = 0;
            this.rediscoverDeviceCheckBox.Text = "Rediscover Device";
            this.rediscoverDeviceCheckBox.UseVisualStyleBackColor = true;
            // 
            // rediscoverObjectCheckBox
            // 
            this.rediscoverObjectCheckBox.AutoSize = true;
            this.rediscoverObjectCheckBox.Location = new System.Drawing.Point(126, 5);
            this.rediscoverObjectCheckBox.Name = "rediscoverObjectCheckBox";
            this.rediscoverObjectCheckBox.Size = new System.Drawing.Size(114, 17);
            this.rediscoverObjectCheckBox.TabIndex = 1;
            this.rediscoverObjectCheckBox.Text = "Rediscover Object";
            this.rediscoverObjectCheckBox.UseVisualStyleBackColor = true;
            // 
            // actionsFrame
            // 
            this.actionsFrame.AutoSize = true;
            this.actionsFrame.Controls.Add(this.actionsLayout);
            this.actionsFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionsFrame.Location = new System.Drawing.Point(0, 0);
            this.actionsFrame.Name = "actionsFrame";
            this.actionsFrame.Size = new System.Drawing.Size(818, 44);
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
            this.actionsLayout.Controls.Add(this.discoverObjectsButton);
            this.actionsLayout.Controls.Add(this.readPropertyButton);
            this.actionsLayout.Controls.Add(this.writePropertyButton);
            this.actionsLayout.Controls.Add(this.clearLogButton);
            this.actionsLayout.Controls.Add(this.cancelActionButton);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(3, 16);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.Size = new System.Drawing.Size(812, 25);
            this.actionsLayout.TabIndex = 0;
            // 
            // startDiscoveryButton
            // 
            this.startDiscoveryButton.AutoSize = true;
            this.startDiscoveryButton.Location = new System.Drawing.Point(3, 3);
            this.startDiscoveryButton.Name = "startDiscoveryButton";
            this.startDiscoveryButton.Size = new System.Drawing.Size(59, 23);
            this.startDiscoveryButton.TabIndex = 0;
            this.startDiscoveryButton.Text = "Discover";
            this.startDiscoveryButton.UseVisualStyleBackColor = true;
            // 
            // cancelDiscoveryButton
            // 
            this.cancelDiscoveryButton.AutoSize = true;
            this.cancelDiscoveryButton.Location = new System.Drawing.Point(68, 3);
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
            this.discoveryStatusLabel.Location = new System.Drawing.Point(130, 8);
            this.discoveryStatusLabel.Name = "discoveryStatusLabel";
            this.discoveryStatusLabel.Size = new System.Drawing.Size(49, 13);
            this.discoveryStatusLabel.TabIndex = 2;
            this.discoveryStatusLabel.Text = "Found: 0";
            this.discoveryStatusLabel.Visible = false;
            // 
            // discoverObjectsButton
            // 
            this.discoverObjectsButton.AutoSize = true;
            this.discoverObjectsButton.Location = new System.Drawing.Point(185, 3);
            this.discoverObjectsButton.Name = "discoverObjectsButton";
            this.discoverObjectsButton.Size = new System.Drawing.Size(98, 23);
            this.discoverObjectsButton.TabIndex = 4;
            this.discoverObjectsButton.Text = "Discover Objects";
            this.discoverObjectsButton.UseVisualStyleBackColor = true;
            // 
            // readPropertyButton
            // 
            this.readPropertyButton.AutoSize = true;
            this.readPropertyButton.Location = new System.Drawing.Point(289, 3);
            this.readPropertyButton.Name = "readPropertyButton";
            this.readPropertyButton.Size = new System.Drawing.Size(85, 23);
            this.readPropertyButton.TabIndex = 5;
            this.readPropertyButton.Text = "Read Property";
            this.readPropertyButton.UseVisualStyleBackColor = true;
            // 
            // writePropertyButton
            // 
            this.writePropertyButton.AutoSize = true;
            this.writePropertyButton.Location = new System.Drawing.Point(380, 3);
            this.writePropertyButton.Name = "writePropertyButton";
            this.writePropertyButton.Size = new System.Drawing.Size(84, 23);
            this.writePropertyButton.TabIndex = 6;
            this.writePropertyButton.Text = "Write Property";
            this.writePropertyButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.AutoSize = true;
            this.clearLogButton.Location = new System.Drawing.Point(470, 3);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(68, 23);
            this.clearLogButton.TabIndex = 7;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // cancelActionButton
            // 
            this.cancelActionButton.AutoSize = true;
            this.cancelActionButton.Enabled = false;
            this.cancelActionButton.Location = new System.Drawing.Point(544, 3);
            this.cancelActionButton.Name = "cancelActionButton";
            this.cancelActionButton.Size = new System.Drawing.Size(75, 23);
            this.cancelActionButton.TabIndex = 8;
            this.cancelActionButton.Text = "Cancel";
            this.cancelActionButton.UseVisualStyleBackColor = true;
            // 
            // bottomPanelSplitContainer
            // 
            this.bottomPanelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanelSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomPanelSplitContainer.Name = "bottomPanelSplitContainer";
            // 
            // bottomPanelSplitContainer.Panel1
            // 
            this.bottomPanelSplitContainer.Panel1.Controls.Add(this.browserFrame);
            // 
            // bottomPanelSplitContainer.Panel2
            // 
            this.bottomPanelSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.bottomPanelSplitContainer.Size = new System.Drawing.Size(818, 695);
            this.bottomPanelSplitContainer.SplitterDistance = 409;
            // 
            // browserFrame
            // 
            this.browserFrame.Controls.Add(this.browserSplitContainer);
            this.browserFrame.Controls.Add(this.browserButtonsPanel);
            this.browserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserFrame.Location = new System.Drawing.Point(0, 0);
            this.browserFrame.Name = "browserFrame";
            this.browserFrame.Size = new System.Drawing.Size(409, 695);
            this.browserFrame.TabIndex = 0;
            this.browserFrame.TabStop = false;
            this.browserFrame.Text = "Device & Object Browser";
            // 
            // browserSplitContainer
            // 
            this.browserSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserSplitContainer.Location = new System.Drawing.Point(3, 45);
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
            this.browserSplitContainer.Size = new System.Drawing.Size(403, 647);
            this.browserSplitContainer.SplitterDistance = 317;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(403, 317);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(403, 326);
            this.objectTreeView.TabIndex = 0;
            // 
            // browserButtonsPanel
            // 
            this.browserButtonsPanel.AutoSize = true;
            this.browserButtonsPanel.ColumnCount = 5;
            this.browserButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.browserButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.browserButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.browserButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.browserButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.browserButtonsPanel.Controls.Add(this.expandAllButton, 0, 0);
            this.browserButtonsPanel.Controls.Add(this.collapseAllButton, 1, 0);
            this.browserButtonsPanel.Controls.Add(this.clearBrowserButton, 2, 0);
            this.browserButtonsPanel.Controls.Add(this.objectDiscoveryProgressBar, 4, 0);
            this.browserButtonsPanel.Controls.Add(this.objectCountLabel, 3, 0);
            this.browserButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.browserButtonsPanel.Location = new System.Drawing.Point(3, 16);
            this.browserButtonsPanel.Name = "browserButtonsPanel";
            this.browserButtonsPanel.RowCount = 1;
            this.browserButtonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.browserButtonsPanel.Size = new System.Drawing.Size(403, 29);
            this.browserButtonsPanel.TabIndex = 1;
            // 
            // expandAllButton
            // 
            this.expandAllButton.Location = new System.Drawing.Point(3, 3);
            this.expandAllButton.Name = "expandAllButton";
            this.expandAllButton.Size = new System.Drawing.Size(75, 23);
            this.expandAllButton.TabIndex = 0;
            this.expandAllButton.Text = "Expand All";
            this.expandAllButton.UseVisualStyleBackColor = true;
            // 
            // collapseAllButton
            // 
            this.collapseAllButton.Location = new System.Drawing.Point(84, 3);
            this.collapseAllButton.Name = "collapseAllButton";
            this.collapseAllButton.Size = new System.Drawing.Size(75, 23);
            this.collapseAllButton.TabIndex = 1;
            this.collapseAllButton.Text = "Collapse All";
            this.collapseAllButton.UseVisualStyleBackColor = true;
            // 
            // clearBrowserButton
            // 
            this.clearBrowserButton.Location = new System.Drawing.Point(165, 3);
            this.clearBrowserButton.Name = "clearBrowserButton";
            this.clearBrowserButton.Size = new System.Drawing.Size(75, 23);
            this.clearBrowserButton.TabIndex = 2;
            this.clearBrowserButton.Text = "Clear";
            this.clearBrowserButton.UseVisualStyleBackColor = true;
            // 
            // objectDiscoveryProgressBar
            // 
            this.objectDiscoveryProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.objectDiscoveryProgressBar.Location = new System.Drawing.Point(247, 3);
            this.objectDiscoveryProgressBar.Name = "objectDiscoveryProgressBar";
            this.objectDiscoveryProgressBar.Size = new System.Drawing.Size(153, 23);
            this.objectDiscoveryProgressBar.TabIndex = 9;
            this.objectDiscoveryProgressBar.Visible = false;
            // 
            // objectCountLabel
            // 
            this.objectCountLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.objectCountLabel.AutoSize = true;
            this.objectCountLabel.Location = new System.Drawing.Point(173, 8);
            this.objectCountLabel.Name = "objectCountLabel";
            this.objectCountLabel.Size = new System.Drawing.Size(68, 13);
            this.objectCountLabel.TabIndex = 8;
            this.objectCountLabel.Text = "Found 0 of 0";
            this.objectCountLabel.Visible = false;
            // 
            // outputFrame
            // 
            this.outputFrame.Controls.Add(this.outputTextBox);
            this.outputFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFrame.Location = new System.Drawing.Point(0, 0);
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Size = new System.Drawing.Size(406, 695);
            this.outputFrame.TabIndex = 0;
            this.outputFrame.TabStop = false;
            this.outputFrame.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(3, 16);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(400, 676);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // BACnet_MSTP_Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "BACnet_MSTP_Remote";
            this.Size = new System.Drawing.Size(834, 939);
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
            this.remoteLayout.ResumeLayout(false);
            this.remoteLayout.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.networkNumberGroupBox.ResumeLayout(false);
            this.networkNumberGroupBox.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.actionsFrame.ResumeLayout(false);
            this.actionsFrame.PerformLayout();
            this.actionsLayout.ResumeLayout(false);
            this.actionsLayout.PerformLayout();
            this.rediscoverPanel.ResumeLayout(false);
            this.rediscoverPanel.PerformLayout();
            this.bottomPanelSplitContainer.Panel1.ResumeLayout(false);
            this.bottomPanelSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomPanelSplitContainer)).EndInit();
            this.bottomPanelSplitContainer.ResumeLayout(false);
            this.browserFrame.ResumeLayout(false);
            this.browserFrame.PerformLayout();
            this.browserSplitContainer.Panel1.ResumeLayout(false);
            this.browserSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).EndInit();
            this.browserSplitContainer.ResumeLayout(false);
            this.browserButtonsPanel.ResumeLayout(false);
            this.browserButtonsPanel.PerformLayout();
            this.outputFrame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox mstpFrame;
        private System.Windows.Forms.GroupBox actionsFrame;
        private System.Windows.Forms.FlowLayoutPanel actionsLayout;
        private System.Windows.Forms.Button startDiscoveryButton;
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
        private System.Windows.Forms.TableLayoutPanel remoteLayout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox localInterfaceComboBox;
        private System.Windows.Forms.ComboBox bbmdIpComboBox;
        private System.Windows.Forms.ComboBox bbmdPortComboBox;
        private System.Windows.Forms.ComboBox bbmdTtlComboBox;
        private System.Windows.Forms.ComboBox apduTimeoutComboBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox networkNumberGroupBox;
        private System.Windows.Forms.RadioButton anyNetworkRadioButton;
        private System.Windows.Forms.RadioButton localNetworkRadioButton;
        private System.Windows.Forms.RadioButton listNetworkRadioButton;
        private System.Windows.Forms.ComboBox networkNumberComboBox;
        private System.Windows.Forms.SplitContainer topPanelSplitContainer;
        private System.Windows.Forms.SplitContainer bottomPanelSplitContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel browserButtonsPanel;
        private System.Windows.Forms.Button expandAllButton;
        private System.Windows.Forms.Button collapseAllButton;
        private System.Windows.Forms.Button clearBrowserButton;
        private System.Windows.Forms.Label objectCountLabel;
        private System.Windows.Forms.ProgressBar objectDiscoveryProgressBar;
        private System.Windows.Forms.FlowLayoutPanel rediscoverPanel;
        private System.Windows.Forms.CheckBox rediscoverDeviceCheckBox;
        private System.Windows.Forms.CheckBox rediscoverObjectCheckBox;
        private System.Windows.Forms.Button cancelActionButton;
    }
}