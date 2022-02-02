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
        private float _APC;
        private string _name;


        public PillarModel(string Name)
        {
            _name = Name;
            _H = 0;
            _W = 0;
            _L = 0;
            _APS = 0;
            _UCS = 0;
        }

        private float calcAPC()
        {
            if (Wth > 3.99)
            {
                _APC = 0.23f + 0.017f * Wth;
            }
            else
            {
                _APC = (float)(0.34f * Math.Pow(Math.Log10(Wth + 0.75f), (1.4f / Wth)));
            }
            return _APC;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        [Browsable(false)]
        public float Weff
        {
            get { return 4 * (_L * _W) / (2 * (_L + _W)); }
        }

        [Browsable(false)]
        public float Wth
        {
            get { return Weff / _H; }
        }

        [Browsable(false)]
        public float APStUCS
        {
            get { return _APS / _UCS; }
        }

        [Browsable(false)]
        public float APC
        {
            get { return calcAPC(); }
        }
    }
}
