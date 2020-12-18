using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerInterface.Data;
using System.Runtime.InteropServices;
using VRidgeAPI = VRE.Vridge.API.Client;
using ControllerInterface.VRidge;
using VRE.Vridge.API.Client.Remotes;
using ControllerInterface.Kinect;
using Ara3D;

namespace ControllerInterface
{
    public partial class Form1 : Form
    {
        private static Queue<Action> _mainThreadActionQueue = new Queue<Action>();
        private static Action _singleAction;
        //private DataDecoder _decoder;
        //private bool _errorWindow = false;
        //private string _errorMsg;
        private VridgeRemote _remote;
        private Controller _rightController;
        private Controller _leftController;
        private HeadTracking _head;
        private KinectDevice _kinect;

        private const uint GW_HWNDFIRST = 0;
        private const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public static void QueueActionOnMainThread(Action action)
        {
            lock (_mainThreadActionQueue)
            {
                _mainThreadActionQueue.Enqueue(action);
            }
        }

        private static void SetActionOnMainThread(Action action)
        {
            lock (_mainThreadActionQueue)
            {
                _singleAction = action;
            }
        }

        public Form1()
        {
            InitializeComponent();
            //_decoder.IsAutoRefreshEnabled = true;
            //_decoder.ErrorFound += _decoder_ErrorFound;
            //_decoder.DataDecoded += _decoder_DataDecoded1;

            _kinect = new KinectDevice();
            _kinect.StartKinectProcess();
            _kinect.NewSkeletonFrameReady += _kinect_NewSkeletonFrameReady;
        }

        private void _kinect_NewSkeletonFrameReady(KinectDevice sender, KinectNewSkeletonFrameReadyEventArgs args)
        {
            _rightController?.SetData(args?.RightHand ?? new Vector3());
            _leftController?.SetData(args?.LeftHand ?? new Vector3());
            _head?.SetData(args?.Head ?? new Vector3());
        }

        private void _decoder_DataDecoded1(DataDecoder sender, DataDecodedEventArgs args)
        {
            _rightController?.SetData(args.Data.RightArduino, args.Data.RightMPU);
            _leftController?.SetData(args.Data.LeftArduino, args.Data.LeftMPU);
        }

        private void _decoder_ErrorFound(DataDecoder sender, ErrorFoundEventArgs args)
        {
            QueueActionOnMainThread(() => 
            {
                MessageBox.Show(args.ToString(), "Error with devices");
            });
        }

        //private void _decoder_DataDecoded(DataDecoder sender, DataDecodedEventArgs args)
        //{
        //    ArduinoData data = args.Data.RightArduino;
            
        //    //_mainThreadActionQueue.Clear();
        //    SetActionOnMainThread(() => {
        //        B1Label.Text = data.Button1 ? "True" : "False";
        //        B2Label.Text = data.Button2 ? "True" : "False";
        //        B3Label.Text = data.Button3 ? "True" : "False";
        //        B4Label.Text = data.Button4 ? "True" : "False";
        //        XLabel.Text = data.StickX.ToString();
        //        YLabel.Text = data.StickY.ToString();
        //    });
        //}

        private void MainThreadDispatcher_Tick(object sender, EventArgs e)
        {
            lock (_mainThreadActionQueue)
            {
                _singleAction?.Invoke();
                _singleAction = null;
                while (_mainThreadActionQueue.Count > 0)
                {
                    _mainThreadActionQueue.Dequeue().Invoke();
                }
            }
        }

        private void SetRightJoyStickPosition(float x, float y)
        {
            int pWidth = StickPanel.Width, pHeight = StickPanel.Height, jWidth = StickCross.Width, jHeight = StickCross.Height;
            StickCross.Location = new Point((int)(pWidth / 2 + pWidth * x / 2 - jWidth / 2), (int)(pHeight / 2 + pHeight * y / 2 - jHeight / 2));
        }

