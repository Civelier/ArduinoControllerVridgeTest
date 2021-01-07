using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.InterProcessCommunication
{
    [Serializable]
    public class InterProcessPacket
    {
        public bool SetForward
        {
            get;
            set;
        }
        public bool Close
        {
            get;
            set;
        }
        public float Height
        {
            get;
            set;
        }
        public float RotationOffset
        {
            get;
            set;
        }

        public InterProcessPacket()
        {
        }

        public InterProcessPacket(float height, float rotationOffset)
        {
            Height = height;
            RotationOffset = rotationOffset;
        }

        public static InterProcessPacket CreateClose()
        {
            return new InterProcessPacket() { Close = true };
        }
    }
}
