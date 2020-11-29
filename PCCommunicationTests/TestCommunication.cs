using System;
using System.IO.Ports;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using ControllerInterface.Data;

namespace PCCommunicationTests
{
    [TestClass]
    public class TestCommunication
    {
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
