using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Protocol.SendCommands
{
    public interface ISendCommand
    {
        byte ID { get; }
        bool ExpectsResponse { get; }
        BinarySerializer Send();
        void OnReply(BinaryDeserializer args);
    }
}
