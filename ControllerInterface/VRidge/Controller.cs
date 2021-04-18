using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ara3D;
using ControllerInterface.Data;
using ControllerInterface.DataTypes;
using Microsoft.Kinect;
using VRidgeAPI = VRE.Vridge.API.Client;
using VRidgeMessages = VRE.Vridge.API.Client.Messages;
using VRidgeRemotes = VRE.Vridge.API.Client.Remotes;

namespace ControllerInterface.VRidge
{
    public class Controller
    {
        VRidgeRemotes.ControllerRemote _controller;
        private JoyStick _joyStick;

        public VRidgeMessages.BasicTypes.HandType Hand { get; }
        public bool IsDisposed => _controller?.IsDisposed ?? true;
        public Controller(VRidgeRemotes.VridgeRemote remote, VRidgeMessages.BasicTypes.HandType hand)
        {
            _controller = remote.Controller;
            Hand = hand;
            _joyStick = new JoyStick(1023, 1023, true, true);
        }

        public ArduinoData ControlsData { get; private set; }
        public MPUData OrientationData { get; private set; }
        public Vector3 Point { get; private set; }


        public void SetData(ArduinoData ad, MPUData mpud)
        {
            ControlsData = ad;
            OrientationData = mpud;
            Update();
        }

        public void SetData(Vector3 point)
        {
            Point = point;
            Update();
        }

        public void Update()
        {

            if (!_controller?.IsDisposed ?? false)
            {
                //----------- Only for debugging with non-functionnal left controller -----------
                bool stick = ControlsData.Stick && Hand == VRidgeMessages.BasicTypes.HandType.Right;
                //bool stick = ControlsData.Stick;
                bool trigger = ControlsData.Button1;

                
                _joyStick.SetValues(ControlsData.StickX, ControlsData.StickY);

                // Set controller data
                _controller?.SetControllerState(Hand == VRidgeMessages.BasicTypes.HandType.Right ? 0 : 1,
                    VRidgeMessages.v3.Controller.HeadRelation.Unrelated, Hand,
                    OrientationData.Quaternion, new System.Numerics.Vector3(Point.X, Point.Y + PropertiesData.Instance.Height, Point.Z),
                    _joyStick.X, _joyStick.Y,
                    trigger ? 1 : 0,
                    ControlsData.Menu, false,
                    trigger,
                    ControlsData.Button2 || ControlsData.Button3 || ControlsData.Button4,
                    stick, stick);
            }
        }
    }
}
