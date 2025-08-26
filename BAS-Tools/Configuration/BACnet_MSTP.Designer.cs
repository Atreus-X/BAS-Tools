namespace MainApp.Configuration
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
            this.startDiscoveryButton = new System.Windows.Forms.Button();
            this.cancelDiscoveryButton = new System.Windows.Forms.Button();
            this.discoveryStatusLabel = new System.Windows.Forms.Label();
            this.pingButton = new System.Windows.Forms.Button();
            this.discoverObjectsButton = new System.Windows.Forms.Button();
            this.readPropertyButton = new System.Windows.Forms.Button();
            this.writePropertyButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.mstpFrame = new System.Windows.Forms.GroupBox();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.mstpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.serialPortComboBox = new System.Windows.Forms.ComboBox();
            this.instanceNumberComboBox = new System.Windows.Forms.ComboBox();
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.maxMastersComboBox = new System.Windows.Forms.ComboBox();
            this.maxInfoFramesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._localModeRadioButton = new System.Windows.Forms.RadioButton();
            this._remoteModeRadioButton = new System.Windows.Forms.RadioButton();
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
            this.mstpFrame.SuspendLayout();
            this.mstpLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.mainSplitContainer);
            this.mainPanel.Controls.Add(this.actionsFrame);
            this.mainPanel.Controls.Add(this.mstpFrame);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainPanel.Size = new System.Drawing.Size(800, 600);
            this.mainPanel.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(10, 280);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.browserFrame);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.outputFrame);
            this.mainSplitContainer.Size = new System.Drawing.Size(780, 310);
            this.mainSplitContainer.SplitterDistance = 450;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // browserFrame
            // 
            this.browserFrame.Controls.Add(this.browserSplitContainer);
            this.browserFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserFrame.Location = new System.Drawing.Point(0, 0);
            this.browserFrame.Name = "browserFrame";
            this.browserFrame.Padding = new System.Windows.Forms.Padding(10);
            this.browserFrame.Size = new System.Drawing.Size(450, 310);
            this.browserFrame.TabIndex = 0;
            this.browserFrame.TabStop = false;
            this.browserFrame.Text = "Device & Object Browser";
            // 
            // browserSplitContainer
            // 
            this.browserSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserSplitContainer.Location = new System.Drawing.Point(10, 28);
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
            this.browserSplitContainer.Size = new System.Drawing.Size(430, 272);
            this.browserSplitContainer.SplitterDistance = 135;
            this.browserSplitContainer.TabIndex = 0;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceTreeView.HideSelection = false;
            this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(430, 135);
            this.deviceTreeView.TabIndex = 0;
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.HideSelection = false;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(430, 133);
            this.objectTreeView.TabIndex = 0;
            // 
            // outputFrame
            // 
            this.outputFrame.Controls.Add(this.outputTextBox);
            this.outputFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFrame.Location = new System.Drawing.Point(0, 0);
            this.outputFrame.Name = "outputFrame";
            this.outputFrame.Padding = new System.Windows.Forms.Padding(10);
            this.outputFrame.Size = new System.Drawing.Size(326, 310);
            this.outputFrame.TabIndex = 0;
            this.outputFrame.TabStop = false;
            this.outputFrame.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.outputTextBox.Location = new System.Drawing.Point(10, 28);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(306, 272);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // actionsFrame
            // 
            this.actionsFrame.Controls.Add(this.actionsLayout);
            this.actionsFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionsFrame.Location = new System.Drawing.Point(10, 210);
            this.actionsFrame.Name = "actionsFrame";
            this.actionsFrame.Padding = new System.Windows.Forms.Padding(10);
            this.actionsFrame.Size = new System.Drawing.Size(780, 70);
            this.actionsFrame.TabIndex = 1;
            this.actionsFrame.TabStop = false;
            this.actionsFrame.Text = "Actions";
            // 
            // actionsLayout
            // 
            this.actionsLayout.Controls.Add(this.startDiscoveryButton);
            this.actionsLayout.Controls.Add(this.cancelDiscoveryButton);
            this.actionsLayout.Controls.Add(this.discoveryStatusLabel);
            this.actionsLayout.Controls.Add(this.pingButton);
            this.actionsLayout.Controls.Add(this.discoverObjectsButton);
            this.actionsLayout.Controls.Add(this.readPropertyButton);
            this.actionsLayout.Controls.Add(this.writePropertyButton);
            this.actionsLayout.Controls.Add(this.clearLogButton);
            this.actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsLayout.Location = new System.Drawing.Point(10, 28);
            this.actionsLayout.Name = "actionsLayout";
            this.actionsLayout.Size = new System.Drawing.Size(760, 32);
            this.actionsLayout.TabIndex = 0;
            // 
            // startDiscoveryButton
            // 
            this.startDiscoveryButton.Location = new System.Drawing.Point(3, 3);
            this.startDiscoveryButton.Name = "startDiscoveryButton";
            this.startDiscoveryButton.Size = new System.Drawing.Size(120, 23);
            this.startDiscoveryButton.TabIndex = 0;
            this.startDiscoveryButton.Text = "Discover Devices";
            this.startDiscoveryButton.UseVisualStyleBackColor = true;
            // 
            // cancelDiscoveryButton
            // 
            this.cancelDiscoveryButton.Location = new System.Drawing.Point(129, 3);
            this.cancelDiscoveryButton.Name = "cancelDiscoveryButton";
            this.cancelDiscoveryButton.Size = new System.Drawing.Size(75, 23);
            this.cancelDiscoveryButton.TabIndex = 6;
            this.cancelDiscoveryButton.Text = "Cancel";
            this.cancelDiscoveryButton.UseVisualStyleBackColor = true;
            this.cancelDiscoveryButton.Visible = false;
            // 
            // discoveryStatusLabel
            // 
            this.discoveryStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.discoveryStatusLabel.AutoSize = true;
            this.discoveryStatusLabel.Location = new System.Drawing.Point(210, 6);
            this.discoveryStatusLabel.Name = "discoveryStatusLabel";
            this.discoveryStatusLabel.Size = new System.Drawing.Size(100, 17);
            this.discoveryStatusLabel.TabIndex = 7;
            this.discoveryStatusLabel.Text = "Found: 0";
            this.discoveryStatusLabel.Visible = false;
            // 
            // pingButton
            // 
            this.pingButton.Location = new System.Drawing.Point(316, 3);
            this.pingButton.Name = "pingButton";
            this.pingButton.Size = new System.Drawing.Size(120, 23);
            this.pingButton.TabIndex = 1;
            this.pingButton.Text = "Ping Device";
            this.pingButton.UseVisualStyleBackColor = true;
            // 
            // discoverObjectsButton
            // 
            this.discoverObjectsButton.Location = new System.Drawing.Point(442, 3);
            this.discoverObjectsButton.Name = "discoverObjectsButton";
            this.discoverObjectsButton.Size = new System.Drawing.Size(120, 23);
            this.discoverObjectsButton.TabIndex = 2;
            this.discoverObjectsButton.Text = "Discover Objects";
            this.discoverObjectsButton.UseVisualStyleBackColor = true;
            // 
            // readPropertyButton
            // 
            this.readPropertyButton.Location = new System.Drawing.Point(568, 3);
            this.readPropertyButton.Name = "readPropertyButton";
            this.readPropertyButton.Size = new System.Drawing.Size(120, 23);
            this.readPropertyButton.TabIndex = 3;
            this.readPropertyButton.Text = "Read Property";
            this.readPropertyButton.UseVisualStyleBackColor = true;
            // 
            // writePropertyButton
            // 
            this.writePropertyButton.Enabled = false;
            this.writePropertyButton.Location = new System.Drawing.Point(3, 32);
            this.writePropertyButton.Name = "writePropertyButton";
            this.writePropertyButton.Size = new System.Drawing.Size(120, 23);
            this.writePropertyButton.TabIndex = 4;
            this.writePropertyButton.Text = "Write Property";
            this.writePropertyButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(129, 32);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(120, 23);
            this.clearLogButton.TabIndex = 8;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // mstpFrame
            // 
            this.mstpFrame.Controls.Add(this.settingsPanel);
            this.mstpFrame.Controls.Add(this._localModeRadioButton);
            this.mstpFrame.Controls.Add(this._remoteModeRadioButton);
            this.mstpFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.mstpFrame.Location = new System.Drawing.Point(10, 10);
            this.mstpFrame.Name = "mstpFrame";
            this.mstpFrame.Padding = new System.Windows.Forms.Padding(10);
            this.mstpFrame.Size = new System.Drawing.Size(780, 200);
            this.mstpFrame.TabIndex = 0;
            this.mstpFrame.TabStop = false;
            this.mstpFrame.Text = "BACnet MS/TP Configuration";
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsPanel.Location = new System.Drawing.Point(13, 50);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(754, 140);
            this.settingsPanel.TabIndex = 3;
            // 
            // mstpLayout
            // 
            this.mstpLayout.ColumnCount = 4;
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.mstpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mstpLayout.Controls.Add(this.serialPortComboBox, 1, 0);
            this.mstpLayout.Controls.Add(this.instanceNumberComboBox, 3, 0);
            this.mstpLayout.Controls.Add(this.baudRateComboBox, 1, 1);
            this.mstpLayout.Controls.Add(this.maxMastersComboBox, 3, 1);
            this.mstpLayout.Controls.Add(this.maxInfoFramesComboBox, 3, 2);
            this.mstpLayout.Controls.Add(this.label1, 0, 0);
            this.mstpLayout.Controls.Add(this.label2, 2, 0);
            this.mstpLayout.Controls.Add(this.label3, 0, 1);
            this.mstpLayout.Controls.Add(this.label4, 2, 1);
            this.mstpLayout.Controls.Add(this.label5, 2, 2);
            this.mstpLayout.Location = new System.Drawing.Point(10, 28);
            this.mstpLayout.Name = "mstpLayout";
            this.mstpLayout.RowCount = 3;
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mstpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mstpLayout.Size = new System.Drawing.Size(760, 162);
            this.mstpLayout.TabIndex = 0;
            // 
            // serialPortComboBox
            // 
            this.serialPortComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serialPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialPortComboBox.FormattingEnabled = true;
            this.serialPortComboBox.Location = new System.Drawing.Point(123, 3);
            this.serialPortComboBox.Name = "serialPortComboBox";
            this.serialPortComboBox.Size = new System.Drawing.Size(249, 24);
            this.serialPortComboBox.TabIndex = 0;
            // 
            // instanceNumberComboBox
            // 
            this.instanceNumberComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instanceNumberComboBox.FormattingEnabled = true;
            this.instanceNumberComboBox.Location = new System.Drawing.Point(508, 3);
            this.instanceNumberComboBox.Name = "instanceNumberComboBox";
            this.instanceNumberComboBox.Size = new System.Drawing.Size(249, 24);
            this.instanceNumberComboBox.TabIndex = 1;
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Location = new System.Drawing.Point(123, 57);
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(249, 24);
            this.baudRateComboBox.TabIndex = 2;
            // 
            // maxMastersComboBox
            // 
            this.maxMastersComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxMastersComboBox.FormattingEnabled = true;
            this.maxMastersComboBox.Location = new System.Drawing.Point(508, 57);
            this.maxMastersComboBox.Name = "maxMastersComboBox";
            this.maxMastersComboBox.Size = new System.Drawing.Size(249, 24);
            this.maxMastersComboBox.TabIndex = 3;
            // 
            // maxInfoFramesComboBox
            // 
            this.maxInfoFramesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxInfoFramesComboBox.FormattingEnabled = true;
            this.maxInfoFramesComboBox.Location = new System.Drawing.Point(508, 111);
            this.maxInfoFramesComboBox.Name = "maxInfoFramesComboBox";
            this.maxInfoFramesComboBox.Size = new System.Drawing.Size(249, 24);
            this.maxInfoFramesComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Serial Port:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Device Instance #:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Baud Rate:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(378, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Max Masters:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Max Info Frames:";
            // 
            // _localModeRadioButton
            // 
            this._localModeRadioButton.AutoSize = true;
            this._localModeRadioButton.Checked = true;
            this._localModeRadioButton.Location = new System.Drawing.Point(16, 24);
            this._localModeRadioButton.Name = "_localModeRadioButton";
            this._localModeRadioButton.Size = new System.Drawing.Size(134, 21);
            this._localModeRadioButton.TabIndex = 1;
            this._localModeRadioButton.TabStop = true;
            this._localModeRadioButton.Text = "Local (COM Port)";
            this._localModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // _remoteModeRadioButton
            // 
            this._remoteModeRadioButton.AutoSize = true;
            this._remoteModeRadioButton.Location = new System.Drawing.Point(160, 24);
            this._remoteModeRadioButton.Name = "_remoteModeRadioButton";
            this._remoteModeRadioButton.Size = new System.Drawing.Size(127, 21);
            this._remoteModeRadioButton.TabIndex = 2;
            this._remoteModeRadioButton.Text = "Remote (BBMD)";
            this._remoteModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // BACnet_MSTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "BACnet_MSTP";
            this.Size = new System.Drawing.Size(800, 600);
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
            this.actionsLayout.PerformLayout();
            this.mstpFrame.ResumeLayout(false);
            this.mstpFrame.PerformLayout();
            this.mstpLayout.ResumeLayout(false);
            this.mstpLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox mstpFrame;
        private System.Windows.Forms.TableLayoutPanel mstpLayout;
        private System.Windows.Forms.ComboBox serialPortComboBox;
        private System.Windows.Forms.ComboBox instanceNumberComboBox;
        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.ComboBox maxMastersComboBox;
        private System.Windows.Forms.ComboBox maxInfoFramesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton _localModeRadioButton;
        private System.Windows.Forms.RadioButton _remoteModeRadioButton;
        private System.Windows.Forms.Panel settingsPanel;
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
    }
}