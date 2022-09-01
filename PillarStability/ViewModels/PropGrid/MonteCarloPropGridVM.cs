using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using Syncfusion.Windows.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.PropGrid
{
    [PropertyGridAttribute(NestedPropertyDisplayMode = NestedPropertyDisplayMode.Show, PropertyName = "SimModel")]
    public class MonteCarloPropGridVM: ViewModelBase
    {
        private MonteCarloModel _monteCarloModel;

        public MonteCarloPropGridVM(PillarModel pillarModel)
        {
            _monteCarloModel = pillarModel.MonteCarloModel;
            _simModel = new LunderPakalnisPropGridVM(_monteCarloModel);
        }

        [DisplayName("Iterations"), Description("Number of sumulation run"), Category("Monte Carlo")]
        public int Iterations
        {
            get { return _monteCarloModel.Iterations; }
            set
            {
                _monteCarloModel.Iterations = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        [DisplayName("Bins"), Description("Number of bins"), Category("Monte Carlo")]
        public int Bins
        {
            get { return _monteCarloModel.Bins; }
            set
            {
                _monteCarloModel.Bins = value;
                OnPropertyChanged(nameof(MonteCarloPropGridVM));
            }
        }

        private ViewModelBase _simModel;

        [DisplayName("Model"), Description("Simulation Model"), Category("Simulation Model"), PropertyGridAttribute(NestedPropertyDisplayMode = NestedPropertyDisplayMode.Show)]
        public ViewModelBase SimModel
        {
            get { return _simModel; }
            set { _simModel = value; }
        }


    }
}