        private void SetLeftJoyStickPosition(float x, float y)
        {
            int pWidth = LeftStickPanel.Width, pHeight = LeftStickPanel.Height, jWidth = LeftStickCross.Width, jHeight = LeftStickCross.Height;
            LeftStickCross.Location = new Point((int)(pWidth / 2 + pWidth * x / 2 - jWidth / 2), (int)(pHeight / 2 + pHeight * y / 2 - jHeight / 2));
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            var data = new Data.DataPacket();

            if (_remote == null)
            {
                _remote = new VRidgeAPI.Remotes.VridgeRemote("localhost", "Arduino-interface",
                VRidgeAPI.Remotes.Capabilities.Controllers | VRidgeAPI.Remotes.Capabilities.HeadTracking);
                _leftController = new Controller(_remote, VRidgeAPI.Messages.BasicTypes.HandType.Left);
                _rightController = new Controller(_remote, VRidgeAPI.Messages.BasicTypes.HandType.Right);
                _head = new HeadTracking(_remote);
            }
            
            //_decoder.IsAutoRefreshEnabled = true;
            //if (_errorWindow && data.Error == DataPacketError.None)
            //{
            //    IntPtr mbWnd = FindWindow(null, "Error with devices");
            //    if (mbWnd != IntPtr.Zero)
            //    {
            //        SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //    }
            //}
            //if (data.Error != DataPacketError.None)
            //{
            //    if (!_errorWindow)
            //    {
            //        _errorWindow = true;
            //        _errorMsg = new ErrorFoundEventArgs(data.Error).ToString();
            //        MessageBox.Show(_errorMsg, "Error with devices");
            //        _errorWindow = false;
            //    }
            //}
            if (data.ContainsData)
            {
                StickLabel.Text = data.RightArduino.Stick ? "True" : "False";
                B1Label.Text = data.RightArduino.Button1 ? "True" : "False";
                B2Label.Text = data.RightArduino.Button2 ? "True" : "False";
                B3Label.Text = data.RightArduino.Button3 ? "True" : "False";
                B4Label.Text = data.RightArduino.Button4 ? "True" : "False";
                XLabel.Text = data.RightArduino.StickX.ToString();
                YLabel.Text = data.RightArduino.StickY.ToString();
                QuatXLabel.Text = data.RightMPU.YawPitchRoll.X.ToString();
                QuatYLabel.Text = data.RightMPU.YawPitchRoll.Y.ToString();
                QuatZLabel.Text = data.RightMPU.YawPitchRoll.Z.ToString();
                LStickLabel.Text = data.LeftArduino.Stick ? "True" : "False";
                LB1Label.Text = data.LeftArduino.Button1 ? "True" : "False";
                LB2Label.Text = data.LeftArduino.Button2 ? "True" : "False";
                LB3Label.Text = data.LeftArduino.Button3 ? "True" : "False";
                LB4Label.Text = data.LeftArduino.Button4 ? "True" : "False";
                LXLabel.Text = data.LeftArduino.StickX.ToString();
                LYLabel.Text = data.LeftArduino.StickY.ToString();
                LQuatXLabel.Text = data.LeftMPU.YawPitchRoll.X.ToString();
                LQuatYLabel.Text = data.LeftMPU.YawPitchRoll.Y.ToString();
                LQuatZLabel.Text = data.LeftMPU.YawPitchRoll.Z.ToString();

                //SetRightJoyStickPosition(_decoder.RightStick.X, _decoder.RightStick.Y);
                //SetLeftJoyStickPosition(_decoder.LeftStick.X, _decoder.LeftStick.Y);
                
            }
            RPosXLabel.Text = _kinect?.RightHand.X.ToString();
            RPosYLabel.Text = _kinect?.RightHand.Y.ToString();
            RPosZLabel.Text = _kinect?.RightHand.Z.ToString();
            LPosXLabel.Text = _kinect?.LeftHand.X.ToString();
            LPosYLabel.Text = _kinect?.LeftHand.Y.ToString();
            LPosZLabel.Text = _kinect?.LeftHand.Z.ToString();
            KinectElevation.Maximum = _kinect?.MaxAngle ?? -100;
            KinectElevation.Minimum = _kinect?.MinAngle ?? 100;
            KinectElevation.Value = _kinect?.Angle ?? 0;
            //_connectService.MoveNext();
            //if (DeviceConnetionService.Instance.RightControllerPort.IsConnected)
            //{
            //    ControllerTypeLabel.Text = "Right";
            //    LastMillisLabel.Text = $"{DeviceConnetionService.Instance.RightControllerPort.LastMilis}ms";
            //}
            //else ControllerTypeLabel.Text = "Disconnected";
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            //_decoder.RightStick.CalibrateZero();
            //_decoder.RightStick.CalibrateRanges();
            //_decoder.LeftStick.CalibrateZero();
            //_decoder.LeftStick.CalibrateRanges();
        }

        private void InitMPUButton_Click(object sender, EventArgs e)
        {
            //ControllerPort.Write(new byte[] { 1 }, 0, 1);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _kinect?.Dispose();
            _remote?.Dispose();
        }

        private void CalibrateOffsets_Click(object sender, EventArgs e)
        {
            //ControllerPort.Write(new byte[] { 2 }, 0, 1);
        }

        private void SetForwardBtn_Click(object sender, EventArgs e)
        {
            _kinect?.QueueKinectSetForward();
            _head?.Recenter();
        }

        private void KinectElevation_ValueChanged(object sender, EventArgs e)
        {
            _kinect.Angle = (int)KinectElevation.Value;
        }

        private void OrientationTrackBar_Scroll(object sender, EventArgs e)
        {
            int v = (((TrackBar)sender).Value - 180);
            OrientationLabel.Text = v.ToString();
            _kinect.Rotation = v;
        }
    }
}
