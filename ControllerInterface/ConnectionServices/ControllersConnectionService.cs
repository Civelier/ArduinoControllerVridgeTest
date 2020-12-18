using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using ControllerInterface.Data;
using ControllerInterface.DataTypes;

namespace ControllerInterface.ConnectionServices
{
    public enum PortPingResult
    {
        Sucess,
        PortDisposed,
        IOException,
        PortAlreadyOpen,
        IncorrectDevice,
        PortNull,
    }

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

    public delegate void DataDecodedEventHandler(ControllersConnectionService sender, DataDecodedEventArgs args);

    public delegate void ErrorFoundEventHandler(ControllersConnectionService sender, ErrorFoundEventArgs args);

    public class ErrorFoundEventArgs
    {
        public DataPacketError Error
        {
            get;
        }

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

    public class ControllersConnectionService : IDisposable
    {
        Thread _process;
        SerialPort _activePort;
        byte[] _calibrateDMP = { 1 };
        byte[] _calibrateOffsets = { 2 };
        byte[] _ping = { 3 };

        private enum CommandQueue
        {
            None,
            CalibrateDMP,
            CalibrateOffsets,
            Ping,
        }

        private CommandQueue _command
        {
            get;
            set;
        }

        byte[] _sig =
        {
            12,
            35,
            253,
            95,
            129,
        };

        bool _lastSigMatched;

        public ControllersConnectionStatus ControllersStatus
        {
            get;
            private set;
        }
        private string portName
        {
            get => _activePort?.PortName;
            set
            {
                if (_activePort == null)
                {
                    InitializePort();
                }
                else
                {
                    if (_activePort.IsOpen) _activePort.Close();
                    _activePort.PortName = value;
                    if (!_activePort.IsOpen) _activePort.Open();
                }
            }
        }

        private void InitializePort()
        {
            var ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                _activePort = new SerialPort(ports[0], 115200);
                _activePort.DataReceived += _activePort_DataReceived;
                _activePort.Open();
            }
        }

        public ControllersConnectionService()
        {
            _process = new Thread(MainProcess);
            _buffer = new byte[1 + 2 * ArduinoData.Size + 2 * MPUData.Size];
            InitializePort();
            RightStick = new JoyStick(1023, 1023);
            LeftStick = new JoyStick(1023, 1023);
        }

        void MainProcess()
        {
            while (_process.ThreadState != ThreadState.AbortRequested)
            {
                switch (_command)
                {
                    case CommandQueue.CalibrateDMP:
                        _command = CommandQueue.None;
                        break;
                    case CommandQueue.CalibrateOffsets:
                        _command = CommandQueue.None;
                        break;
                    default:
                        break;
                }
            }
        }

        void PingPorts()
        {
        
        }

        bool Send(byte[] buffer)
        {
            if (_activePort == null) return false;
            if (!_activePort.IsOpen) return false;
            _activePort.Write(buffer, 0, buffer.Length);
            return true;
        }

        public PortPingResult Ping(string port)
        {
            try
            {
                if (port == null) return PortPingResult.PortNull;
                portName = port;
                if (_activePort.IsOpen) return PortPingResult.PortAlreadyOpen;
                _activePort.Write(_ping, 0, 1);
                Thread.Sleep(5);
                _activePort.DiscardInBuffer();
                _command = CommandQueue.Ping;
                while (_command == CommandQueue.Ping)
                {
                    Thread.Sleep(10);
                }
                if (_lastSigMatched) return PortPingResult.Sucess;
                else return PortPingResult.IncorrectDevice;
            }
            catch (InvalidOperationException)
            {
                return PortPingResult.IOException;
            }
        }

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
            if (!_activePort.IsOpen) _activePort.Open();
            _isReady = false;
            _activePort.Write(new[] { (byte)1 }, 0, 1);
            _requestSent = true;
        }

        private void _activePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_activePort.BytesToRead > _buffer.Length + 4)
                {
                    var i = 0;
                    bool success = false;
                    while (_activePort.BytesToRead > _buffer.Length + 4)
                    {
                        if (!_activePort.IsOpen) return;
                        var b = _activePort.ReadByte();
                        if (b == 255)
                            i++;
                        else
                            i = 0;
                        if (i >= 4)
                        {
                            if (!_activePort.IsOpen) return;
                            var eb = _activePort.ReadByte();
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
                    if (!_activePort.IsOpen) return;
                    _activePort.Read(_buffer, 1, _buffer.Length - 1);
                    OnDataRecieved();
                    _activePort.DiscardInBuffer();
                }
            }
            catch (InvalidOperationException)
            {

            }
            //else
            //{
            //    while (_activePort.BytesToRead > 0)
            //    {
            //        var b = _activePort.ReadByte();
            //        if (b != -1)
            //        {
            //            break;
            //        }
            //        SetByte((byte)b);
            //    }
            //}

            //var s = _activePort.ReadLine();
            //DataDecoded.Invoke(this, new DataDecodedEventArgs(s));
            //_activePort.DiscardInBuffer();
        }

        public void Dispose()
        {
            _process.Abort();
            _activePort?.Dispose();
        }
    }
}
