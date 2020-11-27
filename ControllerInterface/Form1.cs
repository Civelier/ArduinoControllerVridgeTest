using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCToArduinoCommunication.Protocol.SendCommands;
using PCToArduinoCommunication.Protocol;
using PCToArduinoCommunication.Devices;
using ControllerInterface.Data;

namespace ControllerInterface
{
    public partial class Form1 : Form
    {
        private static Queue<Action> _mainThreadActionQueue = new Queue<Action>();
        private static Action _singleAction;
        private DataDecoder _decoder;

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

        private IEnumerator<bool> _connectService;

        public Form1()
        {
            InitializeComponent();
            _decoder = new DataDecoder(ControllerPort);
            //_decoder.DataDecoded += _decoder_DataDecoded;
            ControllerPort.Open();
            _decoder.IsAutoRefreshEnabled = true;
            //DeviceConnetionService.Instance.Begin(ProtocolInfo.Devices[0], ProtocolInfo.Devices[1]);
            
            //_connectService = DeviceConnetionService.Instance.Connect();
        }

        private void _decoder_DataDecoded(DataDecoder sender, DataDecodedEventArgs args)
        {
            ArduinoData data = args.Data.RightArduino;
            
            //_mainThreadActionQueue.Clear();
            SetActionOnMainThread(() => {
                B1Label.Text = data.Button1 ? "True" : "False";
                B2Label.Text = data.Button2 ? "True" : "False";
                B3Label.Text = data.Button3 ? "True" : "False";
                B4Label.Text = data.Button4 ? "True" : "False";
                XLabel.Text = data.StickX.ToString();
                YLabel.Text = data.StickY.ToString();
            });
        }

        private void PingButton_Click(object sender, EventArgs e)
        {
            var ping = new Ping();
            ping.Replied += Ping_Replied;
            //RightProtocol.Send(ping);
        }

        private void Ping_Replied(object sender, PingRepliedEventArgs e)
        {
            var ping = (Ping)sender;
            ping.Replied -= Ping_Replied;
            QueueActionOnMainThread(() => LastMillisLabel.Text = $"{e.Milliseconds}ms");
        }

        private void HandshakeButton_Click(object sender, EventArgs e)
        {
            var handshake = new HandshakeCommand();
            handshake.Replied += Handshake_Replied;
            //RightProtocol.Send(handshake);
        }

        private void Handshake_Replied(object sender, HandshakeRepliedEventArgs e)
        {
            var h = (HandshakeCommand)sender;
            h.Replied -= Handshake_Replied;
            switch (e.Type)
            {
                case DeviceType.LeftController:
                    QueueActionOnMainThread(() => ControllerTypeLabel.Text = "Left");
                    break;
                case DeviceType.RightController:
                    QueueActionOnMainThread(() => ControllerTypeLabel.Text = "Right");
                    break;
                default:
                    break;
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (!ControllerPort.IsOpen) ControllerPort.Open();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PortList.Items.Clear();
            PortList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ControllerPort.IsOpen) ControllerPort.Close();
            ControllerPort.PortName = System.IO.Ports.SerialPort.GetPortNames()[PortList.SelectedIndex];
            if (!ControllerPort.IsOpen) ControllerPort.Open();
        }

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

        private void SetJoyStickPosition(float x, float y)
        {
            int pWidth = StickPanel.Width, pHeight = StickPanel.Height, jWidth = StickCross.Width, jHeight = StickCross.Height;
            StickCross.Location = new Point((int)(pWidth / 2 + pWidth * x / 2 - jWidth / 2), (int)(pHeight / 2 + pHeight * y / 2 - jHeight / 2));
        }

        private void ConnectTimer_Tick(object sender, EventArgs e)
        {
            var data = _decoder.LastDecodedData;

            //_decoder.IsAutoRefreshEnabled = true;
            if (data.ContainsData)
            {
                StickLabel.Text = data.RightArduino.Stick ? "True" : "False";
                B1Label.Text = data.RightArduino.Button1 ? "True" : "False";
                B2Label.Text = data.RightArduino.Button2 ? "True" : "False";
                B3Label.Text = data.RightArduino.Button3 ? "True" : "False";
                B4Label.Text = data.RightArduino.Button4 ? "True" : "False";
                XLabel.Text = data.RightArduino.StickX.ToString();
                YLabel.Text = data.RightArduino.StickY.ToString();
                QuatXLabel.Text = data.RightMPU.Quaternion.X.ToString();
                QuatYLabel.Text = data.RightMPU.Quaternion.Y.ToString();
                QuatZLabel.Text = data.RightMPU.Quaternion.Z.ToString();
                QuatWLabel.Text = data.RightMPU.Quaternion.W.ToString();
                SetJoyStickPosition(_decoder.RightStick.X, _decoder.RightStick.Y);
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
            _decoder.RightStick.CalibrateZero();
            _decoder.RightStick.CalibrateRanges();
        }

        private void InitMPUButton_Click(object sender, EventArgs e)
        {
            ControllerPort.Write(new byte[] { 1 }, 0, 1);
        }
    }
}
