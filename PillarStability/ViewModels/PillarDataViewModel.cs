using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels
{
    public class PillarDataViewModel: ViewModelBase
    {
        private PillarModel _pillarModel;
        private ViewModelBase _graphViewModel;

        public PillarDataViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _pillarDataGridViewModel = new PillarDataGridViewModel(_pillarModel) ;
            SelectedGraphIndex = 0;
        }

        private int _selectedGraphIndex;

        public int SelectedGraphIndex
        {
            get { return _selectedGraphIndex; }
            set
            {
                //if (_selectedViewIndex == value) return;
                _selectedGraphIndex = value;

                SetViewModel();
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

        private PillarDataGridViewModel _pillarDataGridViewModel;

        public Collection<PillarDataGridViewModel> PillarDataGrid
        {
            get
            {
                // Could not find a better way for ObservableCollection to update the DataGrid without createing a new instance of the Collection
                return new Collection<PillarDataGridViewModel>() { _pillarDataGridViewModel };
            }
        }

        private void SetViewModel()
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
