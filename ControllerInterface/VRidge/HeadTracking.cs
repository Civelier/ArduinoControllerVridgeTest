using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ara3D;
using Microsoft.Kinect;
using VRidgeMessages = VRE.Vridge.API.Client.Messages;
using VRidgeRemotes = VRE.Vridge.API.Client.Remotes;

namespace ControllerInterface.VRidge
{
    public class HeadTracking
    {
        VRidgeRemotes.HeadRemote _head;
        public Vector3 Point
        {
            get;
            private set;
        }

        public HeadTracking(VRidgeRemotes.VridgeRemote remote)
        {
            _head = remote.Head;
        }

        public void SetData(Vector3 point)
        {
            Point = point;
            Update();
        }

        public void Update()
        {
            if (!_head?.IsDisposed ?? false) _head?.SetPosition(Point.X, Point.Y, Point.Z);
        }

        public void Recenter()
        {
            if (!_head?.IsDisposed ?? false) _head.Recenter();
        }
    }
}
