using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface
{
    [Flags]
    public enum Buttons : byte
    {
        Button1 = 0b10000000,
        Button2 = 0b01000000,
        Button3 = 0b00100000,
        Button4 = 0b00010000,
        Stick =   0b00001000,
    }
    public struct Data
    {

        private byte[] _buffer;

        public Buttons Buttons => (Buttons)_buffer[0];
        public bool Button1 => ButtonEqual(Buttons.Button1);
        public bool Button2 => ButtonEqual(Buttons.Button2);
        public bool Button3 => ButtonEqual(Buttons.Button3);
        public bool Button4 => ButtonEqual(Buttons.Button4);
        public bool Stick => ButtonEqual(Buttons.Stick);

        public Int16 StickX => BitConverter.ToInt16(_buffer, 1);
        public Int16 StickY => BitConverter.ToInt16(_buffer, 3);

        public Data(byte[] buffer)
        {
            _buffer = buffer;
        }

        public bool ButtonEqual(Buttons btn)
        {
            return (Buttons & btn) == btn;
        }
    }
}
