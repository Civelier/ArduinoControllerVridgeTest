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
using ControllerInterface.ConnectionServices;
using ControllerInterface.DataTypes;
using ControllerInterface.InterProcessCommunication;
using System.Threading;

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
        private ControllersConnectionService _controllersConnection;
        private StatusData _status = new StatusData();
        private InterProcessService _interProcessService;
        private bool _newPacket;
        private InterProcessPacket _lastInterProcessPacket;
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

            _controllersConnection = new ControllersConnectionService();
            _controllersConnection.DataDecoded += _controllersConnection_DataDecoded1;
            _controllersConnection.StartService();
            _kinect = new KinectDevice();
            _kinect.StartKinectProcess();
            _kinect.NewSkeletonFrameReady += _kinect_NewSkeletonFrameReady;
            StatusPropertyGrid.SelectedObject = _status;
            HeightUpDown.Value = (decimal)PropertiesData.Instance.Height;
            OrientationTrackBar.Value = PropertiesData.Instance.KinectAngleOffset;
            int v = OrientationTrackBar.Value - 180;
            OrientationLabel.Text = v.ToString();
            _kinect.Rotation = v;
            _interProcessService = new InterProcessService();
            _interProcessService.Start();
            _interProcessService.PacketRecieved += _interProcessService_PacketRecieved;
            _interProcessService.Request(new SendValuesRequest((float)HeightUpDown.Value, _kinect.Rotation));
        }

        private void _interProcessService_PacketRecieved(InterProcessService sender, PacketRecievedEventArgs args)
        {
            _newPacket = true;
            _lastInterProcessPacket = args.Packet;
        }

        private void _kinect_NewSkeletonFrameReady(KinectDevice sender, KinectNewSkeletonFrameReadyEventArgs args)
        {
            _rightController?.SetData(args?.RightHand ?? new Vector3());
            _leftController?.SetData(args?.LeftHand ?? new Vector3());
            _head?.SetData(args?.Head ?? new Vector3());
        }

        private void _controllersConnection_DataDecoded1(ControllersConnectionService sender, DataDecodedEventArgs args)
        {
            _rightController?.SetData(args.Data.RightArduino, args.Data.RightMPU);
            _leftController?.SetData(args.Data.LeftArduino, args.Data.LeftMPU);
            if (_rightController.ControlsData.System) _controllersConnection.CalibrateOffsets();
        }

        //private void _decoder_ErrorFound(DataDecoder sender, ErrorFoundEventArgs args)
        //{
        //    QueueActionOnMainThread(() => 
        //    {
        //        MessageBox.Show(args.ToString(), "Error with devices");
        //    });
        //}

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
            if (_newPacket)
            {
                if (_lastInterProcessPacket.SetForward)
                {
                    _controllersConnection.CalibrateOffsets();
                    _kinect?.QueueKinectSetForward();
                    _head?.Recenter();
                }
                else
                {
                    HeightUpDown.Value = (decimal)_lastInterProcessPacket.Height;
                    OrientationTrackBar.Value = (int)_lastInterProcessPacket.RotationOffset;
                    int v = OrientationTrackBar.Value - 180;
                    OrientationLabel.Text = v.ToString();
                    _kinect.Rotation = v;
                    PropertiesData.Instance.KinectAngleOffset = OrientationTrackBar.Value;
                }
                _newPacket = false;
            }
            var data = _controllersConnection.LastDecodedData;
            _status.ControllersStatus = _controllersConnection.Status;
            StatusPropertyGrid.Refresh();
            _controllersConnection.UpdateStatus();

            if (_remote == null || (_leftController?.IsDisposed ?? true) || (_rightController?.IsDisposed ?? true) || (_head?.IsDisposed ?? true))
            {
                _remote?.Dispose();
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

                SetRightJoyStickPosition(_controllersConnection.RightStick.X, _controllersConnection.RightStick.Y);
                SetLeftJoyStickPosition(_controllersConnection.LeftStick.X, _controllersConnection.LeftStick.Y);

            }
            RPosXLabel.Text = _kinect?.RightHand.X.ToString();
            RPosYLabel.Text = _kinect?.RightHand.Y.ToString();
            RPosZLabel.Text = _kinect?.RightHand.Z.ToString();
            LPosXLabel.Text = _kinect?.LeftHand.X.ToString();
            LPosYLabel.Text = _kinect?.LeftHand.Y.ToString();
            LPosZLabel.Text = _kinect?.LeftHand.Z.ToString();
            KinectElevation.Maximum = _kinect?.MaxAngle ?? -100;
            KinectElevation.Minimum = _kinect?.MinAngle ?? 100;
            if (_kinect != null)
            {
                KinectElevation.Value = _kinect?.Angle ?? 0;
            }
            
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
            _controllersConnection.RightStick.CalibrateZero();
            _controllersConnection.RightStick.CalibrateRanges();
            _controllersConnection.LeftStick.CalibrateZero();
            _controllersConnection.LeftStick.CalibrateRanges();
        }

        private void InitMPUButton_Click(object sender, EventArgs e)
        {
            _controllersConnection.CalibrateMPU();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            PropertiesData.Save();
            _controllersConnection?.Dispose();
            _kinect?.Dispose();
            _remote?.Dispose();
            _interProcessService?.Dispose();
        }

        private void CalibrateOffsets_Click(object sender, EventArgs e)
        {
            _controllersConnection.CalibrateOffsets();
        }

        private void SetForwardBtn_Click(object sender, EventArgs e)
        {
            _kinect?.QueueKinectSetForward();
            _head?.Recenter();
        }

        private void KinectElevation_ValueChanged(object sender, EventArgs e)
        {
            PropertiesData.Instance.KinectElevationAngle = _kinect.Angle = (int)KinectElevation.Value;
        }

        private void OrientationTrackBar_Scroll(object sender, EventArgs e)
        {
            int v = OrientationTrackBar.Value - 180;
            OrientationLabel.Text = v.ToString();
            _kinect.Rotation = v;
            PropertiesData.Instance.KinectAngleOffset = OrientationTrackBar.Value;
            _interProcessService?.Request(new SendValuesRequest((float)HeightUpDown.Value, _kinect.Rotation));
        }

        private void HeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            PropertiesData.Instance.Height = (float)HeightUpDown.Value;
            _interProcessService?.Request(new SendValuesRequest((float)HeightUpDown.Value, _kinect.Rotation));
        }

        private void OrientationTrackBar_Leave(object sender, EventArgs e)
        {
            PropertiesData.Save();
        }

        private void HeightUpDown_Leave(object sender, EventArgs e)
        {
            PropertiesData.Save();
        }
    }
}
