using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Protocol
{
    public class BinarySerializer
    {
        private List<byte> _data = new List<byte>();
        public IReadOnlyList<byte> Data => _data;

        public BinarySerializer()
        {

        }

        public void AppendValue(byte b)
        {
            _data.Add(b);
        }

        public void AppendValue(char c)
        {
            _data.Add((byte)c);
        }

        public void AppendValue(short s)
        {
            AppendValue(BitConverter.GetBytes(s));
        }

        public void AppendValue(ushort us)
        {
            AppendValue(BitConverter.GetBytes(us));
        }

        public void AppendValue(int i)
        {
            AppendValue(BitConverter.GetBytes(i));
        }

        public void AppendValue(uint ui)
        {
            AppendValue(BitConverter.GetBytes(ui));
        }

        public void AppendValue(long l)
        {
            AppendValue(BitConverter.GetBytes(l));
        }

        public void AppendValue(ulong ul)
        {
            AppendValue(BitConverter.GetBytes(ul));
        }

        public void AppendValue(float f)
        {
            AppendValue(BitConverter.GetBytes(f));
        }

        public void AppendValue(double d)
        {
            AppendValue(BitConverter.GetBytes(d));
        }

        public void AppendValue(IEnumerable<byte> bytes)
        {
            _data.AddRange(bytes);
        }

        public void AppendValue(string s)
        {
            AppendValue(Encoding.ASCII.GetBytes(s));
        }

        public void InsertSizeinFront()
        {
            _data.Insert(0, (byte)_data.Count);
        }
    }
}
