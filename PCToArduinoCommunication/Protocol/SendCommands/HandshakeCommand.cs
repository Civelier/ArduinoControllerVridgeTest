using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Protocol.SendCommands
{
    public enum DeviceType : byte
    {
        LeftController = 0,
        RightController = 1,
        TestDevice = 255,
    }

    public class HandshakeRepliedEventArgs : EventArgs
    {
        private DeviceType _type;
        private byte[] _version;

        public DeviceType Type => _type;
        public byte[] ProtocolVersion => _version;

        public HandshakeRepliedEventArgs(DeviceType type)
        {
            _type = type;
        }
    }

    public class HandshakeCommand : ISendCommand
    {
        private byte _id;

        public byte ID => _id;

        public bool ExpectsResponse => true;

        public event EventHandler<HandshakeRepliedEventArgs> Replied;

        public HandshakeCommand()
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
