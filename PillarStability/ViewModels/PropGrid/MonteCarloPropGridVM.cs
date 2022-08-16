using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.PropGrid
{
    public class MonteCarloPropGridVM: ViewModelBase
    {
        private MonteCarloModel _monteCarloModel;

        public MonteCarloPropGridVM(PillarModel pillarModel)
        {
            _monteCarloModel = pillarModel.MonteCarloModel;
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Monte Carlo")]
        public float stdLength
        {
            get { return _monteCarloModel.StdLength; }
            set 
            { 
                _monteCarloModel.StdLength = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Std Dev Width"), Description("Std Dev of Pillar Width (m)"), Category("Monte Carlo")]
        public float stdWidth
        {
            get { return _monteCarloModel.StdWidth; }
            set 
            { 
                _monteCarloModel.StdWidth = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Monte Carlo")]
        public float stdHeight
        {
            get { return _monteCarloModel.StdHeight; }
            set 
            { 
                _monteCarloModel.StdHeight = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Confinement coeff"), Description("Coefficient of pillar confinement (coeff)"), Category("Monte Carlo")]
        public float Coeff
        {
            get { return _monteCarloModel.Coeff; }
            set 
            { 
                _monteCarloModel.Coeff = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of coeff"), Description("Std Dev of coeff"), Category("Monte Carlo")]
        public float stdCoeff
        {
            get { return _monteCarloModel.StdCoeff; }
            set 
            { 
                _monteCarloModel.StdCoeff = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of Kappa"), Description("Std Dev of Kappa"), Category("Monte Carlo")]
        public float stdKappa
        {
            get { return _monteCarloModel.StdKappa; }
            set 
            { 
                _monteCarloModel.StdKappa = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("C1"), Description("C1 Emperical Rockmass constant"), Category("Monte Carlo")]
        public float C1
        {
            get { return _monteCarloModel.C1; }
            set 
            { 
                _monteCarloModel.C1 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of C1"), Description("Std Dev of C1"), Category("Monte Carlo")]
        public float stdC1
        {
            get { return _monteCarloModel.StdC1; }
            set 
            { 
                _monteCarloModel.StdC1 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("C2"), Description("C2 Emperical Rockmass constant"), Category("Monte Carlo")]
        public float C2
        {
            get { return _monteCarloModel.C2; }
            set 
            { 
                _monteCarloModel.C2 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of C2"), Description("Std Dev of C2"), Category("Monte Carlo")]
        public float stdC2
        {
            get { return _monteCarloModel.StdC2; }
            set 
            { 
                _monteCarloModel.StdC2 = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Strength factor"), Description("Pillar strength size factor (K)"), Category("Monte Carlo")]
        public float Psk
        {
            get { return _monteCarloModel.Psk; }
            set 
            { 
                _monteCarloModel.Psk = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of K"), Description("Std Dev of K"), Category("Monte Carlo")]
        public float stdPsk
        {
            get { return _monteCarloModel.StdPsk; }
            set 
            { 
                _monteCarloModel.StdPsk = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of UCS"), Description("Std Dev of UCS"), Category("Monte Carlo")]
        public float stdUcs
        {
            get { return _monteCarloModel.StdUcs; }
            set 
            { 
                _monteCarloModel.StdUcs = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Std Dev of APS"), Description("Std Dev of APS"), Category("Monte Carlo")]
        public float stdAps
        {
            get { return _monteCarloModel.StdAps; }
            set 
            { 
                _monteCarloModel.StdAps = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }

        [DisplayName("Safety Factor"), Description("Limit Safety Factor"), Category("Monte Carlo")]
        public float Lsf
        {
            get { return _monteCarloModel.Lsf; }
            set 
            { 
                _monteCarloModel.Lsf = value;
                OnPropertyChanged(nameof(PillarPropGridVM));
            }
        }
    }
}
