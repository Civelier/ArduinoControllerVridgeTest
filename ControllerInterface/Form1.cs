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
            DeviceConnetionService.Instance.Begin(ProtocolInfo.Devices[0], ProtocolInfo.Devices[1]);
            
            _connectService = DeviceConnetionService.Instance.Connect();
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
            var handshake = new Handshake();
            handshake.Replied += Handshake_Replied;
            //RightProtocol.Send(handshake);
        }

        private void Handshake_Replied(object sender, HandshakeRepliedEventArgs e)
        {
            var h = (Handshake)sender;
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
            _connectService.MoveNext();
            if (DeviceConnetionService.Instance.RightControllerPort.IsConnected)
            {
                ControllerTypeLabel.Text = "Right";
                LastMillisLabel.Text = $"{DeviceConnetionService.Instance.RightControllerPort.LastMilis}ms";
            }
            else ControllerTypeLabel.Text = "Disconnected";
        }
    }
}
