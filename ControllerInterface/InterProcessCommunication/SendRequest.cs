using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControllerInterface.InterProcessCommunication
{
    public class SendRequest : IInterProcessRequest
    {
        public SendRequest(InterProcessPacket packet)
        {
            Packet = packet;
        }

        public InterProcessPacket Packet
        {
            get;
        }

        public void Execute()
        {
            using (var pipe = new NamedPipeServerStream("ArduinoVRidgePropertiesOut", PipeDirection.Out))
            {
                pipe.WaitForConnection();
                using (var text = new StreamWriter(pipe, Encoding.Default, 1024, true))
                {
                    using (var writer = new JsonTextWriter(text))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, Packet);
                    }
                }
            }
        }
    }
}
