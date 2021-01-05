using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;

namespace ControllerInterface.InterProcessCommunication
{
    public class InterProcessService : IDisposable
    {
        NamedPipeServerStream _inPipe;
        NamedPipeServerStream _outPipe;
        Thread _process;
        StreamReader _reader;
        StreamWriter _writer;

        public InterProcessService()
        {
            //_inPipe = new NamedPipeServerStream("ArduinoVRidgePropertiesIn", PipeDirection.In);
            _outPipe = new NamedPipeServerStream("ArduinoVRidgePropertiesOut", PipeDirection.Out);
            _process = new Thread(Process);
            //_reader = new StreamReader(_inPipe, Encoding.Default, false, 1024, true);
            _writer = new StreamWriter(_outPipe, Encoding.Default, 1024, true);
        }

        private void Process()
        {
            while (true)
            {
                try
                {
                    if (!_outPipe.IsConnected) _outPipe.WaitForConnection();
                    _writer.WriteLine("Hello World!");
                    _writer.Flush();
                    _outPipe.Flush();
                    Thread.Sleep(5000);
                }
                catch (IOException)
                {
                }
            }
        }

        public void Dispose()
        {
            _process?.Abort();
            _reader?.Dispose();
            _inPipe?.Dispose();
            _writer?.Dispose();
            _outPipe?.Dispose();
            _process = null;
        }

        public void Start()
        {
            if (_process == null) throw new ObjectDisposedException("InterProcessService was disposed!");
            _process.Start();
        }
    }
}
