using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCToArduinoCommunication.Devices;

namespace PCToArduinoCommunication.Protocol
{
    public static class ProtocolInfo
    {
        public static byte[] Version => new byte[] { 0, 1, 0 };
        public static byte[] AccessCode => new byte[] { 213, 194 };
        public static Dictionary<byte, IControllerDevice> Devices => new Dictionary<byte, IControllerDevice>()
        {
            { 0, new ArduinoController("Left controller") },
            { 1, new ArduinoController("Right controller") },
            { 255, null },
        };
    }
}
