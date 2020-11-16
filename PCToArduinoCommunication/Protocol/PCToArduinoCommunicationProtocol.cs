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

        public SerialPort Port
        {
            get => _port;
            set => _port = value;
        }

        public PCToArduinoCommunicationProtocol(SerialPort port)
        {
            _port = port;
            port.DataReceived += Port_DataReceived;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[Port.BytesToRead];
            Port.Read(buffer, 0, buffer.Length);
            Port.DiscardInBuffer();
            var deserializer = new BinaryDeserializer(buffer);
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
