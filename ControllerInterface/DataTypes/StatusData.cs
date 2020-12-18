using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace ControllerInterface.DataTypes
{
    [Serializable]
    public class StatusData
    {
        static internal StatusData _instance = new StatusData();
        static public StatusData Instance => _instance;

        [ReadOnly(true)]
        public KinectStatus KinectStatus { get; set; }
        [ReadOnly(true)]
        public int NumberOfAvailableSerialPorts { get; set; }
        [ReadOnly(true)]
        public ControllersConnectionStatus ControllersStatus { get; set; }
    }
}
