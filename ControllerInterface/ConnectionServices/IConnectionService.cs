using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllerInterface.DataTypes;
using Microsoft.Kinect;

namespace ControllerInterface.ConnectionServices
{
    public interface IConnectionService : IDisposable
    {
        ControllersConnectionStatus ControllersStatus { get; }
        KinectStatus KinectStatus { get; }

        void StartServices();
        void StopServices();
    }
}
