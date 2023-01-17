using _SharedWpfLibrary.ViewModels;
using PillarStability.DataObjects;
using PillarStability.Models;
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

            _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);
            _pillarDataGridViewModel = new PillarDataGridViewModel(_pillarModel);

            SelectedGraphIndex = 0;
            SelectedFormulaIndex = 0;

            SetFormulaViewModel();

        }

        public string Name
        {
            get { return _pillarModel.Name; }
            set 
            { 
                Name = value; 
            }
        }

        private void HandlePropGridChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(GraphViewModel));
            OnPropertyChanged(nameof(PillarDataGrid));
            OnPropertyChanged(nameof(Name));
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

        private void SetFormulaViewModel()
        {
            // WH View
            if (SelectedFormulaIndex == 0)
            {
                _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);
            }
            // MonteCarlo View
            else if (SelectedFormulaIndex == 1)
            {
                _propGridViewModel = new PropGrid.MonteCarloPropGridVM(_pillarModel);
            }
            // Default data View - WH View
            else
            {
                _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);
            }

            OnPropertyChanged(nameof(PropGridViewModel));

            // Subscribe to PropGridModel Property Changed 
            PropGridViewModel.PropertyChanged += HandlePropGridChange;
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
