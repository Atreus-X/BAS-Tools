namespace MainApp
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            var mainPanel = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            this.Controls.Add(mainPanel);

            var configFrame = new System.Windows.Forms.GroupBox { Text = "Modbus TCP/IP Configuration", Dock = System.Windows.Forms.DockStyle.Top, Height = 100, Padding = new System.Windows.Forms.Padding(10) };
            mainPanel.Controls.Add(configFrame);

            var controlsPanel = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(0, 10, 0, 0) };
            mainPanel.Controls.Add(controlsPanel);

            var mainSplitContainer = new System.Windows.Forms.SplitContainer { Dock = System.Windows.Forms.DockStyle.Fill, Orientation = System.Windows.Forms.Orientation.Vertical, SplitterDistance = 450 };
            controlsPanel.Controls.Add(mainSplitContainer);

            var actionsFrame = new System.Windows.Forms.GroupBox { Text = "Modbus Actions", Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            mainSplitContainer.Panel1.Controls.Add(actionsFrame);

            var outputFrame = new System.Windows.Forms.GroupBox { Text = "Output", Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };
            mainSplitContainer.Panel2.Controls.Add(outputFrame);

            this.outputTextBox = new System.Windows.Forms.RichTextBox { Dock = System.Windows.Forms.DockStyle.Fill, ReadOnly = true, Font = new System.Drawing.Font("Consolas", 9.75f), BackColor = System.Drawing.Color.WhiteSmoke };
            outputFrame.Controls.Add(this.outputTextBox);

            // Configuration Layout
            var configLayout = new System.Windows.Forms.TableLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, ColumnCount = 4, RowCount = 2 };
            configFrame.Controls.Add(configLayout);
            configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            configLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            int row = 0;
            configLayout.Controls.Add(new System.Windows.Forms.Label { Text = "IP Address:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, row);
            this.ipAddressComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            configLayout.Controls.Add(this.ipAddressComboBox, 1, row);

            configLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Port:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 2, row);
            this.portComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            configLayout.Controls.Add(this.portComboBox, 3, row++);

            configLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Unit ID:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, row);
            this.unitIdComboBox = new System.Windows.Forms.ComboBox { Dock = System.Windows.Forms.DockStyle.Fill };
            configLayout.Controls.Add(this.unitIdComboBox, 1, row);

            this.connectButton = new System.Windows.Forms.Button { Text = "Connect", Width = 80, Height = 25 };
            configLayout.Controls.Add(this.connectButton, 2, row);
            this.disconnectButton = new System.Windows.Forms.Button { Text = "Disconnect", Width = 80, Height = 25 };
            configLayout.Controls.Add(this.disconnectButton, 3, row++);

            // Actions Layout
            var actionsLayout = new System.Windows.Forms.TableLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, ColumnCount = 2, RowCount = 7 };
            actionsFrame.Controls.Add(actionsLayout);
            actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            int actionRow = 0;
            actionsLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Start Address:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, actionRow);
            this.startAddressTextBox = new System.Windows.Forms.TextBox { Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.startAddressTextBox, 1, actionRow++);

            actionsLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Quantity:", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, actionRow);
            this.quantityTextBox = new System.Windows.Forms.TextBox { Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.quantityTextBox, 1, actionRow++);

            actionsLayout.Controls.Add(new System.Windows.Forms.Label { Text = "Write Value(s):", Anchor = System.Windows.Forms.AnchorStyles.Left, AutoSize = true }, 0, actionRow);
            this.writeValueTextBox = new System.Windows.Forms.TextBox { Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.writeValueTextBox, 1, actionRow++);

            this.readCoilsButton = new System.Windows.Forms.Button { Text = "Read Coils", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.readCoilsButton, 0, actionRow);
            this.readDiscreteInputsButton = new System.Windows.Forms.Button { Text = "Read Discrete Inputs", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.readDiscreteInputsButton, 1, actionRow++);

            this.readHoldingRegistersButton = new System.Windows.Forms.Button { Text = "Read Holding Registers", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.readHoldingRegistersButton, 0, actionRow);
            this.readInputRegistersButton = new System.Windows.Forms.Button { Text = "Read Input Registers", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.readInputRegistersButton, 1, actionRow++);

            this.writeSingleCoilButton = new System.Windows.Forms.Button { Text = "Write Single Coil", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.writeSingleCoilButton, 0, actionRow);
            this.writeSingleRegisterButton = new System.Windows.Forms.Button { Text = "Write Single Register", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.writeSingleRegisterButton, 1, actionRow++);

            this.writeMultipleCoilsButton = new System.Windows.Forms.Button { Text = "Write Multiple Coils", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.writeMultipleCoilsButton, 0, actionRow);
            this.writeMultipleRegistersButton = new System.Windows.Forms.Button { Text = "Write Multiple Registers", Dock = System.Windows.Forms.DockStyle.Fill };
            actionsLayout.Controls.Add(this.writeMultipleRegistersButton, 1, actionRow++);
        }

        #endregion

        private System.Windows.Forms.ComboBox ipAddressComboBox;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.ComboBox unitIdComboBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.TextBox startAddressTextBox;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.TextBox writeValueTextBox;
        private System.Windows.Forms.Button readCoilsButton;
        private System.Windows.Forms.Button readDiscreteInputsButton;
        private System.Windows.Forms.Button readHoldingRegistersButton;
        private System.Windows.Forms.Button readInputRegistersButton;
        private System.Windows.Forms.Button writeSingleCoilButton;
        private System.Windows.Forms.Button writeSingleRegisterButton;
        private System.Windows.Forms.Button writeMultipleCoilsButton;
        private System.Windows.Forms.Button writeMultipleRegistersButton;
        private System.Windows.Forms.RichTextBox outputTextBox;
    }
}
