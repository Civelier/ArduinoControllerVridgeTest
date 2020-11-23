using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface
{
    public struct Data
    {
        private byte[] _buffer;

        public bool Button1 => _buffer[0] == 1;
        public bool Button2 => _buffer[1] == 1;
        public bool Button3 => _buffer[2] == 1;
        public bool Button4 => _buffer[3] == 1;
        public bool Stick => _buffer[4] == 1;

        public short StickX => BitConverter.ToInt16(_buffer, 5);
        public short StickY => BitConverter.ToInt16(_buffer, 7);

        public Data(byte[] buffer)
        {
            _buffer = buffer;
        }
    }
}
