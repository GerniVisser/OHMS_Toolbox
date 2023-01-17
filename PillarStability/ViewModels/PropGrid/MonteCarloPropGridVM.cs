using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using PillarStability.Utils;
using Syncfusion.Windows.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels.PropGrid
{
    [PropertyGridAttribute(NestedPropertyDisplayMode = NestedPropertyDisplayMode.Show, PropertyName = "SimModel"), Editor("SimAlgorithm", typeof(ComboEditor))]
    public class MonteCarloPropGridVM: ViewModelBase
    {
        private MonteCarloModel _monteCarloModel;
        private PillarModel _pillarModel;

        public MonteCarloPropGridVM(PillarModel pillarModel)
        {
            _monteCarloModel = pillarModel.MonteCarloModel;
            _pillarModel = pillarModel;
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

        private string _simAlgorithm = "Hello";

        [DisplayName("Algorithm"), RefreshProperties(RefreshProperties.All), Description("Simulation Algorithm"), Category("Simulation Model")]
        public string SimAlgorithm
        {
            get { return _simAlgorithm; }
            set 
            {
                _simAlgorithm = value;
                if (_simAlgorithm == "Lunder and Pakalnis")
                {
                    _pillarModel.MonteCarloModel = new LunderPakalnisModel();
                    SimModel = new LunderPakalnisPropGridVM((LunderPakalnisModel)_pillarModel.MonteCarloModel);
                }
                else if (_simAlgorithm == "Salamon and Munro")
                {
                    _pillarModel.MonteCarloModel = new PowerFormulaModel(0.46f,0.66f);
                    SimModel = new PowerFormulaPropGridVM((PowerFormulaModel)_pillarModel.MonteCarloModel);
                }
                else if (_simAlgorithm == "Hedley and Grant")
                {
                    _pillarModel.MonteCarloModel = new PowerFormulaModel(0.5f, 0.75f);
                    SimModel = new PowerFormulaPropGridVM((PowerFormulaModel)_pillarModel.MonteCarloModel);
                }
                else if (_simAlgorithm == "Stacey and Page")
                {
                    _pillarModel.MonteCarloModel = new PowerFormulaModel(0.5f, 0.7f);
                    SimModel = new PowerFormulaPropGridVM((PowerFormulaModel)_pillarModel.MonteCarloModel);
                }
                else if (_simAlgorithm == "PlatMine Merensky")
                {
                    _pillarModel.MonteCarloModel = new PowerFormulaModel(0.76f, 0.36f);
                    SimModel = new PowerFormulaPropGridVM((PowerFormulaModel)_pillarModel.MonteCarloModel);
                }
                else if (_simAlgorithm == "PlatMine UG2")
                {
                    _pillarModel.MonteCarloModel = new PowerFormulaModel(0.67f, 0.32f);
                    SimModel = new PowerFormulaPropGridVM((PowerFormulaModel)_pillarModel.MonteCarloModel);
                }
            }
        }

        private ViewModelBase _simModel;

        [DisplayName("Model"), Description("Simulation Model"), Category("Parameters"), PropertyGridAttribute(NestedPropertyDisplayMode = NestedPropertyDisplayMode.Show)]
        public ViewModelBase SimModel
        {
            get { return _simModel; }
            set 
            { 
                _simModel = value;
                OnPropertyChanged(nameof(SimModel));
            }
        }

    }
}
