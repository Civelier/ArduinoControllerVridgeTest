using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using VRidgeMessages = VRE.Vridge.API.Client.Messages;
using VRidgeRemotes = VRE.Vridge.API.Client.Remotes;

namespace ControllerInterface.VRidge
{
    public class HeadTracking
    {
        VRidgeRemotes.HeadRemote _head;
        public SkeletonPoint Point
        {
            get;
            private set;
        }

        public HeadTracking(VRidgeRemotes.VridgeRemote remote)
        {
            _head = remote.Head;
        }

        public void SetData(SkeletonPoint point)
        {
            Point = point;
            Update();
        }

        public void Update()
        {
            if (!_head.IsDisposed) _head?.SetPosition(Point.X, Point.Y, Point.Z);
        }
    }
}
