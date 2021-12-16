using System.ComponentModel;

namespace Caveability.Models
{
    public class Wall
    {
        private float _RQD;
        private float _Jn;
        private float _Jr;
        private float _Ja;
        private float _oc;
        private float _omax;
        private float _b;
        private float _c;
        private float _length;
        private float _width;

        [Browsable(false)]
        public Q_Model Q { get; set; } = new Q_Model(78, 8, 1, 1);
        [Browsable(false)]
        public A_Model A { get; set; } = new A_Model(150, 54.33f);
        [Browsable(false)]
        public B_Model B { get; set; } = new B_Model(20);
        [Browsable(false)]
        public C_Model C { get; set; } = new C_Model(0);

        [Browsable(false)]
        public HR_Model HR { get; set; } = new HR_Model(35, 28);

        public Wall()
        {
            rqd = Q._rqd;
            jn = Q._jn;
            jr = Q._jr;
            ja = Q._ja;
            oc = A._oc;
            omax = A._omax;
            b = B._b;
            c = C._c;
            width = HR._width;
            lenght = HR._length;
        }

        public Wall(bool isFootWall)
        {
            C = new C_Footwall_Model(0);

            rqd = Q._rqd;
            jn = Q._jn;
            jr = Q._jr;
            ja = Q._ja;
            oc = A._oc;
            omax = A._omax;
            b = B._b;
            c = C._c;
            width = HR._width;
            lenght = HR._length;
        }

        [Category("Calculate Q"), DisplayName("RQD"), Description("")]
        public float rqd
        {
            get { return _RQD; }
            set
            {
                _RQD = value;
                Q._rqd = value;
            }
        }

        [Category("Calculate Q"), DisplayName("Jn"), Description("")]
        public float jn
        {
            get { return _Jn; }
            set
            {
                _Jn = value;
                Q._jn = value;
            }
        }

        [Category("Calculate Q"), DisplayName("Jr"), Description("")]
        public float jr
        {
            get { return _Jr; }
            set
            {
                _Jr = value;
                Q._jr = value;
            }
        }

        [Category("Calculate Q"), DisplayName("Ja"), Description("")]
        public float ja
        {
            get { return _Ja; }
            set
            {
                _Ja = value;
                Q._ja = value;
            }
        }

        [Category("Calculate A"), DisplayName("σc"), Description("")]
        public float oc
        {
            get { return _oc; }
            set
            {
                _oc = value;
                A._oc = value;
            }
        }

        [Category("Calculate A"), DisplayName("σmax"), Description("")]
        public float omax
        {
            get { return _omax; }
            set
            {
                _omax = value;
                A._omax = value;
            }
        }

        [Category("Calculate B"), DisplayName("b"), Description("")]
        public float b
        {
            get { return _b; }
            set
            {
                _b = value;
                B._b = value;
            }
        }

        [Category("Calculate C"), DisplayName("c"), Description("")]
        public float c
        {
            get { return _c; }
            set
            {
                _c = value;
                C._c = value;
            }
        }

        [Category("HR"), DisplayName("length"), Description("")]
        public float lenght
        {
            get { return _length; }
            set
            {
                _length = value;
                HR._length = value;
            }
        }

        [Category("HR"), DisplayName("width"), Description("")]
        public float width
        {
            get { return _width; }
            set
            {
                _width = value;
                HR._width = value;
            }
        }
    }
}
