using _SharedWpfLibrary.ViewModels;
using PillarStability.Commands;
using PillarStability.DataObjects;
using PillarStability.Models;
using PillarStability.Services;
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
        private ViewModelBase _graphViewModel;
        private PillarDataGridViewModel _pillarDataGridViewModel;
        private int _selectedGraphIndex;
        private int _selectedFormulaIndex;
        private PillarStrengthOptions _strengthOptions;

        public PillarViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _calculateMonteCarlo = new DelegateCommand(Calculate);

            _pillarDataGridViewModel = new PillarDataGridViewModel(_pillarModel);

            SelectedGraphIndex = 0;
            SelectedFormulaIndex = 0;

            SetFormulaViewModel();

        }

        private ICommand _calculateMonteCarlo;

        public ICommand CalculateMonteCarlo
        {
            get { return _calculateMonteCarlo; }
        }

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
        }

        public int SelectedGraphIndex
        {
            get { return _selectedGraphIndex; }
            set
            {
                //if (_selectedViewIndex == value) return;
                _selectedGraphIndex = value;

                SetGraphViewModel();
            }
        }

        public int SelectedFormulaIndex
        {
            get { return _selectedFormulaIndex; }
            set
            {
                //if (_selectedViewIndex == value) return;
                _selectedFormulaIndex = value;

                SetFormulaViewModel();
            }
        }

        public ViewModelBase GraphViewModel
        {
            get { return _graphViewModel; }
            set
            {
                _graphViewModel = value;
                OnPropertyChanged(nameof(GraphViewModel));
            }
        }

        public List<string> FormulaDataSource
        {
            get
            {
                if(_strengthOptions == null)
                {
                    _strengthOptions = new PillarStrengthOptions();
                }
                return _strengthOptions.Options;
            }
        }

        public Collection<PillarDataGridViewModel> PillarDataGrid
        {
            get
            {
                // Could not find a better way for ObservableCollection to update the DataGrid without createing a new instance of the Collection
                return new Collection<PillarDataGridViewModel>() { _pillarDataGridViewModel };
            }
        }

        private void Calculate(object obj)
        {
            OnPropertyChanged(nameof(GraphViewModel));
            OnPropertyChanged(nameof(PillarDataGrid));
            OnPropertyChanged(nameof(Name));
        }

        private void SetFormulaViewModel()
        {

            switch (SelectedFormulaIndex)
            {
                case 0:
                    {
                        _pillarModel.MonteCarloModel = new LunderPakalnisModel();
                        _propGridViewModel = new PropGrid.LunderPakalnisPropGridVM(_pillarModel);
                        break;
                    }
                case 1:
                    {
                        _pillarModel.MonteCarloModel = new PowerFormulaModel(0.46f, 0.66f);
                        _propGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 2:
                    {
                        _pillarModel.MonteCarloModel = new PowerFormulaModel(0.5f, 0.75f);
                        _propGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 3:
                    {
                        _pillarModel.MonteCarloModel = new PowerFormulaModel(0.5f, 0.70f);
                        _propGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 4:
                    {
                        _pillarModel.MonteCarloModel = new PowerFormulaModel(0.76f, 0.36f);
                        _propGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
                case 5:
                    {
                        _pillarModel.MonteCarloModel = new PowerFormulaModel(0.67f, 0.32f);
                        _propGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }

                default:
                    {
                        _pillarModel.MonteCarloModel = new PowerFormulaModel(0.67f, 0.32f);
                        _propGridViewModel = new PropGrid.PowerFormulaPropGridVM(_pillarModel);
                        break;
                    }
            }

            OnPropertyChanged(nameof(PropGridViewModel));
        }


        private void SetGraphViewModel()
        {
            // WH View
            if (_selectedGraphIndex == 0)
            {
                GraphViewModel = new Graphs.WH_GraphVM(_pillarModel);
            }
            // MonteCarlo View
            else if (_selectedGraphIndex == 1)
            {
                GraphViewModel = new Graphs.Confinement_GraphVM(_pillarModel);
            }
            // Default data View - WH View
            else
            {
                GraphViewModel = new Graphs.WH_GraphVM(_pillarModel);
            }
        }
    }
}
