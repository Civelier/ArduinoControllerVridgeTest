using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.Data
{
    
    public enum DataPacketError : byte
    {
        None = 0b00000000,
        Left = 0b10000000,
        Right = 0b01000000,
        MPU = 0b00100000,
        MPUInitMemLoad = 0b00100000,
        LeftMPUInitMemLoad = 0b10100000,
        RightMPUInitMemLoad = 0b01100000,
        MPUDMPConf = 0b00100001,
        LeftMPUDMPConf = 0b10100001,
        RightMPUDMPConf = 0b01100001,
        Arduino = 0b00010000,
        Transmission = 0b00001000,
        TransmissionAddressNACK = 0b00001000,
        LeftArduinoTransmissionAddressNACK = 0b10011000,
        RightArduinoTransmissionAddressNACK = 0b01011000,
        LeftMPUTransmissionAddressNACK = 0b10101000,
        RightMPUTransmissionAddressNACK = 0b01101000,
        TransmissionRegNACK = 0b00001001,
        LeftArduinoTransmissionRegNACK = 0b10011001,
        RightArduinoTransmissionRegNACK = 0b01011001,
        LeftMPUTransmissionRegNACK = 0b10101001,
        RightMPUTransmissionRegNACK = 0b01101001,
        TransmissionStreamEnded = 0b00001010,
        LeftArduinoTransmissionStreamEnded = 0b10011010,
        RightArduinoTransmissionStreamEnded = 0b01011010,
        LeftMPUTransmissionStreamEnded = 0b10101010,
        RightMPUTransmissionStreamEnded = 0b01101010,
        MasterMemGain = 0b00000001,
    }

    public struct DataPacket
    {
        private byte[] _buffer;
        public bool ContainsData => _buffer != null;
        public DataPacketError Error => _buffer == null ? DataPacketError.None : (DataPacketError)_buffer[0];
        public ArduinoData RightArduino => new ArduinoData(_buffer.GetRange(1, ArduinoData.Size));
        public ArduinoData LeftArduino => new ArduinoData(_buffer.GetRange(1 + ArduinoData.Size, ArduinoData.Size));
        public MPUData RightMPU => new MPUData(_buffer.GetRange(1 + 2 * ArduinoData.Size, MPUData.Size));
        public MPUData LeftMPU => new MPUData(_buffer.GetRange(1 + 2 * ArduinoData.Size + MPUData.Size, MPUData.Size));

        public DataPacket(byte[] buffer)
        {
            _buffer = buffer;
        }

        internal DataPacket(DataPacket last, DataPacketError error) : this(last._buffer)
        {
            _buffer[0] = (byte)error;
        }
    }
}
