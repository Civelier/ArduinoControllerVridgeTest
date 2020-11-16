using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCToArduinoCommunication.Protocol
{
    public class BinaryDeserializer
    {
        private int _index = 0;
        private byte[] _data;

        public BinaryDeserializer(byte[] data)
        {
            _data = data;
        }

        void Increment(int i)
        {
            _index += i;
        }

        public byte GetByte()
        {
            var v = _data[_index];
            _index++;
            return v;
        }

        public char GetChar(char c)
        {
            var v = _data[_index];
            _index++;
            return (char)v;
        }

        public short GetShort(short s)
        {
            var v = BitConverter.ToInt16(_data, _index);
            Increment(2);
            return v;
        }

        public ushort GetUShort(ushort us)
        {
            var v = BitConverter.ToUInt16(_data, _index);
            Increment(2);
            return v;
        }

        public int GetInt(int i)
        {
            var v = BitConverter.ToInt32(_data, _index);
            Increment(4);
            return v;
        }

        public uint GetUInt(uint ui)
        {
            var v = BitConverter.ToUInt32(_data, _index);
            Increment(4);
            return v;
        }

        public long GetLong(long l)
        {
            var v = BitConverter.ToInt64(_data, _index);
            Increment(8);
            return v;
        }

        public ulong GetULong(ulong ul)
        {
            var v = BitConverter.ToUInt64(_data, _index);
            Increment(2);
            return v;
        }

        public float GetFloat(float f)
        {
            var v = BitConverter.ToSingle(_data, _index);
            Increment(4);
            return v;
        }

        public double GetDouble(double d)
        {
            var v = BitConverter.ToDouble(_data, _index);
            Increment(8);
            return v;
        }

        public IEnumerable<byte> GetBytes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return _data[_index];
                _index++;
            }
        }

        public IEnumerable<char> GetChars(int count)
        {
            foreach (var c in GetBytes(count))
            {
                yield return (char)c;
            }
        }

        public string GetString(int count)
        {
            string v = new string(GetChars(count).ToArray());
            return v;
        }
    }
}
