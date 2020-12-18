using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllerInterface.Data;

namespace ControllerInterface.DataTypes
{
    [Serializable]
    public struct ControllersConnectionStatus
    {
        [ReadOnly(true)]
        public DataPacketError DataPacketError
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public bool IsConnected
        {
            get;
            set;
        }

        [ReadOnly(true)]
        public string CurrentPort
        {
            get;
            set;
        }
    }
}
