using SharedWpfLibrary.ViewModels;
using PillarStability.Commands;
using PillarStability.DataObjects;
using PillarStability.Models;
using PillarStability.Services;
using PillarStability.ViewModels.Graphs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillarStability.ViewModels
{
    public class PillarViewModel : ViewModelBase
    {
        private PillarModel _pillarModel;
        private ViewModelBase _propGridViewModel;
        private OutputGridViewModel _outputGridViewModel;
        private PillarStrengthService _strengthService;
        
        private int _selectedGraphIndex;
        private int _selectedFormulaIndex;

        private List<GraphBaseViewModel> _graphViewModelList;

        public PillarViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;

            _outputGridViewModel = new OutputGridViewModel(_pillarModel);
            setupGraphs();
            setFormulaViewModel();

            _calculateMonteCarlo = new DelegateCommand(Calculate);
        }

        private ICommand _calculateMonteCarlo;

        public ICommand CalculateMonteCarlo
        {
            get { return _calculateMonteCarlo; }
        }

        // Used by PillarStabilityViewModel to determine name of pillar
        public string Name
        {
            get { return _pillarModel.Name; }
            set 
            { 
                Name = value; 
            }
        }

        public ViewModelBase PropGridViewModel
        {
            get
            {
                return _propGridViewModel;
            }
            set
            {
                _propGridViewModel = value;
                OnPropertyChanged(nameof(PropGridViewModel));
            }
        }

        public OutputGridViewModel OutputGridViewModel 
        {
            get
            {
                return _outputGridViewModel;
            }
        }

        public GraphBaseViewModel CurrentGraphViewModel
        {
            get 
            {
                return _graphViewModelList[_selectedGraphIndex]; 
            }
        }

        public List<string> FormulaDataSource
        {
            get
            {
                return PillarStrengthOptions.Options;
            }
        }

        public int SelectedGraphIndex
        {
            get { return _selectedGraphIndex; }
            set
            {
                _selectedGraphIndex = value;
                OnPropertyChanged(nameof(CurrentGraphViewModel));
            }
        }

        public int SelectedFormulaIndex
        {
            get { return _selectedFormulaIndex; }
            set
            {
                _selectedFormulaIndex = value;
                setFormulaViewModel();
            }
        }

        private void Calculate(object obj)
        {
            MonteCarloService.updateMCLists(_pillarModel, _strengthService);

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(OutputGridViewModel));
            
            CurrentGraphViewModel.Update();
        }

        private void setFormulaViewModel()
        {

            switch (SelectedFormulaIndex)
            {
                case 0:
                    {
                        _pillarModel.PillarStrengthModel = new LunderPakalnisModel();
                        _strengthService = new LunderPakalnisService(_pillarModel);
                        PropGridViewModel = new PropGrid.LunderPakalnisPropGridVM(_pillarModel);
                        break;
                    }
                case 1:
                    {
                        _pillarModel.PillarStrengthModel = new PowerFormulaModel(0.46f, 0.66f);
                        _strengthService = new PowerFormulaService(_pillarModel);
                        PropGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 2:
                    {
                        _pillarModel.PillarStrengthModel = new PowerFormulaModel(0.5f, 0.75f);
                        _strengthService = new PowerFormulaService(_pillarModel);
                        PropGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 3:
                    {
                        _pillarModel.PillarStrengthModel = new PowerFormulaModel(0.5f, 0.70f);
                        _strengthService = new PowerFormulaService(_pillarModel);
                        PropGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 4:
                    {
                        _pillarModel.PillarStrengthModel = new PowerFormulaModel(0.76f, 0.36f);
                        _strengthService = new PowerFormulaService(_pillarModel);
                        PropGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 5:
                    {
                        _pillarModel.PillarStrengthModel = new PowerFormulaModel(0.67f, 0.32f);
                        _strengthService = new PowerFormulaService(_pillarModel);
                        PropGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }

                default:
                    {
                        _pillarModel.PillarStrengthModel = new PowerFormulaModel(0.67f, 0.32f);
                        _strengthService = new PowerFormulaService(_pillarModel);
                        PropGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
            }

            Calculate(null);
            OnPropertyChanged(nameof(CurrentGraphViewModel));
        }

        private void setupGraphs()
        {
            _graphViewModelList = new List<GraphBaseViewModel>();
            _graphViewModelList.Add(new WH_GraphVM(_pillarModel));
            _graphViewModelList.Add(new MonteCarlo_GraphVM(_pillarModel));
        }
    }
}
