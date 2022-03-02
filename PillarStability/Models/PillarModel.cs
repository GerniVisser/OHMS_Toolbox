using PillarStability.Helper;
using System;
using System.ComponentModel;

namespace PillarStability.Models
{
    public class PillarModel
    {
        private float _H = 2.5f;
        private float _W = 7;
        private float _L = 7;
        private float _APS = 80.34f;
        private float _UCS = 111.42f;
        private float _APC;
        private string _name;

        private float _stdLength = 0.5f;
        private float _stdWidth = 0.5f;
        private float _stdHeight = 0.5f;
        private float _coeff = 0.4f;
        private float _stdCoeff = 0.06f;
        private float _stdKappa = 0;
        private float _c1 = 0.68f;
        private float _stdC1 = 0.068f;
        private float _c2 = 0.52f;
        private float _stdC2 = 0.052f;
        private float _psk = 0.45f;
        private float _stdPsk = 0.05f;
        private float _stdUcs = 0;
        private float _stdAps = 0;
        private float _lsf = 1f;

        private string _color;
        private Bins _bins;
        private MCGridObject _mcGridObject;

        public PillarModel(string Name)
        {
            var random = new Random();
            _name = Name;
            _color = String.Format("#{0:X6}", random.Next(0x1000000));
        }

        public PillarModel(string Name, float Height, float Width, float Length, float APS, float UCS)
        {
            var random = new Random();
            _name = Name;
            _color = String.Format("#{0:X6}", random.Next(0x1000000));
            _H = Height;
            _W = Width;
            _L = Length;
            _APS = APS;
            _UCS = UCS;
        }

        [Browsable(false)]
        public string Color
        {
            get { return _color; }
            set { _color = value; }
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

        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [DisplayName("Height"), Description("Pillar Height (m)"), Category("General")]
        public float Height
        {
            get { return _H; }
            set
            {
                _H = value;
            }
        }

        [DisplayName("Width"), Description("Pillar Width (m)"), Category("General")]
        public float Width
        {
            get { return _W; }
            set
            {
                _W = value;
            }
        }

        [DisplayName("Length"), Description("Pillar Length (m)"), Category("General")]
        public float Length
        {
            get { return _L; }
            set
            {
                _L = value;
            }
        }

        [DisplayName("APS"), Description("Average Pillar Stress (MPa)"), Category("General")]
        public float APS
        {
            get { return _APS; }
            set
            {
                _APS = value;
            }
        }

        [DisplayName("UCS"), Description("Average Uniaxial Compressive strength(MPa)"), Category("General")]
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

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Monte Carlo")]
        public float stdLength
        {
            get { return _stdLength; }
            set { _stdLength = value; }
        }

        [DisplayName("Std Dev Width"), Description("Std Dev of Pillar Width (m)"), Category("Monte Carlo")]
        public float stdWidth
        {
            get { return _stdWidth; }
            set { _stdWidth = value; }
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Monte Carlo")]
        public float stdHeight
        {
            get { return _stdHeight; }
            set { _stdHeight = value; }
        }

        [DisplayName("Confinement coeff"), Description("Coefficient of pillar confinement (coeff)"), Category("Monte Carlo")]
        public float Coeff
        {
            get { return _coeff; }
            set { _coeff = value; }
        }

        [DisplayName("Std Dev of coeff"), Description("Std Dev of coeff"), Category("Monte Carlo")]
        public float stdCoeff
        {
            get { return _stdCoeff; }
            set { _stdCoeff = value; }
        }

        [DisplayName("Std Dev of Kappa"), Description("Std Dev of Kappa"), Category("Monte Carlo")]
        public float stdKappa
        {
            get { return _stdKappa; }
            set { _stdKappa = value; }
        }

        [DisplayName("C1"), Description("C1 Emperical Rockmass constant"), Category("Monte Carlo")]
        public float C1
        {
            get { return _c1; }
            set { _c1 = value; }
        }

        [DisplayName("Std Dev of C1"), Description("Std Dev of C1"), Category("Monte Carlo")]
        public float stdC1
        {
            get { return _stdC1; }
            set { _stdC1 = value; }
        }

        [DisplayName("C2"), Description("C2 Emperical Rockmass constant"), Category("Monte Carlo")]
        public float C2
        {
            get { return _c2; }
            set { _c2 = value; }
        }

        [DisplayName("Std Dev of C2"), Description("Std Dev of C2"), Category("Monte Carlo")]
        public float stdC2
        {
            get { return _stdC2; }
            set { _stdC2 = value; }
        }

        [DisplayName("Strength factor"), Description("Pillar strength size factor (K)"), Category("Monte Carlo")]
        public float Psk
        {
            get { return _psk; }
            set { _psk = value; }
        }

        [DisplayName("Std Dev of K"), Description("Std Dev of K"), Category("Monte Carlo")]
        public float stdPsk
        {
            get { return _stdPsk; }
            set { _stdPsk = value; }
        }

        [DisplayName("Std Dev of UCS"), Description("Std Dev of UCS"), Category("Monte Carlo")]
        public float stdUcs
        {
            get { return _stdUcs; }
            set { _stdUcs = value; }
        }

        [DisplayName("Std Dev of APS"), Description("Std Dev of APS"), Category("Monte Carlo")]
        public float stdAps
        {
            get { return _stdAps; }
            set { _stdAps = value; }
        }

        [DisplayName("Safety Factor"), Description("Limit Safety Factor"), Category("Monte Carlo")]
        public float Lsf
        {
            get { return _lsf; }
            set { _lsf = value; }
        }

        [Browsable(false)]
        public MCGridObject MCGridObject
        {
            get { return _mcGridObject; }
            set { _mcGridObject = value; }
        }

        [Browsable(false)]
        public Bins Bins
        {
            get { return _bins; }
            set { _bins = value; }
        }




    }
}
