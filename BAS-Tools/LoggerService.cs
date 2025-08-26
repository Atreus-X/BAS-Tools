using System;
using System.Windows.Forms;

namespace MainApp
{
    public interface ILoggerService
    {
        void Log(string message);
        void SetOutputControl(RichTextBox outputTextBox);
    }

    public class LoggerService : ILoggerService
    {
        private RichTextBox _outputTextBox;

        public void SetOutputControl(RichTextBox outputTextBox)
        {
            _outputTextBox = outputTextBox;
        }

        public void Log(string message)
        {
            if (_outputTextBox == null) return;

            if (_outputTextBox.InvokeRequired)
            {
                _outputTextBox.Invoke(new Action<string>(Log), message);
                return;
            }

            _outputTextBox.AppendText(DateTime.Now.ToLongTimeString() + ": " + message + Environment.NewLine);
            _outputTextBox.ScrollToCaret();
        }
    }
}