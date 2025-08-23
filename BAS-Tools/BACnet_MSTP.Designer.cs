namespace MainApp
{
    partial class BACnet_MSTP
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            var mainPanel = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            this.Controls.Add(mainPanel);

            var mstpFrame = new System.Windows.Forms.GroupBox { Text = "BACnet MS/TP Configuration", Dock = System.Windows.Forms.DockStyle.Top, Height = 120, Padding = new System.Windows.Forms.Padding(10) };
            mainPanel.Controls.Add(mstpFrame);

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

            // MS/TP Configuration Layout
            var mstpLayout = new System.Windows.Forms.TableLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, ColumnCount = 4, RowCount = 3 };
            mstpFrame.Controls.Add(mstpLayout);
            mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            int row = 0;
            mstpLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Serial Port:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, row);
            this.serialPortComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            mstpLayout.Controls.Add(this.serialPortComboBox, 1, row);

            mstpLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Device Instance #:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, row);
            this.instanceNumberComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            this.instanceNumberComboBox.TextChanged += new System.EventHandler(this.UpdateAllStates);
            mstpLayout.Controls.Add(this.instanceNumberComboBox, 3, row++);

            mstpLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Baud Rate:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, row);
            this.baudRateComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            mstpLayout.Controls.Add(this.baudRateComboBox, 1, row);

            mstpLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Max Masters:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, row);
            this.maxMastersComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            mstpLayout.Controls.Add(this.maxMastersComboBox, 3, row++);

            mstpLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Data Bits:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, row);
            this.dataBitsComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            mstpLayout.Controls.Add(this.dataBitsComboBox, 1, row);

            mstpLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Max Info Frames:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, row);
            this.maxInfoFramesComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            mstpLayout.Controls.Add(this.maxInfoFramesComboBox, 3, row++);

            // FlowLayout for Actions
            var actionsLayout = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight };
            actionsFrame.Controls.Add(actionsLayout);

            this.discoverButton = new System.Windows.Forms.Button { Text = "Discover Devices", Width = 120 };
            this.discoverButton.Click += new System.EventHandler(this.DiscoverButton_Click);
            actionsLayout.Controls.Add(this.discoverButton);

            this.pingButton = new System.Windows.Forms.Button { Text = "Ping Device", Width = 120 };
            this.pingButton.Click += new System.EventHandler(this.PingButton_Click);
            actionsLayout.Controls.Add(this.pingButton);

            this.discoverObjectsButton = new System.Windows.Forms.Button { Text = "Discover Objects", Width = 120 };
            this.discoverObjectsButton.Click += new System.EventHandler(this.DiscoverObjectsButton_Click);
            actionsLayout.Controls.Add(this.discoverObjectsButton);

            this.readPropertyButton = new System.Windows.Forms.Button { Text = "Read Property", Width = 120 };
            this.readPropertyButton.Click += new System.EventHandler(this.ReadPropertyButton_Click);
            actionsLayout.Controls.Add(this.readPropertyButton);

            this.writePropertyButton = new System.Windows.Forms.Button { Text = "Write Property", Width = 120, Enabled = false };
            actionsLayout.Controls.Add(this.writePropertyButton); // Not implemented yet
        }

        #endregion

        private System.Windows.Forms.ComboBox serialPortComboBox;
        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.ComboBox dataBitsComboBox;
        private System.Windows.Forms.ComboBox maxMastersComboBox;
        private System.Windows.Forms.ComboBox maxInfoFramesComboBox;
        private System.Windows.Forms.ComboBox instanceNumberComboBox;
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
