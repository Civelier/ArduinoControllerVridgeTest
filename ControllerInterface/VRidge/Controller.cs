using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllerInterface.Data;

namespace ControllerInterface.VRidge
{
    public class Controller
    {
        
        public Controller(ArduinoData controlsData, MPUData orientationData)
        {
            ControlsData = controlsData;
            OrientationData = orientationData;
        }

        public ArduinoData ControlsData { get; private set; }
        public MPUData OrientationData { get; private set; }

        public void SetData(ArduinoData ad, MPUData mpud)
        {
            ControlsData = ad;
            OrientationData = mpud;
        }

        public void Update()
        {
        }
    }
}
