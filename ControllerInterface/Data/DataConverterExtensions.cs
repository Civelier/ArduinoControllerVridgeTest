using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.Data
{
    public static class DataConverterExtensions
    {
        public static Quaternion ToQuaternion(this byte[] buffer, int start = 0)
        {
            var w = BitConverter.ToSingle(buffer, start);
            var x = BitConverter.ToSingle(buffer, start + 4);
            var y = BitConverter.ToSingle(buffer, start + 8);
            var z = BitConverter.ToSingle(buffer, start + 12);
            return new Quaternion(x, y, z, w);
        }

        public static Vector3 ToVector3(this byte[] buffer, int start = 0)
        {
            var x = BitConverter.ToSingle(buffer, start);
            var y = BitConverter.ToSingle(buffer, start + 4);
            var z = BitConverter.ToSingle(buffer, start + 8);
            return new Vector3(x, y, z);
        }

        public static byte[] GetRange(this byte[] buffer, int start, int count)
        {
            int i = 0, ii = start + count;
            if (buffer == null) return null;
            var query = from b in buffer
                        where i++ >= start && i <= ii
                        select b;
            return query.ToArray();
        }
    }
}
