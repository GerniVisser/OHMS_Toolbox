using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.PropGrid
{
    public class PowerFormulaPropGridVM : ViewModelBase
    {
        private PillarModel _pillarModel;
        private MonteCarloModel _monteCarloModel;
        private PowerFormulaModel _powerFormulaModel;

        public PowerFormulaPropGridVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _monteCarloModel = pillarModel.MonteCarloModel;
            _powerFormulaModel = (PowerFormulaModel)_pillarModel.MonteCarloModel;
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

        [DisplayName("Alpha"), ReadOnly(true), Description("Alpha value"), Category("Parameters"), Display(Order = 2)]
        public float Alpha
        {
            get { return _powerFormulaModel.Alpha; }
            set
            {
                _powerFormulaModel.Alpha = value;
                OnPropertyChanged(nameof(Alpha));
            }
        }

        [DisplayName("Beta"), ReadOnly(true), Description("Beta value"), Category("Parameters"), Display(Order = 2)]
        public float Beta
        {
            get { return _powerFormulaModel.Beta; }
            set
            {
                _powerFormulaModel.Beta = value;
                OnPropertyChanged(nameof(Beta));
            }
        }

        [DisplayName("K"), Description("Back-fit ubit-strength"), Category("Parameters"), Display(Order = 2)]
        public float K
        {
            get { return _powerFormulaModel.K; }
            set
            {
                _powerFormulaModel.K = value;
                OnPropertyChanged(nameof(K));
            }
        }

        [DisplayName("Std of K"), Description("Standard Deviation of K"), Category("Parameters"), Display(Order = 2)]
        public float stdK
        {
            get { return _powerFormulaModel.StdK; }
            set
            {
                _powerFormulaModel.StdK = value;
                OnPropertyChanged(nameof(stdK));
            }
        }

        [DisplayName("Std of Width"), Description("Standard Deviation of Width"), Category("Parameters"), Display(Order = 2)]
        public float stdWidth
        {
            get { return _powerFormulaModel.StdWidth; }
            set
            {
                _powerFormulaModel.StdWidth = value;
                OnPropertyChanged(nameof(stdWidth));
            }
        }

        [DisplayName("Std of Height"), Description("Standard Deviation of Height"), Category("Parameters"), Display(Order = 2)]
        public float stdHeight
        {
            get { return _powerFormulaModel.StdHeight; }
            set
            {
                _powerFormulaModel.StdHeight = value;
                OnPropertyChanged(nameof(stdHeight));
            }
        }
    }
}
