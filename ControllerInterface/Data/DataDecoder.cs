using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCToArduinoCommunication.Protocol;

namespace ControllerInterface.Data
{
    public class DataDecodedEventArgs
    {
        public DataDecodedEventArgs(ArduinoData data)
        {
            Data = data;
        }

        public ArduinoData Data
        {
            get;
        }
    }

    public delegate void DataDecodeEventHandler(DataDecoder sender, DataDecodedEventArgs args);

    public class DataDecoder
    {
        SerialPort _port;

        public event DataDecodeEventHandler DataDecoded;

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
        public ArduinoData LastDecodedData
        {
            get => _lastDecodedData;
            private set
            {
                _lastDecodedData = value;
                RightStick.SetValues(_lastDecodedData.StickX, _lastDecodedData.StickY);
            }
        }

        public JoyStick RightStick
        {
            get; private set;
        }

        private ArduinoData _lastDecodedData;

        public DataDecoder(SerialPort port)
        {
            _buffer = new byte[5];
            _port = port;
            RightStick = new JoyStick(800, 800);
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

        public ArduinoData? WaitForData()
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

        public bool Allign()
        {
            for (var i = 0; i < _buffer.Length; i++)
            {
                var data = new ArduinoData(GetBuffer(i));
                if (430 < data.StickX && data.StickX < 470 && 430 < data.StickY && data.StickY < 470)
                {
                    _bufferOffset = i;
                    return true;
                }
            }
            return false;
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
            LastDecodedData = new ArduinoData(GetBuffer(_bufferOffset));
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
            if (_port.BytesToRead > _buffer.Length)
            {
                var i = 0;
                while (_port.BytesToRead > _buffer.Length)
                {
                    var b = _port.ReadByte();
                    if (b == 255) i++;
                    else i = 0;
                    if (i >= 4) break;
                }
                _port.Read(_buffer, 0, _buffer.Length);
                OnDataRecieved();
                _port.DiscardInBuffer();
            }
            else
            {
                while (_port.BytesToRead > 0)
                {
                    var b = _port.ReadByte();
                    if (b != -1)
                    {
                        break;
                    }
                    SetByte((byte)b);
                }
            }

            //var s = _port.ReadLine();
            //DataDecoded.Invoke(this, new DataDecodedEventArgs(s));
            //_port.DiscardInBuffer();
        }
    }
}
