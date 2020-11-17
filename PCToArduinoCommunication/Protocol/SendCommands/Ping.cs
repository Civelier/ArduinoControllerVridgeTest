using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Protocol.SendCommands
{
    public class PingRepliedEventArgs : EventArgs
    {
        public uint Milliseconds { get; }
        public PingRepliedEventArgs(uint millis)
        {
            Milliseconds = millis;
        }
    }

    public class Ping : ISendCommand
    {
        public byte ID => 1;

        public bool ExpectsResponse => true;

        public event EventHandler<PingRepliedEventArgs> Replied;

        public void OnReply(BinaryDeserializer args)
        {
            Replied?.Invoke(this, new PingRepliedEventArgs(args.GetUInt()));
        }

        public BinarySerializer Send()
        {
            var serializer = new BinarySerializer();
            serializer.AppendValue(ID);
            serializer.InsertSizeinFront();
            return serializer;
        }
    }
}
