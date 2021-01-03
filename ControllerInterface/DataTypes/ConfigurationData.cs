using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.DataTypes
{
    public class ConfigurationData
    {
        private static ConfigurationData _instance = new ConfigurationData();
        public static ConfigurationData Instance => _instance;


        private float _height = 1.6f;

        public float Height
        {
            get => _height;
            set => _height = value;
        }

    }
}
