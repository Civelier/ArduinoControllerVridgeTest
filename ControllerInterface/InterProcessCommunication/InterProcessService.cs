using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

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
        private List<ISingleInstanceInterProcessRequest> _singleInstanceRequests = new List<ISingleInstanceInterProcessRequest>();
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
            lock (_requests)
            {
                _requests.Enqueue(request);
            }
        }

        public void Request(ISingleInstanceInterProcessRequest request)
        {
            lock (_singleInstanceRequests)
            {
                _singleInstanceRequests.RemoveAll((m) => request.IsInstance(m));
                _singleInstanceRequests.Add(request);
            }
        }

        private void ProcessRequests()
        {
            var requests = new Queue<IInterProcessRequest>();
            lock (_requests)
            {
                while (_requests.Count > 0)
                {
                    var r = _requests.Dequeue();
                    if (!r.Execute() && r.RequireSucess) requests.Enqueue(r);
                }
                _requests = requests;
            }

            var singleInstanceRequests = new List<ISingleInstanceInterProcessRequest>();
            lock (_singleInstanceRequests)
            {
                while (_singleInstanceRequests.Count > 0)
                {
                    var r = _singleInstanceRequests.First();
                    if (!r.Execute() && r.RequireSuccess) singleInstanceRequests.Add(r);
                    _singleInstanceRequests.Remove(r);
                }
                _singleInstanceRequests = singleInstanceRequests;
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
                    catch (IOException e)
                    {
                        Debug.WriteLine("InterProcessService process threw an IOException: " + e.Message);
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
                try
                {
                    using (var pipe = new NamedPipeServerStream("ArduinoVRidgePropertiesOut", PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
                    {
                        var t = DateTime.Now;
                        pipe.WaitForConnectionEx(1000);
                        if (pipe.IsConnected)
                        {
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
                catch (IOException e)
                {
                    Debug.WriteLine("Closing unity app message threw an IOException: " + e.Message);
                }
                Debug.WriteLine("InterProcessService thread was aborted");
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
            catch (ObjectDisposedException e)
            {
                Debug.WriteLine(e.Message);
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
