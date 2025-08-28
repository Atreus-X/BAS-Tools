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
            this.remoteLayout.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.networkNumberGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.mainPanel.Size = new System.Drawing.Size(834, 939);
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
            this.mainSplitContainer.Size = new System.Drawing.Size(818, 923);
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
            this.topPanelSplitContainer.Size = new System.Drawing.Size(818, 397);
            this.topPanelSplitContainer.SplitterDistance = 323;
            this.topPanelSplitContainer.SplitterWidth = 3;
            this.topPanelSplitContainer.TabIndex = 0;
            // 
            // mstpFrame
            // 
            this.mstpFrame.AutoSize = true;
            this.mstpFrame.Controls.Add(this.remoteLayout);
            this.mstpFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.mstpFrame.Location = new System.Drawing.Point(0, 0);
            this.mstpFrame.Margin = new System.Windows.Forms.Padding(2);
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
            this.remoteLayout.Margin = new System.Windows.Forms.Padding(2);
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
            this.label6.Location = new System.Drawing.Point(2, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.localInterfaceComboBox.Location = new System.Drawing.Point(87, 2);
            this.localInterfaceComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.localInterfaceComboBox.Name = "localInterfaceComboBox";
            this.localInterfaceComboBox.Size = new System.Drawing.Size(713, 21);
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(802, 25);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "BBMD IP:";
            // 
            // bbmdIpComboBox
            // 
            this.bbmdIpComboBox.FormattingEnabled = true;
            this.bbmdIpComboBox.Location = new System.Drawing.Point(60, 2);
            this.bbmdIpComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.bbmdIpComboBox.Name = "bbmdIpComboBox";
            this.bbmdIpComboBox.Size = new System.Drawing.Size(92, 21);
            this.bbmdIpComboBox.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(156, 6);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "BBMD Port:";
            // 
            // bbmdPortComboBox
            // 
            this.bbmdPortComboBox.FormattingEnabled = true;
            this.bbmdPortComboBox.Location = new System.Drawing.Point(223, 2);
            this.bbmdPortComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.bbmdPortComboBox.Name = "bbmdPortComboBox";
            this.bbmdPortComboBox.Size = new System.Drawing.Size(54, 21);
            this.bbmdPortComboBox.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "BBMD TTL (s):";
            // 
            // bbmdTtlComboBox
            // 
            this.bbmdTtlComboBox.FormattingEnabled = true;
            this.bbmdTtlComboBox.Location = new System.Drawing.Point(363, 2);
            this.bbmdTtlComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.bbmdTtlComboBox.Name = "bbmdTtlComboBox";
            this.bbmdTtlComboBox.Size = new System.Drawing.Size(38, 21);
            this.bbmdTtlComboBox.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(405, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "APDU Timeout (ms):";
            // 
            // apduTimeoutComboBox
            // 
            this.apduTimeoutComboBox.FormattingEnabled = true;
            this.apduTimeoutComboBox.Location = new System.Drawing.Point(512, 2);
            this.apduTimeoutComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.apduTimeoutComboBox.Name = "apduTimeoutComboBox";
            this.apduTimeoutComboBox.Size = new System.Drawing.Size(54, 21);
            this.apduTimeoutComboBox.TabIndex = 9;
            // 
            // networkNumberGroupBox
            // 
            this.remoteLayout.SetColumnSpan(this.networkNumberGroupBox, 2);
            this.networkNumberGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.networkNumberGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.networkNumberGroupBox.Location = new System.Drawing.Point(2, 52);
            this.networkNumberGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.networkNumberGroupBox.Name = "networkNumberGroupBox";
            this.networkNumberGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.networkNumberGroupBox.Size = new System.Drawing.Size(798, 43);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 15);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(794, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // anyNetworkRadioButton
            // 
            this.anyNetworkRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.anyNetworkRadioButton.AutoSize = true;
            this.anyNetworkRadioButton.Checked = true;
            this.anyNetworkRadioButton.Location = new System.Drawing.Point(2, 4);
            this.anyNetworkRadioButton.Margin = new System.Windows.Forms.Padding(2);
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
            this.localNetworkRadioButton.Location = new System.Drawing.Point(49, 4);
            this.localNetworkRadioButton.Margin = new System.Windows.Forms.Padding(2);
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
            this.listNetworkRadioButton.Location = new System.Drawing.Point(104, 4);
            this.listNetworkRadioButton.Margin = new System.Windows.Forms.Padding(2);
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
            this.networkNumberComboBox.Location = new System.Drawing.Point(152, 2);
            this.networkNumberComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.networkNumberComboBox.Name = "networkNumberComboBox";
            this.networkNumberComboBox.Size = new System.Drawing.Size(188, 21);
            this.networkNumberComboBox.TabIndex = 3;
            this.networkNumberComboBox.Visible = false;
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
            this.actionsFrame.Size = new System.Drawing.Size(818, 71);
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
            this.actionsLayout.Size = new System.Drawing.Size(814, 54);
            this.actionsLayout.TabIndex = 0;
            // 
            // startDiscoveryButton
            // 
            this.startDiscoveryButton.AutoSize = true;
            this.startDiscoveryButton.Location = new System.Drawing.Point(2, 2);
            this.startDiscoveryButton.Margin = new System.Windows.Forms.Padding(2);
            this.startDiscoveryButton.Name = "startDiscoveryButton";
            this.startDiscoveryButton.Size = new System.Drawing.Size(56, 23);
            this.startDiscoveryButton.TabIndex = 0;
            this.startDiscoveryButton.Text = "Discover";
            this.startDiscoveryButton.UseVisualStyleBackColor = true;
            // 
            // cancelDiscoveryButton
            // 
            this.cancelDiscoveryButton.AutoSize = true;
            this.cancelDiscoveryButton.Location = new System.Drawing.Point(62, 2);
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
            this.discoveryStatusLabel.Location = new System.Drawing.Point(122, 7);
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
            this.pingButton.Location = new System.Drawing.Point(175, 2);
            this.pingButton.Margin = new System.Windows.Forms.Padding(2);
            this.pingButton.Name = "pingButton";
            this.pingButton.Size = new System.Drawing.Size(71, 23);
            this.pingButton.TabIndex = 3;
            this.pingButton.Text = "Ping Device";
            this.pingButton.UseVisualStyleBackColor = true;
            // 
            // discoverObjectsButton
            // 
            this.discoverObjectsButton.AutoSize = true;
            this.discoverObjectsButton.Location = new System.Drawing.Point(250, 2);
            this.discoverObjectsButton.Margin = new System.Windows.Forms.Padding(2);
            this.discoverObjectsButton.Name = "discoverObjectsButton";
            this.discoverObjectsButton.Size = new System.Drawing.Size(90, 23);
            this.discoverObjectsButton.TabIndex = 4;
            this.discoverObjectsButton.Text = "Discover Objects";
            this.discoverObjectsButton.UseVisualStyleBackColor = true;
            // 
            // readPropertyButton
            // 
            this.readPropertyButton.AutoSize = true;
            this.readPropertyButton.Location = new System.Drawing.Point(344, 2);
            this.readPropertyButton.Margin = new System.Windows.Forms.Padding(2);
            this.readPropertyButton.Name = "readPropertyButton";
            this.readPropertyButton.Size = new System.Drawing.Size(82, 23);
            this.readPropertyButton.TabIndex = 5;
            this.readPropertyButton.Text = "Read Property";
            this.readPropertyButton.UseVisualStyleBackColor = true;
            // 
            // writePropertyButton
            // 
            this.writePropertyButton.AutoSize = true;
            this.writePropertyButton.Location = new System.Drawing.Point(430, 2);
            this.writePropertyButton.Margin = new System.Windows.Forms.Padding(2);
            this.writePropertyButton.Name = "writePropertyButton";
            this.writePropertyButton.Size = new System.Drawing.Size(82, 23);
            this.writePropertyButton.TabIndex = 6;
            this.writePropertyButton.Text = "Write Property";
            this.writePropertyButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.AutoSize = true;
            this.clearLogButton.Location = new System.Drawing.Point(516, 2);
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
            this.bottomPanelSplitContainer.Size = new System.Drawing.Size(818, 523);
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
            this.browserFrame.Size = new System.Drawing.Size(409, 523);
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
            this.browserSplitContainer.Size = new System.Drawing.Size(405, 506);
            this.browserSplitContainer.SplitterDistance = 248;
            this.browserSplitContainer.SplitterWidth = 3;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(405, 248);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(405, 255);
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
            this.outputFrame.Size = new System.Drawing.Size(406, 523);
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
            this.outputTextBox.Size = new System.Drawing.Size(402, 506);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // BACnet_MSTP_Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.ComboBox instanceNumberComboBox;
        private System.Windows.Forms.Label label2;
    }
}