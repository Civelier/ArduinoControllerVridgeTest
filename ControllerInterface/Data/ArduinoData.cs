using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.Data
{
    [Flags]
    public enum Buttons : byte
    {
        Stick   = 0b00000001,
        Menu    = 0b00000010,
        System  = 0b00000100,
        Button1 = 0b00001000,
        Button2 = 0b00010000,
        Button3 = 0b00100000,
        Button4 = 0b01000000,
    }
    public struct ArduinoData
    {
        public static int Size = 5;
        private byte[] _buffer;

        public Buttons Buttons => _buffer != null ? (Buttons)_buffer[0] : 0;
        public bool Button1 => ButtonEqual(Buttons.Button1);
        public bool Button2 => ButtonEqual(Buttons.Button2);
        public bool Button3 => ButtonEqual(Buttons.Button3);
        public bool Button4 => ButtonEqual(Buttons.Button4);
        public bool Stick => !ButtonEqual(Buttons.Stick);
        public bool Menu => ButtonEqual(Buttons.Menu);
        public bool System => ButtonEqual(Buttons.System);

        public short StickX => _buffer != null ? BitConverter.ToInt16(_buffer, 3) : (short)0;
        public short StickY => _buffer != null ? BitConverter.ToInt16(_buffer, 1) : (short)0;

        public ArduinoData(byte[] buffer)
        {
            _buffer = buffer;
        }

        public bool ButtonEqual(Buttons btn)
        {
            return (Buttons & btn) == btn;
        }
    }
}
