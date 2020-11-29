using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.Data
{
    public class DataDecodedEventArgs
    {
        public DataDecodedEventArgs(DataPacket data)
        {
            Data = data;
        }

        public DataPacket Data
        {
            get;
        }
    }

    public delegate void DataDecodedEventHandler(DataDecoder sender, DataDecodedEventArgs args);

    public delegate void ErrorFoundEventHandler(DataDecoder sender, ErrorFoundEventArgs args);

    public class ErrorFoundEventArgs
    {
        public DataPacketError Error { get; }

        public bool IsMaster => AreDown(0b00110000);
        public bool IsSlave => AreUp(0b00010000);
        public bool IsMPU => AreUp(0b00100000);
        public bool IsLeft => AreUp(0b10000000);
        public bool IsRight => AreUp(0b01000000);
        public bool IsTransmission => AreUp(0b00001000);

        public ErrorFoundEventArgs(DataPacketError error)
        {
            Error = error;
        }

        private bool AreUp(byte v)
        {
            return ((byte)Error & v) == v;
        }

        private bool AreDown(byte v)
        {
            return ((byte)Error & v) == 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (IsLeft) sb.Append("Left ");
            if (IsRight) sb.Append("Right ");
            if (IsSlave) sb.Append("Arduino ");
            if (IsMPU) sb.Append("MPU ");
            if (IsMaster) sb.Append("Master ");
            if (Error == DataPacketError.MPUInitMemLoad) sb.Append("falied initial memory load!");
            if (Error == DataPacketError.MPUDMPConf) sb.Append("couldn't load DMP configuration!");
            if (IsTransmission)
            {
                if (((byte)Error & 0b00000111) == 0b00000000) sb.Append("did not ack (possible incorrect address or device not connected)!");
                if (((byte)Error & 0b00000111) == 0b00000001) sb.Append("registry did not ack (potential incorrect address or incorrect version)!");
                if (((byte)Error & 0b00000111) == 0b00000010) sb.Append("stream closed (potential bad connection)!");
            }
            if (Error == DataPacketError.MasterMemGain) sb.Append("Master gained memory (memory leak)!");
            return sb.ToString();
        }
    }

    public class DataDecoder
    {
        SerialPort _port;

        public event DataDecodedEventHandler DataDecoded;

        public event ErrorFoundEventHandler ErrorFound;

        byte[] _buffer;

        public bool IsAutoRefreshEnabled
        {
            get;
            set;
        }

        int _bufferOffset;
        private int _writePosition;

        private bool _isReady;
        private bool _requestSent;
        public DataPacket LastDecodedData
        {
            get => _lastDecodedData;
            private set
            {
                _lastDecodedData = value;
                RightStick.SetValues(_lastDecodedData.RightArduino.StickX, _lastDecodedData.RightArduino.StickY);
                LeftStick.SetValues(_lastDecodedData.LeftArduino.StickX, _lastDecodedData.LeftArduino.StickY);
            }
        }

        public JoyStick RightStick
        {
            get; private set;
        }

        public JoyStick LeftStick
        {
            get; private set;
        }

        private DataPacket _lastDecodedData;

        public DataDecoder(SerialPort port)
        {
            _buffer = new byte[1 + 2 * ArduinoData.Size + 2 * MPUData.Size];
            _port = port;
            RightStick = new JoyStick(1023, 1023);
            LeftStick = new JoyStick(1023, 1023);
            _port.DataReceived += _port_DataReceived;
        }

        private byte[] GetBuffer(int offset)
        {
            var buffer = new byte[_buffer.Length];
            for (int i = 0, ii = offset; i < _buffer.Length; i++, ii = (ii + 1) % _buffer.Length)
            {
                buffer[i] = _buffer[ii];
            }
            return buffer;
        }

        public DataPacket? WaitForData()
        {
            var st = DateTime.Now;
            if (!IsAutoRefreshEnabled || !_requestSent)
            {
                //SendRequest();
            }
            while ((DateTime.Now - st).TotalMilliseconds <= 100) if (_isReady) return LastDecodedData;
            _requestSent = false;
            return null;
        }

        private void SetByte(byte b)
        {
            _buffer[_writePosition] = b;
            _writePosition = (_writePosition + 1) % _buffer.Length;
            if (_bufferOffset - _writePosition == 0)
            {
                OnDataRecieved();
            }
        }

        private void OnDataRecieved()
        {
            LastDecodedData = new DataPacket(_buffer);
            _isReady = true;
            _requestSent = false;
            DataDecoded?.Invoke(this, new DataDecodedEventArgs(LastDecodedData));
            if (IsAutoRefreshEnabled) WaitForData();
        }

        public void SendRequest()
        {
            if (!_port.IsOpen) _port.Open();
            _isReady = false;
            _port.Write(new[] { (byte)1 }, 0, 1);
            _requestSent = true;
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_port.BytesToRead > _buffer.Length + 4)
            {
                var i = 0;
                bool success = false;
                while (_port.BytesToRead > _buffer.Length + 4)
                {
                    var b = _port.ReadByte();
                    if (b == 255) 
                        i++;
                    else 
                        i = 0;
                    if (i >= 4)
                    {
                        var eb = _port.ReadByte();
                        if (eb != 255)
                        {
                            _buffer[0] = (byte)eb;
                            success = true;
                            if (eb != 0) ErrorFound?.Invoke(this, new ErrorFoundEventArgs((DataPacketError)eb));
                            break;
                        }
                        else i = 0;
                    }
                }
                if (!success) return;
                _port.Read(_buffer, 1, _buffer.Length - 1);
                OnDataRecieved();
                _port.DiscardInBuffer();
            }
            //else
            //{
            //    while (_port.BytesToRead > 0)
            //    {
            //        var b = _port.ReadByte();
            //        if (b != -1)
            //        {
            //            break;
            //        }
            //        SetByte((byte)b);
            //    }
            //}

            //var s = _port.ReadLine();
            //DataDecoded.Invoke(this, new DataDecodedEventArgs(s));
            //_port.DiscardInBuffer();
        }
    }
}
