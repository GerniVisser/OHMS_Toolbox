using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Helper
{
    public class Bin
    {
        private int _frecuancy;
        private float _max;
        private float _min;
        private float _mid;

        public Bin(float min, float max)
        {
            _min = min;
            _max = max;
            _mid = ((_max - _min) / 2f) + min;
        }

        public int Frequency
        {
            get { return _frecuancy; }
            set { _frecuancy = value; }
        }

        public float Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public float Mid
        {
            get { return _mid; }
            set { _mid = value; }
        }

        public float Max
        {
            get { return _max; }
            set { _max = value; }
        }
    }
}
