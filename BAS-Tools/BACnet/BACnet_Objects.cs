using System.Collections.Generic;
using System.IO.BACnet;

namespace MainApp.BACnet
{
    public struct BacnetObjectInfo
    {
        public string Label;
        public string Group;

        public BacnetObjectInfo(string label, string group)
        {
            Label = label;
            Group = group;
        }
    }

    public static class BACnetObjectFactory
    {
        private static readonly Dictionary<BacnetObjectTypes, BacnetObjectInfo> StandardObjects = new Dictionary<BacnetObjectTypes, BacnetObjectInfo>
        {
            { BacnetObjectTypes.OBJECT_ANALOG_INPUT, new BacnetObjectInfo("Analog Input", "Analog Inputs") },
            { BacnetObjectTypes.OBJECT_ANALOG_OUTPUT, new BacnetObjectInfo("Analog Output", "Analog Outputs") },
            { BacnetObjectTypes.OBJECT_ANALOG_VALUE, new BacnetObjectInfo("Analog Value", "Analog Values") },
            { BacnetObjectTypes.OBJECT_BINARY_INPUT, new BacnetObjectInfo("Binary Input", "Binary Inputs") },
            { BacnetObjectTypes.OBJECT_BINARY_OUTPUT, new BacnetObjectInfo("Binary Output", "Binary Outputs") },
            { BacnetObjectTypes.OBJECT_BINARY_VALUE, new BacnetObjectInfo("Binary Value", "Binary Values") },
            { BacnetObjectTypes.OBJECT_CALENDAR, new BacnetObjectInfo("Calendar", "Calendars") },
            { BacnetObjectTypes.OBJECT_DEVICE, new BacnetObjectInfo("Device", "Devices") },
            { BacnetObjectTypes.OBJECT_FILE, new BacnetObjectInfo("File", "Files") },
            { BacnetObjectTypes.OBJECT_MULTI_STATE_INPUT, new BacnetObjectInfo("Multi-State Input", "Multi-State Inputs") },
            { BacnetObjectTypes.OBJECT_MULTI_STATE_OUTPUT, new BacnetObjectInfo("Multi-State Output", "Multi-State Outputs") },
            { BacnetObjectTypes.OBJECT_NOTIFICATION_CLASS, new BacnetObjectInfo("Notification Class", "Notification Classes") },
            { BacnetObjectTypes.OBJECT_PROGRAM, new BacnetObjectInfo("Program", "Programs") },
            { BacnetObjectTypes.OBJECT_SCHEDULE, new BacnetObjectInfo("Schedule", "Schedules") },
            { BacnetObjectTypes.OBJECT_MULTI_STATE_VALUE, new BacnetObjectInfo("Multi-State Value", "Multi-State Values") },
            { BacnetObjectTypes.OBJECT_TRENDLOG, new BacnetObjectInfo("Trend Log", "Trend Logs") },
            { BacnetObjectTypes.OBJECT_ACCUMULATOR, new BacnetObjectInfo("Accumulator", "Accumulators") },
            { BacnetObjectTypes.OBJECT_PULSE_CONVERTER, new BacnetObjectInfo("Pulse Converter", "Pulse Converters") }
        };

        private static readonly Dictionary<ushort, Dictionary<BacnetObjectTypes, BacnetObjectInfo>> ProprietaryObjects = new Dictionary<ushort, Dictionary<BacnetObjectTypes, BacnetObjectInfo>>
        {
            {
                6, new Dictionary<BacnetObjectTypes, BacnetObjectInfo>
                {
                    { (BacnetObjectTypes)128, new BacnetObjectInfo("Unit Health", "Unit Health") },
                    { (BacnetObjectTypes)143, new BacnetObjectInfo("Broadcasts", "Broadcasts") },
                    { (BacnetObjectTypes)200, new BacnetObjectInfo("Universal Input Summary", "Universal Input Summary") },
                    { (BacnetObjectTypes)201, new BacnetObjectInfo("Binary Output Summary", "Binary Output Summary") },
                    { (BacnetObjectTypes)202, new BacnetObjectInfo("Analog Output Summary", "Analog Output Summary") },
                    { (BacnetObjectTypes)203, new BacnetObjectInfo("Binary Input Summary", "Binary Input Summary") },
                    { (BacnetObjectTypes)204, new BacnetObjectInfo("Summary", "Summary") },
                    { (BacnetObjectTypes)205, new BacnetObjectInfo("Schedule Summary", "Schedule Summary") },
                    { (BacnetObjectTypes)300, new BacnetObjectInfo("Input Selects", "Input Selects") },
                    { (BacnetObjectTypes)301, new BacnetObjectInfo("Min / Max / Avg", "Min / Max / Avg") },
                    { (BacnetObjectTypes)302, new BacnetObjectInfo("Math", "Math") },
                    { (BacnetObjectTypes)303, new BacnetObjectInfo("Logic", "Logic") },
                    { (BacnetObjectTypes)304, new BacnetObjectInfo("Local Remaps", "Local Remaps") },
                    { (BacnetObjectTypes)305, new BacnetObjectInfo("Piecewise Curves", "Piecewise Curves") },
                    { (BacnetObjectTypes)306, new BacnetObjectInfo("Scaling", "Scaling") },
                    { (BacnetObjectTypes)307, new BacnetObjectInfo("Netmaps", "Netmaps") },
                    { (BacnetObjectTypes)308, new BacnetObjectInfo("Enthalpy", "Enthalpy") },
                    { (BacnetObjectTypes)309, new BacnetObjectInfo("Staging", "Staging") },
                    { (BacnetObjectTypes)400, new BacnetObjectInfo("Communication Status", "Communication Status") },
                    { (BacnetObjectTypes)402, new BacnetObjectInfo("Season", "Season") },
                    { (BacnetObjectTypes)403, new BacnetObjectInfo("Timers", "Timers") },
                    { (BacnetObjectTypes)450, new BacnetObjectInfo("StatBus Ports", "StatBus Ports") },
                    { (BacnetObjectTypes)500, new BacnetObjectInfo("Analog-Output Control Loops", "Analog-Output Control Loops") },
                    { (BacnetObjectTypes)501, new BacnetObjectInfo("Pulse-Pair Control Loops", "Pulse-Pair Control Loops") },
                    { (BacnetObjectTypes)502, new BacnetObjectInfo("Thermostat Control Loops", "Thermostat Control Loops") }
                }
            }
        };

        public static BacnetObjectInfo GetBacnetObjectInfo(BacnetObjectTypes objectType, ushort vendorId)
        {
            if (ProprietaryObjects.ContainsKey(vendorId) && ProprietaryObjects[vendorId].ContainsKey(objectType))
            {
                return ProprietaryObjects[vendorId][objectType];
            }

            if (StandardObjects.ContainsKey(objectType))
            {
                return StandardObjects[objectType];
            }

            string objectTypeStr = objectType.ToString();
            return new BacnetObjectInfo(objectTypeStr, objectTypeStr + "s");
        }
    }
}

