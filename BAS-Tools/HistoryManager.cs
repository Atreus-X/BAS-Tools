using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json; // Make sure System.Text.Json NuGet package is installed
using System.Windows.Forms; // Only for MessageBox in error handling, consider moving to Log if available

namespace MainApp
{
    /// <summary>
    /// Manages the history of user inputs for various controls, storing them in a JSON file.
    /// Each protocol can use a unique prefix to keep its history separate within the same file.
    /// </summary>
    public class HistoryManager
    {
        private const string HISTORY_FILE_NAME = "BAS-Tools.history.json";
#pragma warning disable IDE0044 // Add readonly modifier
        private string _historyFilePath;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning disable IDE0044 // Add readonly modifier
        private Dictionary<string, List<string>> _history;
#pragma warning restore IDE0044 // Add readonly modifier
        private readonly string _prefix; // Prefix for keys to isolate history per protocol

        /// <summary>
        /// Initializes a new instance of the HistoryManager.
        /// </summary>
        /// <param name="prefix">A unique prefix for this history instance (e.g., "BACnet_IP_", "Modbus_TCP_").</param>
        public HistoryManager(string prefix)
        {
            _prefix = prefix;
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var appDirectory = Path.Combine(appDataPath, "BAS-Tools");
            _historyFilePath = Path.Combine(appDirectory, HISTORY_FILE_NAME);

            // Ensure the directory exists
            Directory.CreateDirectory(appDirectory);

            _history = LoadHistory();
        }

        /// <summary>
        /// Gets the entire loaded history dictionary.
        /// </summary>
        public Dictionary<string, List<string>> History => _history;

        /// <summary>
        /// Loads the history from the JSON file.
        /// </summary>
        /// <returns>A dictionary containing the loaded history, or a new empty dictionary if loading fails.</returns>
        private Dictionary<string, List<string>> LoadHistory()
        {
            if (File.Exists(_historyFilePath))
            {
                try
                {
                    var jsonString = File.ReadAllText(_historyFilePath);
                    return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString) ?? new Dictionary<string, List<string>>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading history from {_historyFilePath}: {ex.Message}", "History Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return new Dictionary<string, List<string>>();
                }
            }
            return new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Saves the current history to the JSON file.
        /// </summary>
        public void SaveHistory()
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(_history, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_historyFilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving history to {_historyFilePath}: {ex.Message}", "History Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Clears all history entries associated with this manager's prefix.
        /// </summary>
        public void ClearHistory()
        {
            var keysToRemove = _history.Keys.Where(k => k.StartsWith(_prefix)).ToList();
            foreach (var key in keysToRemove)
            {
                _history.Remove(key);
            }
            SaveHistory();
        }

        /// <summary>
        /// Adds an entry to the history for a given key, ensuring it's at the top and limits the list size.
        /// </summary>
        /// <param name="key">The key for the history entry (will be prefixed).</param>
        /// <param name="value">The value to add to the history.</param>
        public void AddEntry(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            string prefixedKey = _prefix + key;

            if (!_history.ContainsKey(prefixedKey))
            {
                _history[prefixedKey] = new List<string>();
            }

            var list = _history[prefixedKey];
            if (list.Contains(value))
            {
                list.Remove(value); // Remove if it already exists to move it to the top
            }
            list.Insert(0, value); // Add to the top of the list

            // Keep the list to a reasonable size, e.g., 20
            if (list.Count > 20)
            {
                list.RemoveAt(list.Count - 1);
            }

            // Save immediately after adding an entry, so changes are persistent.
            SaveHistory();
        }

        /// <summary>
        /// Retrieves the history list for a specific key (prefixed automatically).
        /// </summary>
        /// <param name="key">The key for which to retrieve history.</param>
        /// <returns>A list of history entries for the given key, or an empty list if none exists.</returns>
        public List<string> GetHistoryForPrefixedKey(string key)
        {
            string prefixedKey = _prefix + key;
            return _history.TryGetValue(prefixedKey, out var list) ? list : new List<string>();
        }
    }
}