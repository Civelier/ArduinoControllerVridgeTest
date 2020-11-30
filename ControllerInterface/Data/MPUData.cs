using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.Data
{
    public struct MPUData
    {
        public static int Size = 16;
        private byte[] _buffer;

        public Quaternion Quaternion
        {
            get
            {
                return _buffer.ToQuaternion();
                //q.Y = -q.Y;
                //return q;
            }
        }

        public MPUData(byte[] buffer)
        {
            _buffer = buffer;
        }
    }
}
