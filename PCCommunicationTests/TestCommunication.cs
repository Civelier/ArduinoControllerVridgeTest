using System;
using System.IO.Ports;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCToArduinoCommunication.Protocol;
using PCToArduinoCommunication.Protocol.SendCommands;

namespace PCCommunicationTests
{
    [TestClass]
    public class TestCommunication
    {
        SerialPort port;
        PCToArduinoCommunicationProtocol comm;
        bool hsReplied = false;
        [TestMethod]
        public void TestOpenPort()
        {
            port.PortName = SerialPort.GetPortNames().FirstOrDefault();
            comm = new PCToArduinoCommunicationProtocol(port);
            comm.Port.Open();
        }

        [TestMethod]
        public void TestHandshake()
        {
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
        }

        private void Hs_Replied(object sender, HandshakeRepliedEventArgs e)
        {
            Assert.AreEqual(e.Type, DeviceType.TestDevice);
            hsReplied = true;
        }
    }
}
