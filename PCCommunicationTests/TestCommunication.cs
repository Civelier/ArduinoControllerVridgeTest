using System;
using System.IO.Ports;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCToArduinoCommunication.Protocol;
using PCToArduinoCommunication.Protocol.SendCommands;
using FluentAssertions;
using ControllerInterface.Data;

namespace PCCommunicationTests
{
    [TestClass]
    public class TestCommunication
    {
        SerialPort port = new SerialPort("COM1", 115200);
        PCToArduinoCommunicationProtocol comm;
        bool hsReplied = false;
        [TestMethod]
        public void ATestOpenPort()
        {
            port.PortName = SerialPort.GetPortNames().FirstOrDefault();
            comm = new PCToArduinoCommunicationProtocol(port);
            comm.Port.Open();
            comm.Port.IsOpen.Should().BeTrue();
            comm.Port.Close();
        }

        [TestMethod]
        public void TestHandshake()
        {
            port.PortName = SerialPort.GetPortNames().FirstOrDefault();
            comm = new PCToArduinoCommunicationProtocol(port);
            comm.Port.Open();
            HandshakeCommand hs = new HandshakeCommand();
            hs.Replied += Hs_Replied;
            comm.Send(hs);

            int timeout = 5000;
            int refresh = 50;
            for (int i = 0; i < timeout / refresh; i++)
            {
                if (hsReplied) return;
                System.Threading.Thread.Sleep(refresh);
            }
            Assert.Fail("Timed out");
            comm.Port.Close();
        }

        private void Hs_Replied(object sender, HandshakeRepliedEventArgs e)
        {
            e.Type.Should().Be(DeviceType.TestDevice);
            hsReplied = true;
        }

        [TestMethod]
        public void TestBufferGerRange()
        {
            var buff = new byte[]
            {
                0,
                23,
                7,
                15,
                57,
                204,
            };

            var range = buff.GetRange(2, 3);
            range.Should().BeEquivalentTo(new byte[]
            {
                7,
                15,
                57,
            });
        }
    }
}
