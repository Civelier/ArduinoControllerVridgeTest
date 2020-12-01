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
        VRidgeRemotes.VridgeRemote _remote;
        public SkeletonPoint Point
        {
            get;
            private set;
        }

        public HeadTracking(VRidgeRemotes.VridgeRemote remote)
        {
            _remote = remote;
        }

        public void SetData(SkeletonPoint point)
        {
            Point = point;
            Update();
        }

        public void Update()
        {
            _remote.Head?.SetPosition(Point.X, Point.Y, Point.Z);
        }
    }
}
