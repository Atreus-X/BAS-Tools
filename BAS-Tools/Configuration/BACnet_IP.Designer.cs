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
            this.leftPanelSplitContainer = new System.Windows.Forms.SplitContainer();
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
            this.actionsFrame = new System.Windows.Forms.GroupBox();
            this.actionsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.discoverButton = new System.Windows.Forms.Button();
            this.pingButton = new System.Windows.Forms.Button();
            this.discoverObjectsButton = new System.Windows.Forms.Button();
            this.manualReadWriteButton = new System.Windows.Forms.Button();
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
            this.labelNetworkNumber = new System.Windows.Forms.Label();
            this.networkNumberComboBox = new System.Windows.Forms.ComboBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftPanelSplitContainer)).BeginInit();
            this.leftPanelSplitContainer.Panel1.SuspendLayout();
            this.leftPanelSplitContainer.Panel2.SuspendLayout();
            this.leftPanelSplitContainer.SuspendLayout();
            this.browserFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).BeginInit();
            this.browserSplitContainer.Panel1.SuspendLayout();
            this.browserSplitContainer.Panel2.SuspendLayout();
            this.browserSplitContainer.SuspendLayout();
            this.browserButtonsPanel.SuspendLayout();
            this.propertiesFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).BeginInit();
            this.pollingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readIntervalNumericUpDown)).BeginInit();
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
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(8);
            this.mainPanel.Size = new System.Drawing.Size(834, 969);
            this.mainPanel.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(8, 224);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftPanelSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.mainSplitContainer.Size = new System.Drawing.Size(818, 737);
            this.mainSplitContainer.SplitterDistance = 470;
            this.mainSplitContainer.TabIndex = 2;
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
            this.leftPanelSplitContainer.Size = new System.Drawing.Size(818, 470);
            this.leftPanelSplitContainer.SplitterDistance = 400;
            this.leftPanelSplitContainer.TabIndex = 3;
            // 
            // browserFrame
            // 
            this.browserFrame.Controls.Add(this.browserSplitContainer);
            this.browserFrame.Controls.Add(this.browserButtonsPanel);
            this.browserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserFrame.Location = new System.Drawing.Point(0, 0);
            this.browserFrame.Name = "browserFrame";
            this.browserFrame.Size = new System.Drawing.Size(400, 470);
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
            this.browserSplitContainer.Size = new System.Drawing.Size(394, 422);
            this.browserSplitContainer.SplitterDistance = 210;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.HideSelection = false;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(394, 210);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.HideSelection = false;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(394, 208);
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
            this.browserButtonsPanel.Size = new System.Drawing.Size(394, 29);
            this.browserButtonsPanel.TabIndex = 2;
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
            this.objectDiscoveryProgressBar.Location = new System.Drawing.Point(246, 3);
            this.objectDiscoveryProgressBar.Name = "objectDiscoveryProgressBar";
            this.objectDiscoveryProgressBar.Size = new System.Drawing.Size(145, 23);
            this.objectDiscoveryProgressBar.TabIndex = 9;
            this.objectDiscoveryProgressBar.Visible = false;
            // 
            // objectCountLabel
            // 
            this.objectCountLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.objectCountLabel.AutoSize = true;
            this.objectCountLabel.Location = new System.Drawing.Point(172, 8);
            this.objectCountLabel.Name = "objectCountLabel";
            this.objectCountLabel.Size = new System.Drawing.Size(68, 13);
            this.objectCountLabel.TabIndex = 8;
            this.objectCountLabel.Text = "Found 0 of 0";
            this.objectCountLabel.Visible = false;
            // 
            // propertiesFrame
            // 
            this.propertiesFrame.Controls.Add(this.propertiesDataGridView);
            this.propertiesFrame.Controls.Add(this.pollingPanel);
            this.propertiesFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesFrame.Location = new System.Drawing.Point(0, 0);
            this.propertiesFrame.Name = "propertiesFrame";
            this.propertiesFrame.Size = new System.Drawing.Size(414, 470);
            this.propertiesFrame.TabIndex = 1;
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
            this.propertiesDataGridView.Size = new System.Drawing.Size(408, 422);
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
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Padding = new System.Windows.Forms.Padding(8);
            this.outputFrame.Size = new System.Drawing.Size(818, 263);
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
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(802, 234);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // actionsFrame
            // 
            this.actionsFrame.AutoSize = true;
            this.actionsFrame.Controls.Add(this.actionsLayout);
            this.actionsFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionsFrame.Location = new System.Drawing.Point(8, 138);
            this.actionsFrame.Name = "actionsFrame";
            this.actionsFrame.Padding = new System.Windows.Forms.Padding(8);
            this.actionsFrame.Size = new System.Drawing.Size(818, 57);
            this.actionsFrame.TabIndex = 1;
            this.actionsFrame.TabStop = false;
            this.actionsFrame.Text = "Actions";
            // 
            // actionsLayout
            // 
            this.actionsLayout.AutoSize = true;
            this.actionsLayout.Controls.Add(this.discoverButton);
            this.actionsLayout.Controls.Add(this.pingButton);
            this.actionsLayout.Controls.Add(this.discoverObjectsButton);
            this.actionsLayout.Controls.Add(this.manualReadWriteButton);
            this.actionsLayout.Controls.Add(this.clearLogButton);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(8, 21);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.Size = new System.Drawing.Size(802, 28);
            this.actionsLayout.TabIndex = 0;
            // 
            // discoverButton
            // 
            this.discoverButton.AutoSize = true;
            this.discoverButton.Location = new System.Drawing.Point(3, 3);
            this.discoverButton.Name = "discoverButton";
            this.discoverButton.Size = new System.Drawing.Size(90, 23);
            this.discoverButton.TabIndex = 0;
            this.discoverButton.Text = "Discover Devices";
            this.discoverButton.UseVisualStyleBackColor = true;
            // 
            // pingButton
            // 
            this.pingButton.AutoSize = true;
            this.pingButton.Location = new System.Drawing.Point(99, 3);
            this.pingButton.Name = "pingButton";
            this.pingButton.Size = new System.Drawing.Size(90, 23);
            this.pingButton.TabIndex = 1;
            this.pingButton.Text = "Ping Device";
            this.pingButton.UseVisualStyleBackColor = true;
            // 
            // discoverObjectsButton
            // 
            this.discoverObjectsButton.AutoSize = true;
            this.discoverObjectsButton.Location = new System.Drawing.Point(195, 3);
            this.discoverObjectsButton.Name = "discoverObjectsButton";
            this.discoverObjectsButton.Size = new System.Drawing.Size(90, 23);
            this.discoverObjectsButton.TabIndex = 2;
            this.discoverObjectsButton.Text = "Discover Objects";
            this.discoverObjectsButton.UseVisualStyleBackColor = true;
            // 
            // manualReadWriteButton
            // 
            this.manualReadWriteButton.AutoSize = true;
            this.manualReadWriteButton.Location = new System.Drawing.Point(291, 3);
            this.manualReadWriteButton.Name = "manualReadWriteButton";
            this.manualReadWriteButton.Size = new System.Drawing.Size(90, 23);
            this.manualReadWriteButton.TabIndex = 3;
            this.manualReadWriteButton.Text = "Manual Read/Write";
            this.manualReadWriteButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.AutoSize = true;
            this.clearLogButton.Location = new System.Drawing.Point(387, 3);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(90, 23);
            this.clearLogButton.TabIndex = 5;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // ipFrame
            // 
            this.ipFrame.AutoSize = true;
            this.ipFrame.Controls.Add(this.ipLayout);
            this.ipFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipFrame.Location = new System.Drawing.Point(8, 8);
            this.ipFrame.Name = "ipFrame";
            this.ipFrame.Padding = new System.Windows.Forms.Padding(8);
            this.ipFrame.Size = new System.Drawing.Size(818, 130);
            this.ipFrame.TabIndex = 0;
            this.ipFrame.TabStop = false;
            this.ipFrame.Text = "BACnet/IP Configuration";
            // 
            // ipLayout
            // 
            this.ipLayout.AutoSize = true;
            this.ipLayout.ColumnCount = 4;
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
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
            this.ipLayout.Controls.Add(this.labelNetworkNumber, 0, 4);
            this.ipLayout.Controls.Add(this.networkNumberComboBox, 1, 4);
            this.ipLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipLayout.Location = new System.Drawing.Point(8, 21);
            this.ipLayout.Name = "ipLayout";
            this.ipLayout.RowCount = 5;
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ipLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ipLayout.Size = new System.Drawing.Size(802, 101);
            this.ipLayout.TabIndex = 0;
            // 
            // labelTargetIP
            // 
            this.labelTargetIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTargetIP.AutoSize = true;
            this.labelTargetIP.Location = new System.Drawing.Point(3, 7);
            this.labelTargetIP.Name = "labelTargetIP";
            this.labelTargetIP.Size = new System.Drawing.Size(78, 13);
            this.labelTargetIP.TabIndex = 0;
            this.labelTargetIP.Text = "Target Device IP:";
            // 
            // ipAddressComboBox
            // 
            this.ipAddressComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipAddressComboBox.FormattingEnabled = true;
            this.ipAddressComboBox.Location = new System.Drawing.Point(87, 3);
            this.ipAddressComboBox.Name = "ipAddressComboBox";
            this.ipAddressComboBox.Size = new System.Drawing.Size(320, 21);
            this.ipAddressComboBox.TabIndex = 1;
            // 
            // labelInstance
            // 
            this.labelInstance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelInstance.AutoSize = true;
            this.labelInstance.Location = new System.Drawing.Point(413, 7);
            this.labelInstance.Name = "labelInstance";
            this.labelInstance.Size = new System.Drawing.Size(85, 13);
            this.labelInstance.TabIndex = 2;
            this.labelInstance.Text = "Target Instance #:";
            // 
            // instanceNumberComboBox
            // 
            this.instanceNumberComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instanceNumberComboBox.FormattingEnabled = true;
            this.instanceNumberComboBox.Location = new System.Drawing.Point(504, 3);
            this.instanceNumberComboBox.Name = "instanceNumberComboBox";
            this.instanceNumberComboBox.Size = new System.Drawing.Size(295, 21);
            this.instanceNumberComboBox.TabIndex = 3;
            // 
            // labelLocalInterface
            // 
            this.labelLocalInterface.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLocalInterface.AutoSize = true;
            this.labelLocalInterface.Location = new System.Drawing.Point(3, 32);
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
            this.interfaceComboBox.Location = new System.Drawing.Point(87, 30);
            this.interfaceComboBox.Name = "interfaceComboBox";
            this.interfaceComboBox.Size = new System.Drawing.Size(712, 21);
            this.interfaceComboBox.TabIndex = 5;
            // 
            // labelBbmdIp
            // 
            this.labelBbmdIp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBbmdIp.AutoSize = true;
            this.labelBbmdIp.Location = new System.Drawing.Point(3, 57);
            this.labelBbmdIp.Name = "labelBbmdIp";
            this.labelBbmdIp.Size = new System.Drawing.Size(54, 13);
            this.labelBbmdIp.TabIndex = 6;
            this.labelBbmdIp.Text = "BBMD IP:";
            // 
            // bbmdIpComboBox
            // 
            this.bbmdIpComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bbmdIpComboBox.FormattingEnabled = true;
            this.bbmdIpComboBox.Location = new System.Drawing.Point(87, 57);
            this.bbmdIpComboBox.Name = "bbmdIpComboBox";
            this.bbmdIpComboBox.Size = new System.Drawing.Size(320, 21);
            this.bbmdIpComboBox.TabIndex = 7;
            // 
            // labelUdpPort
            // 
            this.labelUdpPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUdpPort.AutoSize = true;
            this.labelUdpPort.Location = new System.Drawing.Point(413, 57);
            this.labelUdpPort.Name = "labelUdpPort";
            this.labelUdpPort.Size = new System.Drawing.Size(84, 13);
            this.labelUdpPort.TabIndex = 8;
            this.labelUdpPort.Text = "Local UDP Port:";
            // 
            // ipPortComboBox
            // 
            this.ipPortComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipPortComboBox.FormattingEnabled = true;
            this.ipPortComboBox.Location = new System.Drawing.Point(504, 57);
            this.ipPortComboBox.Name = "ipPortComboBox";
            this.ipPortComboBox.Size = new System.Drawing.Size(295, 21);
            this.ipPortComboBox.TabIndex = 9;
            // 
            // labelApduTimeout
            // 
            this.labelApduTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelApduTimeout.AutoSize = true;
            this.labelApduTimeout.Location = new System.Drawing.Point(3, 82);
            this.labelApduTimeout.Name = "labelApduTimeout";
            this.labelApduTimeout.Size = new System.Drawing.Size(81, 13);
            this.labelApduTimeout.TabIndex = 10;
            this.labelApduTimeout.Text = "APDU Timeout (ms):";
            // 
            // apduTimeoutComboBox
            // 
            this.apduTimeoutComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apduTimeoutComboBox.FormattingEnabled = true;
            this.apduTimeoutComboBox.Location = new System.Drawing.Point(87, 84);
            this.apduTimeoutComboBox.Name = "apduTimeoutComboBox";
            this.apduTimeoutComboBox.Size = new System.Drawing.Size(320, 21);
            this.apduTimeoutComboBox.TabIndex = 11;
            // 
            // labelBbmdTtl
            // 
            this.labelBbmdTtl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBbmdTtl.AutoSize = true;
            this.labelBbmdTtl.Location = new System.Drawing.Point(413, 82);
            this.labelBbmdTtl.Name = "labelBbmdTtl";
            this.labelBbmdTtl.Size = new System.Drawing.Size(78, 13);
            this.labelBbmdTtl.TabIndex = 12;
            this.labelBbmdTtl.Text = "BBMD TTL (s):";
            // 
            // bbmdTtlComboBox
            // 
            this.bbmdTtlComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bbmdTtlComboBox.FormattingEnabled = true;
            this.bbmdTtlComboBox.Location = new System.Drawing.Point(504, 84);
            this.bbmdTtlComboBox.Name = "bbmdTtlComboBox";
            this.bbmdTtlComboBox.Size = new System.Drawing.Size(295, 21);
            this.bbmdTtlComboBox.TabIndex = 13;
            // 
            // labelNetworkNumber
            // 
            this.labelNetworkNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelNetworkNumber.AutoSize = true;
            this.labelNetworkNumber.Location = new System.Drawing.Point(3, 107);
            this.labelNetworkNumber.Name = "labelNetworkNumber";
            this.labelNetworkNumber.Size = new System.Drawing.Size(62, 13);
            this.labelNetworkNumber.TabIndex = 14;
            this.labelNetworkNumber.Text = "Network #:";
            // 
            // networkNumberComboBox
            // 
            this.networkNumberComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.networkNumberComboBox.FormattingEnabled = true;
            this.networkNumberComboBox.Location = new System.Drawing.Point(87, 108);
            this.networkNumberComboBox.Name = "networkNumberComboBox";
            this.networkNumberComboBox.Size = new System.Drawing.Size(320, 21);
            this.networkNumberComboBox.TabIndex = 15;
            // 
            // BACnet_IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "BACnet_IP";
            this.Size = new System.Drawing.Size(834, 969);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftPanelSplitContainer.Panel1.ResumeLayout(false);
            this.leftPanelSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftPanelSplitContainer)).EndInit();
            this.leftPanelSplitContainer.ResumeLayout(false);
            this.browserFrame.ResumeLayout(false);
            this.browserFrame.PerformLayout();
            this.browserSplitContainer.Panel1.ResumeLayout(false);
            this.browserSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserSplitContainer)).EndInit();
            this.browserSplitContainer.ResumeLayout(false);
            this.browserButtonsPanel.ResumeLayout(false);
            this.browserButtonsPanel.PerformLayout();
            this.propertiesFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).EndInit();
            this.pollingPanel.ResumeLayout(false);
            this.pollingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readIntervalNumericUpDown)).EndInit();
            this.outputFrame.ResumeLayout(false);
            this.actionsFrame.ResumeLayout(false);
            this.actionsFrame.PerformLayout();
            this.actionsLayout.ResumeLayout(false);
            this.actionsLayout.PerformLayout();
            this.ipFrame.ResumeLayout(false);
            this.ipFrame.PerformLayout();
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
        private System.Windows.Forms.Button manualReadWriteButton;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.GroupBox browserFrame;
        private System.Windows.Forms.SplitContainer browserSplitContainer;
        private System.Windows.Forms.TreeView deviceTreeView;
        private System.Windows.Forms.TreeView objectTreeView;
        private System.Windows.Forms.GroupBox outputFrame;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.TableLayoutPanel browserButtonsPanel;
        private System.Windows.Forms.Button expandAllButton;
        private System.Windows.Forms.Button collapseAllButton;
        private System.Windows.Forms.Button clearBrowserButton;
        private System.Windows.Forms.ProgressBar objectDiscoveryProgressBar;
        private System.Windows.Forms.Label objectCountLabel;
        private System.Windows.Forms.Label labelNetworkNumber;
        private System.Windows.Forms.ComboBox networkNumberComboBox;
        private System.Windows.Forms.SplitContainer leftPanelSplitContainer;
        private System.Windows.Forms.GroupBox propertiesFrame;
        private System.Windows.Forms.Panel pollingPanel;
        private System.Windows.Forms.Label labelReadInterval;
        private System.Windows.Forms.NumericUpDown readIntervalNumericUpDown;
        private System.Windows.Forms.Button togglePollingButton;
        private System.Windows.Forms.DataGridView propertiesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Label labelWritePriority;
        private System.Windows.Forms.ComboBox writePriorityComboBox;
    }
}