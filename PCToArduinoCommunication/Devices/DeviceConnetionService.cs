using System;
using System.Collections.Generic;
using Port = System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCToArduinoCommunication.Protocol;
using PCToArduinoCommunication.Protocol.SendCommands;
using System.Diagnostics;

namespace PCToArduinoCommunication.Devices
{
    public class DeviceConnetionService
    {
        internal static DeviceConnetionService _instance = new DeviceConnetionService();
        public PCToArduinoCommunicationProtocol LeftControllerPort = new PCToArduinoCommunicationProtocol();
        public PCToArduinoCommunicationProtocol RightControllerPort = new PCToArduinoCommunicationProtocol();
        private IControllerDevice _leftController;
        private IControllerDevice _rightController;

        public static DeviceConnetionService Instance => _instance;

        public DeviceConnetionService()
        {

        }

        public void Begin(IControllerDevice leftController, IControllerDevice rightController)
        {
            _leftController = leftController;
            _rightController = rightController;
        }

        private void Send(byte[] data, Port.SerialPort port)
        {
            if (!port.IsOpen) return;
            port.Write(data, 0, data.Length);
        }

        public void Send(ISendCommand command, Port.SerialPort port)
        {
            var s = command.Send();
            Send(s.Data.ToArray(), port);
            if (command.ExpectsResponse)
            {
            }
        }

        enum HandshakeAttemptResult
        {
            NoReply,
            RecievingData,
            TimedOut,
            Failed,
            Sucess,
            InvalidDevice
        }

        private IEnumerator<Tuple<HandshakeAttemptResult, IControllerDevice>> TryHandshakeToDevice(Port.SerialPort port)
        {
            var handshake = new HandshakeCommand();
            Stopwatch stopwatch = new Stopwatch();

            Send(handshake, port);
            stopwatch.Start();
            byte incommingMessageSize = 0;
            List<byte> incommingMessage = new List<byte>();
            do
            {
                while (port.BytesToRead > 0)
                {
                    int b = port.ReadByte();
                    if (b == -1 || incommingMessageSize != 0 && incommingMessage.Count >= incommingMessageSize)
                    {
                        break;
                    }
                    else
                    {
                        if (incommingMessageSize == 0) incommingMessageSize = (byte)(b - 1);
                        else incommingMessage.Add((byte)b);
                    }
                    yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.RecievingData, null);
                }
                if (stopwatch.ElapsedMilliseconds >= 500) yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.TimedOut, null);
            } while (incommingMessageSize != 0 && incommingMessage.Count >= incommingMessageSize);
            var deserializer = new BinaryDeserializer(incommingMessage.ToArray());
            byte id = 0;
            bool failed = false;
            try
            {
                id = deserializer.GetByte();
            }
            catch (IndexOutOfRangeException)
            {
                failed = true;
            }

            if (failed)
            {
                yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.InvalidDevice, null);
            }
            else
            {
                if (id == 0)
                {
                    var ac1 = deserializer.GetByte();
                    var ac2 = deserializer.GetByte();
                    if (ac1 == ProtocolInfo.AccessCode[0] && ac2 == ProtocolInfo.AccessCode[1])
                    {
                        byte deviceID = deserializer.GetByte();
                        if (ProtocolInfo.Devices.ContainsKey(deviceID)) yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.Sucess, ProtocolInfo.Devices[deviceID]);
                        else yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.Failed, null);

                    }
                    else yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.InvalidDevice, null);
                }
                if (id == 255)
                {
                    var errorCode = deserializer.GetByte();
                    Console.WriteLine($"Error encountered : {errorCode}");
                    yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.Failed, null);
                }
            }

            yield return new Tuple<HandshakeAttemptResult, IControllerDevice>(HandshakeAttemptResult.TimedOut, null);
        }

        public IEnumerator<bool> Connect()
        {
            while (true)
            {
                if (!LeftControllerPort.IsConnected || !RightControllerPort.IsConnected)
                {
                    var portNames = Port.SerialPort.GetPortNames();
                    Port.SerialPort port = new Port.SerialPort("COM1", 115200);
                    foreach (var portName in portNames)
                    {
                        if (!(LeftControllerPort.IsConnected && LeftControllerPort.Port.PortName == portName) &&
                            !(RightControllerPort.IsConnected && RightControllerPort.Port.PortName == portName))
                        port.PortName = portName;
                        if (!port.IsOpen)
                        {
                            port.Open();
                        }

                        using (var tryConnect = TryHandshakeToDevice(port))
                        {
                            bool interrupt = false;
                            while (!interrupt)
                            {
                                interrupt = !tryConnect.MoveNext();
                                switch (tryConnect.Current.Item1)
                                {
                                    case HandshakeAttemptResult.NoReply:
                                    case HandshakeAttemptResult.RecievingData:
                                        break;
                                    case HandshakeAttemptResult.TimedOut:
                                    case HandshakeAttemptResult.Failed:
                                    case HandshakeAttemptResult.Sucess:
                                    case HandshakeAttemptResult.InvalidDevice:
                                        interrupt = true;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            if (tryConnect.Current.Item2 != null)
                            {
                                if (tryConnect.Current.Item2.Name == "Left controller")
                                {
                                    LeftControllerPort.Port = port;
                                    port = new Port.SerialPort("COM1", 115200);
                                }
                                if (tryConnect.Current.Item2.Name == "Right controller")
                                {
                                    RightControllerPort.Port = port;
                                    port = new Port.SerialPort("COM1", 115200);
                                }
                            }
                            else port.Close();
                        }
                    }
                }
                yield return false;
            }
        }
    }
}
