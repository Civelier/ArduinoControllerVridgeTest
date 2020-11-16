using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Protocol.SendCommands
{
    public enum DeviceType
    {
        LeftController = 0,
        RightController = 1,
    }

    public class HandshakeRepliedEventArgs : EventArgs
    {
        private DeviceType _type;

        public DeviceType Type => _type;

        public HandshakeRepliedEventArgs(DeviceType type)
        {
            _type = type;
        }
    }

    public class Handshake : ISendCommand
    {
        private byte _id;

        public byte ID => _id;

        public bool ExpectsResponse => true;

        public event EventHandler<HandshakeRepliedEventArgs> Replied;

        public Handshake()
        {
            _id = 0;
        }

        public BinarySerializer Send()
        {
            var s = new BinarySerializer();
            s.AppendValue(ID);
            s.InsertSizeinFront();
            return s;
        }

        public void OnReply(BinaryDeserializer args)
        {
            int v = args.GetByte();
            Replied?.Invoke(this, new HandshakeRepliedEventArgs((DeviceType)v));
        }
    }
}
