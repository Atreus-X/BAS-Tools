using System.Windows.Forms;
namespace BacnetToolsCSharp
{
    public partial class Modbus_IP : UserControl
    {
        public Modbus_IP()
        {
            InitializeComponent();
            var label = new Label { Text = "Modbus TCP/IP Configuration Not Implemented", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter };
            this.Controls.Add(label);
        }
    }
}