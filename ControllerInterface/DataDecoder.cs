using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCToArduinoCommunication.Protocol;

namespace ControllerInterface
{
    public class DataDecodedEventArgs
    {
        public DataDecodedEventArgs(Data data)
        {
            Data = data;
        }

        public Data Data
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

        int _bufferOffset;
        private int _writePosition;

        private bool _isReady;
        public Data LastDecodedData
        {
            get;
            private set;
        }

        public DataDecoder(SerialPort port)
        {
            _buffer = new byte[9];
            _port = port;
            _port.DataReceived += _port_DataReceived;
        }

        private byte[] GetBuffer(int offset)
        {
            byte[] buffer = new byte[_buffer.Length];
            for (int i = 0, ii = offset; i < _buffer.Length; i++, ii = (ii + 1) % _buffer.Length)
            {
                buffer[i] = _buffer[ii];
            }
            return buffer;
        }

        public Data? WaitForData()
        {
            var st = DateTime.Now;
            SendRequest();
            while (!_isReady) if ((DateTime.Now - st).TotalMilliseconds > 100) return null;
            return LastDecodedData;
        }

        public bool Allign()
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                var data = new Data(GetBuffer(i));
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
            LastDecodedData = new Data(GetBuffer(_bufferOffset));
            _isReady = true;
            DataDecoded?.Invoke(this, new DataDecodedEventArgs(LastDecodedData));
        }

        public void SendRequest()
        {
            if (!_port.IsOpen) _port.Open();
            _isReady = false;
            _port.Write(new[] { (byte)0 }, 0, 1);
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_port.BytesToRead > _buffer.Length)
            {
                System.Threading.Thread.Sleep(1);

                while (_port.BytesToRead > _buffer.Length) _port.ReadByte();
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
