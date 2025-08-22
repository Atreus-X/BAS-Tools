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
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(834, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";

            // --- Create Menu Items ---
            var fileMenu = new System.Windows.Forms.ToolStripMenuItem("File");
            var configMenu = new System.Windows.Forms.ToolStripMenuItem("Configuration");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileMenu, configMenu });

            // File Menu Dropdown
            var exitMenuItem = new System.Windows.Forms.ToolStripMenuItem("Exit");
            exitMenuItem.Click += (sender, e) => this.Close();
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Configuration Menu Dropdown
            var bacnetIpMenuItem = new System.Windows.Forms.ToolStripMenuItem("BACnet/IP");
            bacnetIpMenuItem.Click += new System.EventHandler(this.bacnetIpMenuItem_Click);
            var bacnetMstpMenuItem = new System.Windows.Forms.ToolStripMenuItem("BACnet MS/TP");
            bacnetMstpMenuItem.Click += new System.EventHandler(this.bacnetMstpMenuItem_Click);
            var modbusTcpMenuItem = new System.Windows.Forms.ToolStripMenuItem("Modbus TCP/IP");
            modbusTcpMenuItem.Click += new System.EventHandler(this.modbusTcpMenuItem_Click);
            var modbusRtuMenuItem = new System.Windows.Forms.ToolStripMenuItem("Modbus RTU");
            modbusRtuMenuItem.Click += new System.EventHandler(this.modbusRtuMenuItem_Click);
            configMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { bacnetIpMenuItem, bacnetMstpMenuItem, modbusTcpMenuItem, modbusRtuMenuItem });

            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(0, 24);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(834, 776);
            this.mainContentPanel.TabIndex = 1;
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 800);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainApp";
            this.Text = "BACnet Tools";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.Panel mainContentPanel;
    }
}