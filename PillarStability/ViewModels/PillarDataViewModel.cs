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

        public PillarDataViewModel(PillarModel pillarModel, ViewModelBase graphViewModel)
        {
            _pillarModel = pillarModel;
            _pillarDataGridViewModel = new PillarDataGridViewModel(_pillarModel) ;
            GraphViewModel = graphViewModel;
        }

        public ViewModelBase GraphViewModel
        {
            get { return _graphViewModel; }
            set 
            { 
                _graphViewModel = value;
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

    }
}
