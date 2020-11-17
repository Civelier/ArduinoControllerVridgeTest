using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCToArduinoCommunication.Protocol;

namespace PCToArduinoCommunication.Devices
{
    public class ArduinoController : IControllerDevice
    {
        public PCToArduinoCommunicationProtocol Port { get; set; }
        public string Name { get; }

        public ArduinoController(string name)
        {
            Name = name;
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFirmwareVersion()
        {
            throw new NotImplementedException();
        }

        public uint Ping()
        {
            throw new NotImplementedException();
        }

        public DateTime TimeCode()
        {
            throw new NotImplementedException();
        }
    }
}
