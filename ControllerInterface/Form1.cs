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

namespace ControllerInterface
{
    public partial class Form1 : Form
    {
        private static Queue<Action> _mainThreadActionQueue = new Queue<Action>();
        private DataDecoder _decoder;

        public static void QueueActionOnMainThread(Action action)
        {
            lock (_mainThreadActionQueue)
            {
                _mainThreadActionQueue.Enqueue(action);
            }
        }

        private IEnumerator<bool> _connectService;

        public Form1()
        {
            InitializeComponent();
            MainThreadDispatcher.Enabled = true;
            _decoder = new DataDecoder(RightController);
            _decoder.DataDecoded += _decoder_DataDecoded;
            RightController.Open();
            //DeviceConnetionService.Instance.Begin(ProtocolInfo.Devices[0], ProtocolInfo.Devices[1]);
            
            //_connectService = DeviceConnetionService.Instance.Connect();
        }

        private void _decoder_DataDecoded(DataDecoder sender, DataDecodedEventArgs args)
        {
            string[] s = args.Buffer.Split(';');
            int[] values = new int[s.Length];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = int.Parse(s[i]);
            }

            bool b1 = values[0] == 1;
            bool b2 = values[1] == 1;
            bool b3 = values[2] == 1;
            bool b4 = values[3] == 1;
            int x = values[4];
            int y = values[5];
            //_mainThreadActionQueue.Clear();
            QueueActionOnMainThread(() => {
                B1Label.Text = b1 ? "True" : "False";
                B2Label.Text = b2 ? "True" : "False";
                B3Label.Text = b3 ? "True" : "False";
                B4Label.Text = b4 ? "True" : "False";
                XLabel.Text = x.ToString();
                YLabel.Text = y.ToString();
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
            if (!RightController.IsOpen) RightController.Open();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PortList.Items.Clear();
            PortList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RightProtocol.Port.PortName = System.IO.Ports.SerialPort.GetPortNames()[PortList.SelectedIndex];
        }

        private void MainThreadDispatcher_Tick(object sender, EventArgs e)
        {
            lock (_mainThreadActionQueue)
            {
                while (_mainThreadActionQueue.Count > 0)
                {
                    _mainThreadActionQueue.Dequeue().Invoke();
                }
            }
        }

        private void ConnectTimer_Tick(object sender, EventArgs e)
        {
            //_connectService.MoveNext();
            //if (DeviceConnetionService.Instance.RightControllerPort.IsConnected)
            //{
            //    ControllerTypeLabel.Text = "Right";
            //    LastMillisLabel.Text = $"{DeviceConnetionService.Instance.RightControllerPort.LastMilis}ms";
            //}
            //else ControllerTypeLabel.Text = "Disconnected";
        }
    }
}
