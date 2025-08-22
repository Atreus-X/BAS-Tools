using System.Windows.Forms;
namespace BacnetToolsCSharp
{
    public partial class Modbus_RTU : UserControl
    {
        public Modbus_RTU()
        {
            InitializeComponent();
            var label = new Label { Text = "Modbus RTU Configuration Not Implemented", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter };
            this.Controls.Add(label);
        }
    }
}