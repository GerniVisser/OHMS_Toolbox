using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using Syncfusion.Windows.PropertyGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PillarStability.ViewModels.PropGrid
{
    public class LunderPakalnisPropGridVM: ViewModelBase
    {
        private PillarModel _pillarModel;
        private MonteCarloModel _monteCarloModel;
        private LunderPakalnisModel _lunderPakalnisModel;
        public LunderPakalnisPropGridVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _monteCarloModel = pillarModel.MonteCarloModel;
            _lunderPakalnisModel = (LunderPakalnisModel)_pillarModel.MonteCarloModel;
        }

        [DisplayName("Name"), Description("Pillar Nickname"), Category("General"), Display(Order = 0)]
        public string Name
        {
            get { return _pillarModel.Name; }
            set
            {
                _pillarModel.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [DisplayName("Color"), Description("Pillar Color"), Category("General"), Display(Order = 0)]
        public Brush Color
        {
            get { return _pillarModel.Color; }
            set
            {
                _pillarModel.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        [DisplayName("Height"), Description("Pillar Height (m)"), Category("General"), Display(Order = 0)]
        public float Height
        {
            get { return _pillarModel.Height; }
            set
            {
                _pillarModel.Height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        [DisplayName("Width"), Description("Pillar Width (m)"), Category("General"), Display(Order = 0)]
        public float Width
        {
            get { return _pillarModel.Width; }
            set
            {
                _pillarModel.Width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        [DisplayName("Length"), Description("Pillar Length (m)"), Category("General"), Display(Order = 0)]
        public float Length
        {
            get { return _pillarModel.Length; }
            set
            {
                _pillarModel.Length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        [DisplayName("APS"), Description("Average Pillar Stress (MPa)"), Category("General"), Display(Order = 0)]
        public float APS
        {
            get { return _pillarModel.APS; }
            set
            {
                _pillarModel.APS = value;
                OnPropertyChanged(nameof(APS));
            }
        }

        [DisplayName("Desired FOS"), Description("Desired Factor of Safety"), Category("General"), Display(Order = 0)]
        public float DesFOS
        {
            get { return _pillarModel.DesiredFOS; }
            set
            {
                _pillarModel.DesiredFOS = value;
                OnPropertyChanged(nameof(DesFOS));
            }
        }

        [DisplayName("Iterations"), Description("Number of sumulation run"), Category("Monte Carlo"), Display(Order = 1)]
        public int Iterations
        {
            get { return _monteCarloModel.Iterations; }
            set
            {
                _monteCarloModel.Iterations = value;
                OnPropertyChanged(nameof(Iterations));
            }
        }

        [DisplayName("Bins"), Description("Number of bins"), Category("Monte Carlo"), Display(Order = 1)]
        public int Bins
        {
            get { return _monteCarloModel.Bins; }
            set
            {
                _monteCarloModel.Bins = value;
                OnPropertyChanged(nameof(Bins));
            }
        }

        [DisplayName("UCS"), Description("Average Uniaxial Compressive strength(MPa)"), Category("Parameters"), Display(Order = 0)]
        public float UCS
        {
            get { return _lunderPakalnisModel.UCS; }
            set
            {
                _lunderPakalnisModel.UCS = value;
                OnPropertyChanged(nameof(UCS));
            }
        }

        [DisplayName("Safety Factor"), Description("Limit Safety Factor"), Category("Parameters"), Display(Order = 2)]
        public float Lsf
        {
            get { return _lunderPakalnisModel.Lsf; }
            set
            {
                _lunderPakalnisModel.Lsf = value;
                OnPropertyChanged(nameof(Lsf));
            }
        }

        [DisplayName("Confinement coeff"), Description("Coefficient of pillar confinement (coeff)"), Category("Parameters"), Display(Order = 3)]
        public float Coeff
        {
            get { return _lunderPakalnisModel.Coeff; }
            set
            {
                _lunderPakalnisModel.Coeff = value;
                OnPropertyChanged(nameof(Coeff));
            }
        }

        [DisplayName("Strength factor"), Description("Pillar strength size factor (K)"), Category("Parameters"), Display(Order = 4)]
        public float Psk
        {
            get { return _lunderPakalnisModel.Psk; }
            set
            {
                _lunderPakalnisModel.Psk = value;
                OnPropertyChanged(nameof(Psk));
            }
        }

        [DisplayName("C1"), Description("C1 Emperical Rockmass constant"), Category("Parameters"), Display(Order = 5)]
        public float C1
        {
            get { return _lunderPakalnisModel.C1; }
            set
            {
                _lunderPakalnisModel.C1 = value;
                OnPropertyChanged(nameof(C1));
            }
        }

        [DisplayName("C2"), Description("C2 Emperical Rockmass constant"), Category("Parameters"), Display(Order = 6)]
        public float C2
        {
            get { return _lunderPakalnisModel.C2; }
            set
            {
                _lunderPakalnisModel.C2 = value;
                OnPropertyChanged(nameof(C2));
            }
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Parameters")]
        public float stdLength
        {
            get { return _lunderPakalnisModel.StdLength; }
            set
            {
                _lunderPakalnisModel.StdLength = value;
                OnPropertyChanged(nameof(stdLength));
            }
        }

        [DisplayName("Std Dev Width"), Description("Std Dev of Pillar Width (m)"), Category("Parameters")]
        public float stdWidth
        {
            get { return _lunderPakalnisModel.StdWidth; }
            set
            {
                _lunderPakalnisModel.StdWidth = value;
                OnPropertyChanged(nameof(stdWidth));
            }
        }

        [DisplayName("Std Dev Height"), Description("Std Dev of Pillar Height (m)"), Category("Parameters")]
        public float stdHeight
        {
            get { return _lunderPakalnisModel.StdHeight; }
            set
            {
                _lunderPakalnisModel.StdHeight = value;
                OnPropertyChanged(nameof(stdHeight));
            }
        }

        [DisplayName("Std Dev of coeff"), Description("Std Dev of coeff"), Category("Parameters")]
        public float stdCoeff
        {
            get { return _lunderPakalnisModel.StdCoeff; }
            set
            {
                _lunderPakalnisModel.StdCoeff = value;
                OnPropertyChanged(nameof(stdCoeff));
            }
        }

        [DisplayName("Std Dev of Kappa"), Description("Std Dev of Kappa"), Category("Parameters")]
        public float stdKappa
        {
            get { return _lunderPakalnisModel.StdKappa; }
            set
            {
                _lunderPakalnisModel.StdKappa = value;
                OnPropertyChanged(nameof(stdKappa));
            }
        }

        [DisplayName("Std Dev of C1"), Description("Std Dev of C1"), Category("Parameters")]
        public float stdC1
        {
            get { return _lunderPakalnisModel.StdC1; }
            set
            {
                _lunderPakalnisModel.StdC1 = value;
                OnPropertyChanged(nameof(stdC1));
            }
        }

        [DisplayName("Std Dev of C2"), Description("Std Dev of C2"), Category("Parameters")]
        public float stdC2
        {
            get { return _lunderPakalnisModel.StdC2; }
            set
            {
                _lunderPakalnisModel.StdC2 = value;
                OnPropertyChanged(nameof(stdC2));
            }
        }

        [DisplayName("Std Dev of K"), Description("Std Dev of K"), Category("Parameters")]
        public float stdPsk
        {
            get { return _lunderPakalnisModel.StdPsk; }
            set
            {
                _lunderPakalnisModel.StdPsk = value;
                OnPropertyChanged(nameof(stdPsk));
            }
        }

        [DisplayName("Std Dev of UCS"), Description("Std Dev of UCS"), Category("Parameters")]
        public float stdUcs
        {
            get { return _lunderPakalnisModel.StdUcs; }
            set
            {
                _lunderPakalnisModel.StdUcs = value;
                OnPropertyChanged(nameof(stdUcs));
            }
        }

        [DisplayName("Std Dev of APS"), Description("Std Dev of APS"), Category("Parameters")]
        public float stdAps
        {
            get { return _lunderPakalnisModel.StdAps; }
            set
            {
                _lunderPakalnisModel.StdAps = value;
                OnPropertyChanged(nameof(stdAps));
            }
        }

    }
}
