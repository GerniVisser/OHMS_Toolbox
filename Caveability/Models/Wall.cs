using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class Wall
    {
        private double _RQD;
        private double _Jn;
        private double _Jr;
        private double _Ja;
        private double _oc;
        private double _omax;
        private double _b;
        private double _c;
        private double _length;
        private double _width;

        [Browsable(false)]
        public Q_Model Q { get; set; } = new Q_Model(66, 12, 1, 1);
        [Browsable(false)]
        public A_Model A { get; set; } = new A_Model(50, 150);
        [Browsable(false)]
        public B_Model B { get; set; } = new B_Model(0);
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
        public double rqd
        {
            get { return _RQD; }
            set
            {
                _RQD = value;
                Q._rqd = value;
            }
        }

        [Category("Calculate Q"), DisplayName("Jn"), Description("")]
        public double jn
        {
            get { return _Jn; }
            set
            {
                _Jn = value;
                Q._jn = value;
            }
        }

        [Category("Calculate Q"), DisplayName("Jr"), Description("")]
        public double jr
        {
            get { return _Jr; }
            set
            {
                _Jr = value;
                Q._jr = value;
            }
        }

        [Category("Calculate Q"), DisplayName("Ja"), Description("")]
        public double ja
        {
            get { return _Ja; }
            set
            {
                _Ja = value;
                Q._ja = value;
            }
        }

        [Category("Calculate A"), DisplayName("σc"), Description("")]
        public double oc
        {
            get { return _oc; }
            set
            {
                _oc = value;
                A._oc = value;
            }
        }

        [Category("Calculate A"), DisplayName("σmax"), Description("")]
        public double omax
        {
            get { return _omax; }
            set
            {
                _omax = value;
                A._omax = value;
            }
        }

        [Category("Calculate B"), DisplayName("b"), Description("")]
        public double b
        {
            get { return _b; }
            set
            {
                _b = value;
                B._b = value;
            }
        }

        [Category("Calculate C"), DisplayName("c"), Description("")]
        public double c
        {
            get { return _c; }
            set
            {
                _c = value;
                C._c = value;
            }
        }

        [Category("HR"), DisplayName("length"), Description("")]
        public double lenght
        {
            get { return _length; }
            set
            {
                _length = value;
                HR._length = value;
            }
        }

        [Category("HR"), DisplayName("width"), Description("")]
        public double width
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
