using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.InterProcessCommunication
{
    public class SendCloseRequest : ISingleInstanceInterProcessRequest
    {
        public string ID => "SendClose";

        public bool RequireSuccess => false;

        public bool Execute()
        {
            using (var pipe = new NamedPipeServerStream("ArduinoVRidgePropertiesOut", PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
            {
                pipe.WaitForConnectionEx(20);
                if (pipe.IsConnected)
                {
                    using (var text = new StreamWriter(pipe, Encoding.Default, 1024, true))
                    {
                        using (var writer = new JsonTextWriter(text))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(writer, InterProcessPacket.CreateClose());
                        }
                    }
                    return true;
                }
                else return false;
            }
        }

        public bool IsInstance(ISingleInstanceInterProcessRequest other)
        {
            throw new NotImplementedException();
        }
    }
}
