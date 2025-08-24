namespace MainApp
{
    partial class BACnet_IP
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            var mainPanel = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            this.Controls.Add(mainPanel);

            var ipFrame = new System.Windows.Forms.GroupBox { Text = "BACnet/IP Configuration", Dock = System.Windows.Forms.DockStyle.Top, Height = 120, Padding = new System.Windows.Forms.Padding(10) };
            mainPanel.Controls.Add(ipFrame);

            var actionsFrame = new System.Windows.Forms.GroupBox { Text = "Actions", Dock = System.Windows.Forms.DockStyle.Top, Height = 70, Padding = new System.Windows.Forms.Padding(10) };
            mainPanel.Controls.Add(actionsFrame);

            var mainSplitContainer = new System.Windows.Forms.SplitContainer { Dock = System.Windows.Forms.DockStyle.Fill, Orientation = System.Windows.Forms.Orientation.Vertical, SplitterDistance = 450 };
            mainPanel.Controls.Add(mainSplitContainer);

            var browserFrame = new System.Windows.Forms.GroupBox { Text = "Device & Object Browser", Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            mainSplitContainer.Panel1.Controls.Add(browserFrame);

            var outputFrame = new System.Windows.Forms.GroupBox { Text = "Output", Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            mainSplitContainer.Panel2.Controls.Add(outputFrame);

            var browserSplitContainer = new System.Windows.Forms.SplitContainer { Dock = System.Windows.Forms.DockStyle.Fill, Orientation = System.Windows.Forms.Orientation.Horizontal, SplitterDistance = 250 };
            browserFrame.Controls.Add(browserSplitContainer);

            this.deviceTreeView = new System.Windows.Forms.TreeView { Dock = System.Windows.Forms.DockStyle.Fill, HideSelection = false };
            browserSplitContainer.Panel1.Controls.Add(this.deviceTreeView);

            this.objectTreeView = new System.Windows.Forms.TreeView { Dock = System.Windows.Forms.DockStyle.Fill, HideSelection = false };
            browserSplitContainer.Panel2.Controls.Add(this.objectTreeView);

            this.outputTextBox = new System.Windows.Forms.RichTextBox { Dock = System.Windows.Forms.DockStyle.Fill, ReadOnly = true, Font = new System.Drawing.Font("Consolas", 9.75f), BackColor = System.Drawing.Color.WhiteSmoke };
            outputFrame.Controls.Add(this.outputTextBox);

            var ipLayout = new System.Windows.Forms.TableLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, ColumnCount = 4, RowCount = 3 };
            ipFrame.Controls.Add(ipLayout);
            ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            ipLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            ipLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Target Device IP:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, 0);
            this.ipAddressComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            ipLayout.Controls.Add(this.ipAddressComboBox, 1, 0);

            ipLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Target Instance #:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, 0);
            this.instanceNumberComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            ipLayout.Controls.Add(this.instanceNumberComboBox, 3, 0);

            ipLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Local Interface:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, 1);
            this.interfaceComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            ipLayout.SetColumnSpan(this.interfaceComboBox, 3);
            ipLayout.Controls.Add(this.interfaceComboBox, 1, 1);

            ipLayout.Controls.Add(new System.Windows.Forms.Label { Text = "BBMD IP:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, 2);
            this.bbmdIpComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            ipLayout.Controls.Add(this.bbmdIpComboBox, 1, 2);

            ipLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Local UDP Port:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, 2);
            this.ipPortComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            ipLayout.Controls.Add(this.ipPortComboBox, 3, 2);

            var ipLayout2 = new System.Windows.Forms.TableLayoutPanel { Dock = System.Windows.Forms.DockStyle.Bottom, ColumnCount = 4, RowCount = 1, Height = 30 };
            ipFrame.Controls.Add(ipLayout2);
            ipLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            ipLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            ipLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            ipLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            ipLayout2.Controls.Add(new System.Windows.Forms.Label { Text = "APDU Timeout (ms):", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, 0);
            this.apduTimeoutComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            ipLayout2.Controls.Add(this.apduTimeoutComboBox, 1, 0);

            ipLayout2.Controls.Add(new System.Windows.Forms.Label { Text = "BBMD TTL (s):", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, 0);
            this.bbmdTtlComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            ipLayout2.Controls.Add(this.bbmdTtlComboBox, 3, 0);

            var actionsLayout = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight };
            actionsFrame.Controls.Add(actionsLayout);

            this.discoverButton = new System.Windows.Forms.Button { Text = "Discover Devices", Width = 120 };
            actionsLayout.Controls.Add(this.discoverButton);

            this.pingButton = new System.Windows.Forms.Button { Text = "Ping Device", Width = 120 };
            actionsLayout.Controls.Add(this.pingButton);

            this.discoverObjectsButton = new System.Windows.Forms.Button { Text = "Discover Objects", Width = 120 };
            actionsLayout.Controls.Add(this.discoverObjectsButton);

            this.readPropertyButton = new System.Windows.Forms.Button { Text = "Read Property", Width = 120 };
            actionsLayout.Controls.Add(this.readPropertyButton);

            this.writePropertyButton = new System.Windows.Forms.Button { Text = "Write Property", Width = 120, Enabled = false };
            actionsLayout.Controls.Add(this.writePropertyButton);
        }

        #endregion

        private System.Windows.Forms.ComboBox ipAddressComboBox;
        private System.Windows.Forms.ComboBox instanceNumberComboBox;
        private System.Windows.Forms.ComboBox interfaceComboBox;
        private System.Windows.Forms.ComboBox apduTimeoutComboBox;
        private System.Windows.Forms.ComboBox bbmdIpComboBox;
        private System.Windows.Forms.ComboBox ipPortComboBox;
        private System.Windows.Forms.ComboBox bbmdTtlComboBox;
        private System.Windows.Forms.Button pingButton;
        private System.Windows.Forms.Button discoverButton;
        private System.Windows.Forms.Button readPropertyButton;
        private System.Windows.Forms.Button writePropertyButton;
        private System.Windows.Forms.Button discoverObjectsButton;
        private System.Windows.Forms.TreeView deviceTreeView;
        private System.Windows.Forms.TreeView objectTreeView;
        private System.Windows.Forms.RichTextBox outputTextBox;
    }
}
