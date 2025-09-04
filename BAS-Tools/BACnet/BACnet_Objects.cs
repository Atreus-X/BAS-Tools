using System.Collections.Generic;
using System.IO.BACnet;

namespace MainApp.BACnet
{
    public static class BACnetObjectFactory
    {
        // Standard BACnet Objects
        private static readonly Dictionary<BacnetObjectTypes, string> StandardObjectTypeNames = new Dictionary<BacnetObjectTypes, string>
        {
            { BacnetObjectTypes.OBJECT_ANALOG_INPUT, "Analog Inputs" },
            { BacnetObjectTypes.OBJECT_ANALOG_OUTPUT, "Analog Outputs" },
            { BacnetObjectTypes.OBJECT_ANALOG_VALUE, "Analog Values" },
            { BacnetObjectTypes.OBJECT_BINARY_INPUT, "Binary Inputs" },
            { BacnetObjectTypes.OBJECT_BINARY_OUTPUT, "Binary Outputs" },
            { BacnetObjectTypes.OBJECT_BINARY_VALUE, "Binary Values" },
            { BacnetObjectTypes.OBJECT_CALENDAR, "Calendars" },
            { BacnetObjectTypes.OBJECT_DEVICE, "Devices" },
            { BacnetObjectTypes.OBJECT_FILE, "Files" },
            { BacnetObjectTypes.OBJECT_MULTI_STATE_INPUT, "Multi-State Inputs" },
            { BacnetObjectTypes.OBJECT_MULTI_STATE_OUTPUT, "Multi-State Outputs" },
            { BacnetObjectTypes.OBJECT_NOTIFICATION_CLASS, "Notification Classes" },
            { BacnetObjectTypes.OBJECT_PROGRAM, "Programs" },
            { BacnetObjectTypes.OBJECT_SCHEDULE, "Schedules" },
            { BacnetObjectTypes.OBJECT_MULTI_STATE_VALUE, "Multi-State Values" },
            { BacnetObjectTypes.OBJECT_TRENDLOG, "Trend Logs" },
            { BacnetObjectTypes.OBJECT_ACCUMULATOR, "Accumulators" },
            { BacnetObjectTypes.OBJECT_PULSE_CONVERTER, "Pulse Converters" }
        };

        // Vendor ID 6: ABB/American Auto-Matrix
        private static readonly Dictionary<int, string> AAMObjectTypeNames = new Dictionary<int, string>
        {
            { 128, "Unit Health" },
            { 143, "Broadcasts" },
            { 200, "Universal Input Summary" },
            { 201, "Binary Output Summary" },
            { 202, "Analog Output Summary" },
            { 203, "Binary Input Summary" },
            { 204, "Summary" },
            { 205, "Schedule Summary" },
            { 300, "Input Selects" },
            { 301, "Min / Max / Avg" },
            { 302, "Math" },
            { 303, "Logic" },
            { 304, "Local Remaps" },
            { 305, "Piecewise Curves" },
            { 306, "Scaling" },
            { 307, "Netmaps" },
            { 308, "Enthalpy" },
            { 309, "Staging" },
            { 400, "Communication Status" },
            { 402, "Season" },
            { 403, "Timers" },
            { 450, "StatBus Ports" },
            { 500, "Analog-Output Control Loops" },
            { 501, "Pulse-Pair Control Loops" },
            { 502, "Thermostat Control Loops" }
        };

        public static string GetGroupLabel(ushort vendorId, BacnetObjectTypes objectType)
        {
            if (StandardObjectTypeNames.TryGetValue(objectType, out string name))
            {
                return name;
            }

            if (vendorId == 6 && AAMObjectTypeNames.TryGetValue((int)objectType, out name))
            {
                return name;
            }

            return objectType.ToString();
        }

        public static string GetInstanceLabel(ushort vendorId, BacnetObjectTypes objectType, uint instance)
        {
            string groupLabel = GetGroupLabel(vendorId, objectType);

            if (groupLabel.EndsWith("s"))
            {
                groupLabel = groupLabel.Substring(0, groupLabel.Length - 1);
            }

            return $"{groupLabel} {instance}";
        }
    }
}
