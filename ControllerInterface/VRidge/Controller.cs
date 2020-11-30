using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllerInterface.Data;
using VRidgeAPI = VRE.Vridge.API.Client;
using VRidgeMessages = VRE.Vridge.API.Client.Messages;
using VRidgeRemotes = VRE.Vridge.API.Client.Remotes;

namespace ControllerInterface.VRidge
{
    public class Controller
    {
        VRidgeRemotes.VridgeRemote _remote;
        public VRidgeMessages.BasicTypes.HandType Hand { get; }
        public Controller(VRidgeRemotes.VridgeRemote remote, VRidgeMessages.BasicTypes.HandType hand)
        {
            _remote = remote;
            Hand = hand;
        }

        public ArduinoData ControlsData { get; private set; }
        public MPUData OrientationData { get; private set; }

        public void SetData(ArduinoData ad, MPUData mpud)
        {
            ControlsData = ad;
            OrientationData = mpud;
            Update();
        }

        public void Update()
        {
            _remote.Controller?.SetControllerState(Hand == VRidgeMessages.BasicTypes.HandType.Right ? 0 : 1,
                VRidgeMessages.v3.Controller.HeadRelation.Unrelated, Hand,
                OrientationData.Quaternion, new System.Numerics.Vector3(0, -0.2f, 0),
                ControlsData.StickX, ControlsData.StickY,
                ControlsData.Button1 && !(ControlsData.Button2 || ControlsData.Button3 || ControlsData.Button4) ? 1 : 0,
                false, false,
                ControlsData.Button1 && !(ControlsData.Button2 || ControlsData.Button3 || ControlsData.Button4),
                ControlsData.Button2 && ControlsData.Button3 && ControlsData.Button4,
                ControlsData.Stick, ControlsData.Stick);
        }
    }
}
