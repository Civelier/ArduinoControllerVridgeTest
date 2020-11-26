using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface
{
    public class JoyStick
    {
        private Int16 _xAxis;
        private Int16 _xMax;
        private Int16 _xHalf => (Int16)(_xMax / 2);
        public float X => (_xAxis - _xHalf) / (float)_xMax;

        private Int16 _yAxis;
        private Int16 _yMax;
        private Int16 _yHalf => (Int16)(_yMax / 2);
        public float Y => (_yAxis - _yHalf) / (float)_yMax;

        public JoyStick(short xMax, short yMax)
        {
            _xMax = xMax;
            _yMax = yMax;
        }

        internal void SetValues(Int16 x, Int16 y)
        {
            _xMax = Math.Max(_xMax, x);
            _yMax = Math.Max(_yMax, y);
            _xAxis = x;
            _yAxis = y;
        }
    }

}
