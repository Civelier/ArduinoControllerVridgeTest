using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCToArduinoCommunication.Protocol;

namespace ControllerInterface
{
    public class DataDecodedEventArgs
    {
        public DataDecodedEventArgs(string buffer)
        {
            Buffer = buffer;
        }

        public string Buffer
        {
            get;
        }
    }

    public delegate void DataDecodeEventHandler(DataDecoder sender, DataDecodedEventArgs args);

    public class DataDecoder
    {
        SerialPort _port;

        public event DataDecodeEventHandler DataDecoded;

        public DataDecoder(SerialPort port)
        {
            _port = port;
            _port.DataReceived += _port_DataReceived;
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var s = _port.ReadLine();
            DataDecoded.Invoke(this, new DataDecodedEventArgs(s));
            _port.DiscardInBuffer();
        }
    }
}
