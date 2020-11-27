using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.Data
{
    [Flags]
    public enum DataPacketError : byte
    {
        None = 0b00000000,
        Left = 0b10000000,
        Right = 0b01000000,
        MPU = 0b00100000,
        MPUInitMemLoad = 0b00100000,
        MPUDMPConf = 0b00100001,
        Arduino = 0b00010000,
        Transmission = 0b00001000,
        TransmissionAddressNACK = 0b00001000,
        TransmissionRegNACK = 0b00001001,
    }

    public struct DataPacket
    {
        private byte[] _buffer;
        public bool ContainsData => _buffer != null;
        public DataPacketError Error => (DataPacketError)_buffer[0];
        public ArduinoData RightArduino => new ArduinoData(_buffer.GetRange(1, ArduinoData.Size));
        public ArduinoData LeftArduino => new ArduinoData(_buffer.GetRange(1 + ArduinoData.Size, ArduinoData.Size));
        public MPUData RightMPU => new MPUData(_buffer.GetRange(1 + 2 * ArduinoData.Size, MPUData.Size));
        public MPUData LeftMPU => new MPUData(_buffer.GetRange(1 + 2 * ArduinoData.Size + MPUData.Size, MPUData.Size));

        public DataPacket(byte[] buffer)
        {
            _buffer = buffer;
        }
    }
}
