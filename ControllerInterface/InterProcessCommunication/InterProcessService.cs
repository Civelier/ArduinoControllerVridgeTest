using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using Newtonsoft.Json;

namespace ControllerInterface.InterProcessCommunication
{
    public class PacketRecievedEventArgs
    {
        public PacketRecievedEventArgs(InterProcessPacket packet)
        {
            Packet = packet;
        }

        public InterProcessPacket Packet
        {
            get;
        }
    }

    public delegate void PacketRecievedEventHandler(InterProcessService sender, PacketRecievedEventArgs args);


    public class InterProcessService : IDisposable
    {
        public event PacketRecievedEventHandler PacketRecieved;

        private Queue<IInterProcessRequest> _requests = new Queue<IInterProcessRequest>();
        private Thread _process;

        public InterProcessService()
        {
            //_inPipe = new NamedPipeServerStream("ArduinoVRidgePropertiesIn", PipeDirection.In);
            //_outPipe = new NamedPipeServerStream("ArduinoVRidgePropertiesOut", PipeDirection.Out);
            _process = new Thread(Process);
            //_reader = new StreamReader(_inPipe, Encoding.Default, false, 1024, true);
            //_writer = new StreamWriter(_outPipe, Encoding.Default, 1024, true);
        }


        public void Request(IInterProcessRequest request)
        {
            //request.Execute();
            lock (_requests)
            {
                _requests.Enqueue(request);
            }
        }

        private void ProcessRequests()
        {
            lock (_requests)
            {
                while (_requests.Count > 0)
                {
                    _requests.Dequeue().Execute();
                }
            }
        }

        private void Process()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        ProcessRequests();
                        using (var pipe = new NamedPipeServerStream("ArduinoVRidgePropertiesIn", PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
                        {
                            pipe.WaitForConnectionEx(100);
                            if (pipe.IsConnected)
                            {
                                using (var text = new StreamReader(pipe, Encoding.Default, false, 1024, true))
                                {
                                    using (var reader = new JsonTextReader(text))
                                    {
                                        var serializer = new JsonSerializer();
                                        var packet = serializer.Deserialize<InterProcessPacket>(reader);
                                        OnPacketRecieved(packet);
                                    }
                                }
                            }
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
            catch (ThreadAbortException)
            {
                //if (res != null)
                //{
                //    var pipe = (NamedPipeServerStream)res.AsyncState;
                //    pipe.EndWaitForConnection(res);
                //}
                using (var pipe = new NamedPipeServerStream("ArduinoVRidgePropertiesOut", PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
                {
                    var t = DateTime.Now;
                    pipe.WaitForConnectionEx(1000);
                    using (var text = new StreamWriter(pipe, Encoding.Default, 1024, true))
                    {
                        using (var writer = new JsonTextWriter(text))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(writer, new InterProcessPacket() { Close = true });
                        }
                    }
                }
            }
        }

        void ConnectionCallback(IAsyncResult res)
        {
            try
            {
                var pipe = (NamedPipeServerStream)res.AsyncState;
                using (var text = new StreamReader(pipe, Encoding.Default, false, 1024, true))
                {
                    using (var reader = new JsonTextReader(text))
                    {
                        var serializer = new JsonSerializer();
                        var packet = serializer.Deserialize<InterProcessPacket>(reader);
                        OnPacketRecieved(packet);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        void OnPacketRecieved(InterProcessPacket packet)
        {
            PacketRecieved?.Invoke(this, new PacketRecievedEventArgs(packet));
        }

        public void Dispose()
        {
            _process?.Abort();
            _process = null;
        }

        public void Start()
        {
            if (_process == null) throw new ObjectDisposedException("InterProcessService was disposed!");
            _process.Start();
        }
    }
}
