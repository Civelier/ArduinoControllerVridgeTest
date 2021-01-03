using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface
{
    public enum JoyStickCalibrationStep
    {
        RequestCenter,
        Centered,
        Ranges,
    }
    public class JoyStick
    {
        private Int16 _xAxis;
        private Int16 _xMax;
        private Int16 _xHalf;
        public float X => Lerp(0, _xHalf, _xMax, _xAxis) * (ReverseX ? -1 : 1);

        private Int16 _yAxis;
        private Int16 _yMax;
        private Int16 _yHalf;
        public float Y => Lerp(0, _yHalf, _yMax, _yAxis) * (ReverseY ? -1 : 1);

        public bool ReverseX
        {
            get;
            set;
        }

        public bool ReverseY
        {
            get;
            set;
        }

        public JoyStick(short xMax, short yMax, bool reverseX, bool reverseY)
        {
            _xMax = xMax;
            _yMax = yMax;
            ReverseX = reverseX;
            ReverseY = reverseY;
        }

        internal void SetValues(Int16 x, Int16 y)
        {
            if (_yHalf == 0 && _xHalf == 0) CalibrateZero();
            _xMax = Math.Max(_xMax, _xAxis);
            _yMax = Math.Max(_yMax, _yAxis);
            _xAxis = x;
            _yAxis = y;
        }

        private float Lerp(int v0, int v1, int t)
        {
            return ((float)t - v0) / (v0 - v1);
        }

        private float Lerp(int min, int middle, int max, int value)
        {
            if (value < middle) 
                return -Lerp(middle, min, value);
            if (value > middle) 
                return Lerp(middle, max, value);
            return 0;
        }

        public void CalibrateRanges()
        {
            _xMax = _yMax = 0;
        }
        public void CalibrateZero()
        {
            _xHalf = _xAxis;
            _yHalf = _yAxis;
        }
    }

}
