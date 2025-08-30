using System;
using System.Windows.Forms;

namespace MainApp
{
    /// <summary>
    /// A simple static logger to allow deep parts of the code
    /// to write messages to the active UI's RichTextBox.
    /// </summary>
    public static class GlobalLogger
    {
        private static RichTextBox _outputTextBox;
        private static Action<string> _logAction;

        /// <summary>
        /// The UI control calls this to register its output box.
        /// </summary>
        public static void Register(RichTextBox outputTextBox)
        {
            _outputTextBox = outputTextBox;
            _logAction = (message) =>
            {
                if (_outputTextBox == null || _outputTextBox.IsDisposed) return;

                if (_outputTextBox.InvokeRequired)
                {
                    // Ensure thread-safe UI updates
                    _outputTextBox.Invoke(new Action(() => _logAction(message)));
                }
                else
                {
                    _outputTextBox.AppendText($"{DateTime.Now.ToLongTimeString()}: [DEBUG] {message}{Environment.NewLine}");
                    _outputTextBox.ScrollToCaret();
                }
            };
        }

        /// <summary>
        /// A static method to log messages from anywhere in the application.
        /// </summary>
        public static void Log(string message)
        {
            // Invoke the logging action if it has been registered
            _logAction?.Invoke(message);
        }
    }
}