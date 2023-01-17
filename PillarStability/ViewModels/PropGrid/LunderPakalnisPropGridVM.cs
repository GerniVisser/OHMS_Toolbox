using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using Syncfusion.Windows.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.PropGrid
{
    public class LunderPakalnisPropGridVM: ViewModelBase
    {
        private LunderPakalnisModel _lunderPakalnisModel;
        public LunderPakalnisPropGridVM(LunderPakalnisModel lunderPakalnisModel)
        {
            _lunderPakalnisModel = lunderPakalnisModel;
        }

        [DisplayName("Safety Factor"), Description("Limit Safety Factor"), Category("Monte Carlo"), Display(Order = 0)]
        public float Lsf
        {
            get { return _lunderPakalnisModel.Lsf; }
            set
            {
                _lunderPakalnisModel.Lsf = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Confinement coeff"), Description("Coefficient of pillar confinement (coeff)"), Category("Monte Carlo"), Display(Order = 1)]
        public float Coeff
        {
            get { return _lunderPakalnisModel.Coeff; }
            set
            {
                _lunderPakalnisModel.Coeff = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Strength factor"), Description("Pillar strength size factor (K)"), Category("Monte Carlo"), Display(Order = 2)]
        public float Psk
        {
            get { return _lunderPakalnisModel.Psk; }
            set
            {
                _lunderPakalnisModel.Psk = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("C1"), Description("C1 Emperical Rockmass constant"), Category("Monte Carlo"), Display(Order = 3)]
        public float C1
        {
            get { return _lunderPakalnisModel.C1; }
            set
            {
                _lunderPakalnisModel.C1 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("C2"), Description("C2 Emperical Rockmass constant"), Category("Monte Carlo"), Display(Order = 4)]
        public float C2
        {
            get { return _lunderPakalnisModel.C2; }
            set
            {
                _lunderPakalnisModel.C2 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Monte Carlo")]
        public float stdLength
        {
            get { return _lunderPakalnisModel.StdLength; }
            set
            {
                _lunderPakalnisModel.StdLength = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Std Dev Width"), Description("Std Dev of Pillar Width (m)"), Category("Monte Carlo")]
        public float stdWidth
        {
            get { return _lunderPakalnisModel.StdWidth; }
            set
            {
                _lunderPakalnisModel.StdWidth = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Monte Carlo")]
        public float stdHeight
        {
            get { return _lunderPakalnisModel.StdHeight; }
            set
            {
                _lunderPakalnisModel.StdHeight = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Std Dev of coeff"), Description("Std Dev of coeff"), Category("Monte Carlo")]
        public float stdCoeff
        {
            get { return _lunderPakalnisModel.StdCoeff; }
            set
            {
                _lunderPakalnisModel.StdCoeff = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of Kappa"), Description("Std Dev of Kappa"), Category("Monte Carlo")]
        public float stdKappa
        {
            get { return _lunderPakalnisModel.StdKappa; }
            set
            {
                _lunderPakalnisModel.StdKappa = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of C1"), Description("Std Dev of C1"), Category("Monte Carlo")]
        public float stdC1
        {
            get { return _lunderPakalnisModel.StdC1; }
            set
            {
                _lunderPakalnisModel.StdC1 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of C2"), Description("Std Dev of C2"), Category("Monte Carlo")]
        public float stdC2
        {
            get { return _lunderPakalnisModel.StdC2; }
            set
            {
                _lunderPakalnisModel.StdC2 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of K"), Description("Std Dev of K"), Category("Monte Carlo")]
        public float stdPsk
        {
            get { return _lunderPakalnisModel.StdPsk; }
            set
            {
                _lunderPakalnisModel.StdPsk = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of UCS"), Description("Std Dev of UCS"), Category("Monte Carlo")]
        public float stdUcs
        {
            get { return _lunderPakalnisModel.StdUcs; }
            set
            {
                _lunderPakalnisModel.StdUcs = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of APS"), Description("Std Dev of APS"), Category("Monte Carlo")]
        public float stdAps
        {
            get { return _lunderPakalnisModel.StdAps; }
            set
            {
                _lunderPakalnisModel.StdAps = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        

        public override string ToString()
        {
            return "Lunder and Pakalnis";
        }
    }
}
