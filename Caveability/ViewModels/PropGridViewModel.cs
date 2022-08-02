using _SharedWpfLibrary.ViewModels;
using Caveability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.ViewModels
{
    public class PropGridViewModel : ViewModelBase
    {
        private WallModel _wallModel;

        public PropGridViewModel(WallModel wallModel)
        {
            _wallModel = wallModel;
        }

        [Category("Calculate Q"), DisplayName("RQD"), Description("Rock Quality Designation")]
        public float rqd
        {
            get { return _wallModel.Q_Model.rqd; }
            set
            {
                _wallModel.Q_Model.rqd = value;
                OnPropertyChanged(nameof(rqd));
            }
        }

        [Category("Calculate Q"), DisplayName("Jn"), Description("Joint set number")]
        public float jn
        {
            get { return _wallModel.Q_Model.jn; }
            set
            {
                _wallModel.Q_Model.jn = value;
                OnPropertyChanged(nameof(jr));
            }
        }

        [Category("Calculate Q"), DisplayName("Jr"), Description("Joint roughness number")]
        public float jr
        {
            get { return _wallModel.Q_Model.jr; }
            set
            {
                _wallModel.Q_Model.jr = value;
                OnPropertyChanged(nameof(jr));
            }
        }

        [Category("Calculate Q"), DisplayName("Ja"), Description("Joint alteration number")]
        public float ja
        {
            get { return _wallModel.Q_Model.ja; }
            set
            {
                _wallModel.Q_Model.ja = value;
                OnPropertyChanged(nameof(ja));
            }
        }

        [Category("Calculate A"), DisplayName("σc"), Description("This is the intact rock strength (UCS) of the orebody")]
        public float oc
        {
            get { return _wallModel.A_Model.oc; }
            set 
            { 
                _wallModel.A_Model.oc = value;
                OnPropertyChanged(nameof(oc));
            }
        }

        [Category("Calculate A"), DisplayName("σmax"), Description("")]
        public float omax
        {
            get { return _wallModel.A_Model.omax; }
            set
            {
                _wallModel.A_Model.omax = value;
                OnPropertyChanged(nameof(omax));
            }
        }

        [Category("Calculate B"), DisplayName("b"), Description("Smallest intersection angle between plane under investigation and identified sets")]
        public float b
        {
            get { return _wallModel.B_Model.b; }
            set
            {
                _wallModel.B_Model.b = value;
                OnPropertyChanged(nameof(b));
            }
        }

        [Category("Calculate C"), DisplayName("c"), Description("Dip of slabbing")]
        public float c
        {
            get { return _wallModel.C_Model.c; }
            set
            {
                _wallModel.C_Model.c = value;
                OnPropertyChanged(nameof(c));
            }
        }

        [Category("HR"), DisplayName("Length"), Description("")]
        public float lenght
        {
            get { return _wallModel.HR_Model.Height; }
            set
            {
                _wallModel.HR_Model.Height = value;
                OnPropertyChanged(nameof(lenght));
            }
        }

        [Category("HR"), DisplayName("Width"), Description("")]
        public float width
        {
            get { return _wallModel.HR_Model.Width; }
            set
            {
                _wallModel.HR_Model.Width = value;
                OnPropertyChanged(nameof(width));
            }
        }
    }
}
