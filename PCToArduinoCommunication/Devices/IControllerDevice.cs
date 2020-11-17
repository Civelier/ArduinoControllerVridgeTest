using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Devices
{
    public interface IControllerDevice
    {
        string Name { get; }
        byte[] GetFirmwareVersion();
        uint Ping();
        DateTime TimeCode();
        void Activate();
        void Deactivate();
    }
}
