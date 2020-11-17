using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Timers;
using PCToArduinoCommunication.Protocol.SendCommands;

namespace PCToArduinoCommunication.Protocol
{
    public class PCToArduinoCommunicationProtocol
    {
        public const double ReplyTimeout = 1000;
        private Timer _replyTimer = new Timer(ReplyTimeout);
        private SerialPort _port;
        private ISendCommand _lastCommandSent;
        private List<byte> _incommingMessage = new List<byte>();
        private byte _incommingMessageSize;

        public SerialPort Port
        {
            get => _port;
            set => _port = value;
        }

        public PCToArduinoCommunicationProtocol(SerialPort port)
        {
            _port = port;
            _port.DiscardNull = false;
            port.DataReceived += Port_DataReceived;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (Port.BytesToRead > 0)
            {
                int b = Port.ReadByte();
                if (b == -1 || _incommingMessageSize != 0 && _incommingMessage.Count >= _incommingMessageSize)
                {
                    break;
                }
                else
                {
                    if (_incommingMessageSize == 0) _incommingMessageSize = (byte)(b - 1);
                    else _incommingMessage.Add((byte)b);
                }
            }
            if (_incommingMessageSize != 0 && _incommingMessage.Count >= _incommingMessageSize)
            {
                var deserializer = new BinaryDeserializer(_incommingMessage.ToArray());
                var id = deserializer.GetByte();
                if (id == 0)
                {
                    _lastCommandSent?.OnReply(deserializer);
                }
                if (id == 255)
                {
                    var errorCode = deserializer.GetByte();
                    Console.WriteLine($"Error encountered : {errorCode}");
                }
                _incommingMessage.Clear();
                _incommingMessageSize = 0;
            }
        }

        private void Send(byte[] data)
        {
            if (!Port.IsOpen) return;
            Port.Write(data, 0, data.Length);
        }

        public void Send(ISendCommand command)
        {
            var s = command.Send();
            Send(s.Data.ToArray());
            _lastCommandSent = command;
            if (command.ExpectsResponse)
            {
            }
        }
    }
}
