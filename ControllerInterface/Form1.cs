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

namespace ControllerInterface
{
    public partial class Form1 : Form
    {
        PCToArduinoCommunicationProtocol RightProtocol;
        public Form1()
        {
            InitializeComponent();
            RightProtocol = new PCToArduinoCommunicationProtocol(RightController);
            
        }

        private void PingButton_Click(object sender, EventArgs e)
        {

        }

        private void HandshakeButton_Click(object sender, EventArgs e)
        {
            var handshake = new Handshake();
            handshake.Replied += Handshake_Replied;
            RightProtocol.Send(handshake);
        }

        private void Handshake_Replied(object sender, HandshakeRepliedEventArgs e)
        {
            var h = (Handshake)sender;
            h.Replied -= Handshake_Replied;
            switch (e.Type)
            {
                case DeviceType.LeftController:
                    ControllerTypeLabel.Text = "Left";
                    break;
                case DeviceType.RightController:
                    ControllerTypeLabel.Text = "Right";
                    break;
                default:
                    break;
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            RightController.Open();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PortList.Items.Clear();
            PortList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RightProtocol.Port.PortName = System.IO.Ports.SerialPort.GetPortNames()[PortList.SelectedIndex];
        }
    }
}
