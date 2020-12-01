using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VRE.Vridge.API.Client.Helpers;

namespace ControllerInterface.Data
{
    public struct MPUData
    {
        public static int Size = 16;
        private byte[] _buffer;

        static Quaternion _rot = Quaternion.CreateFromRotationMatrix(Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, (float)MathHelpers.DegToRad(90)));

        public Quaternion Quaternion
        {
            get
            {
                if (_buffer == null) return new Quaternion();
                var q = _buffer.ToQuaternion();
                var z = q.Z;
                q.Z = -q.Y;
                q.Y = z;
                return q;
            }
        }

        public MPUData(byte[] buffer)
        {
            _buffer = buffer;
        }
    }
}
