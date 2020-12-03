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
        public static int Size = 12;
        private byte[] _buffer;

        static Matrix4x4 _rot = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, (float)MathHelpers.DegToRad(90)) * Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, (float)MathHelpers.DegToRad(90));

        public Vector3 YawPitchRoll
        {
            get
            {
                if (_buffer == null) return new Vector3();
                var ypr = _buffer.ToVector3();
                return ypr;
            }
        }

        public Quaternion Quaternion
        {
            get
            {
                if (_buffer == null) return new Quaternion();
                var v = YawPitchRoll;
                var q = Quaternion.CreateFromYawPitchRoll(-v.X, v.Z, v.Y);
                //var z = q.Z;
                //q.Z = -q.Y;
                //q.Y = z;
                //var m = Matrix4x4.CreateFromQuaternion(q);
                //m = m * _rot;
                //return Quaternion.CreateFromRotationMatrix(m);
                return q;
            }
        }

        public MPUData(byte[] buffer)
        {
            _buffer = buffer;
        }
    }
}
