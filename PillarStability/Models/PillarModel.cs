using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PillarStability.Models
{
    public class PillarModel
    {
        private float _H;
        private float _W;
        private float _L;
        private float _APS;
        private float _UCS;
        private float _Weff;
        private float _Wth;
        private float _APStUCS;
        private float _APC;

        public PillarModel()
        {
            _H = 0;
            _W = 0;
            _L = 0;
            _APS = 0;
            _UCS = 0;
            _Weff = 4 * (_L * _W) / (2 * (_L + _W));
            _Wth = _Weff / _H;
            _APStUCS = _APS / _UCS;
            _APC = 0;
        }

        private float calcAPC()
        {
            if (_Wth > 3.99)
            {
                _APC = 0.23f + 0.017f * _Wth;
            }
            else
            {
                _APC = (float)(0.34f * Math.Pow(Math.Log10(_Wth + 0.75f), (1.4f / _Wth)));
            }
            return _APC;
        }

        [DisplayName("Height"), Description("")]
        public float Height
        {
            get { return _H; }
            set 
            {
                _H = value;
            }
        }

        [DisplayName("Width"), Description("")]
        public float Width
        {
            get { return _W; }
            set
            {
                _W = value;
            }
        }

        [DisplayName("Length"), Description("")]
        public float Length
        {
            get { return _L; }
            set
            {
                _L = value;
            }
        }

        [DisplayName("APS"), Description("")]
        public float APS
        {
            get { return _APS; }
            set
            {
                _APS = value;
            }
        }

        [DisplayName("UCS"), Description(""), Category("Input")]
        public float UCS
        {
            get { return _UCS; }
            set
            {
                _UCS = value;
            }
        }
    }
}
