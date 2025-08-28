namespace MainApp
{
    partial class MainApp
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bacnetIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bacnetMSTPLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bacnetMSTPRemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modbusTCPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modbusRTUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(834, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearHistoryToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // clearHistoryToolStripMenuItem
            // 
            this.clearHistoryToolStripMenuItem.Name = "clearHistoryToolStripMenuItem";
            this.clearHistoryToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.clearHistoryToolStripMenuItem.Text = "Clear History";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bacnetIPToolStripMenuItem,
            this.bacnetMSTPLocalToolStripMenuItem,
            this.bacnetMSTPRemoteToolStripMenuItem,
            this.modbusTCPToolStripMenuItem,
            this.modbusRTUToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // bacnetIPToolStripMenuItem
            // 
            this.bacnetIPToolStripMenuItem.Name = "bacnetIPToolStripMenuItem";
            this.bacnetIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bacnetIPToolStripMenuItem.Text = "BACnet/IP";
            // 
            // bacnetMSTPLocalToolStripMenuItem
            // 
            this.bacnetMSTPLocalToolStripMenuItem.Name = "bacnetMSTPLocalToolStripMenuItem";
            this.bacnetMSTPLocalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bacnetMSTPLocalToolStripMenuItem.Text = "BACnet MS/TP Local";
            // 
            // bacnetMSTPRemoteToolStripMenuItem
            // 
            this.bacnetMSTPRemoteToolStripMenuItem.Name = "bacnetMSTPRemoteToolStripMenuItem";
            this.bacnetMSTPRemoteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bacnetMSTPRemoteToolStripMenuItem.Text = "BACnet MS/TP Remote";
            // 
            // modbusTCPToolStripMenuItem
            // 
            this.modbusTCPToolStripMenuItem.Name = "modbusTCPToolStripMenuItem";
            this.modbusTCPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.modbusTCPToolStripMenuItem.Text = "Modbus TCP/IP";
            // 
            // modbusRTUToolStripMenuItem
            // 
            this.modbusRTUToolStripMenuItem.Name = "modbusRTUToolStripMenuItem";
            this.modbusRTUToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.modbusRTUToolStripMenuItem.Text = "Modbus RTU";
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(0, 24);
            this.mainContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(834, 969);
            this.mainContentPanel.TabIndex = 1;
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 993);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainApp";
            this.Text = "BACnet Tools";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bacnetIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bacnetMSTPLocalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bacnetMSTPRemoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modbusTCPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modbusRTUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}