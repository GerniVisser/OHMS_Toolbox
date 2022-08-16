using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels
{
    public class PillarViewModel : ViewModelBase
    {
        private PillarModel _pillarModel;
        private ViewModelBase _propGridViewModel;

        public PillarViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _selectedViewIndex = 0;
        }

        public string Name
        {
            get { return _pillarModel.Name; }
            set 
            { 
                Name = value; 
            }
        }

        private int _selectedViewIndex;

        public int SelectedViewIndex
        {
            get { return _selectedViewIndex; }
            set 
            {
                _selectedViewIndex = value;

                if(_selectedViewIndex == 2)
                {
                    _propGridViewModel = new PropGrid.MonteCarloPropGridVM(_pillarModel);
                }
                else _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);

                // Subscribe to PropGridModel Property Changed 
                _propGridViewModel.PropertyChanged += HandlePropGridChange;

                OnPropertyChanged(nameof(CurrentViewModel));
                OnPropertyChanged(nameof(PropGridViewModel));
            }
        }

        private void HandlePropGridChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public ViewModelBase PropGridViewModel
        {
            get
            {
                return _propGridViewModel;
            }
        }


        public ViewModelBase CurrentViewModel
        {
            get
            {
                switch (_selectedViewIndex)
                {
                    case 0: return new PillarDataViewModel(_pillarModel, new Graphs.WH_GraphVM(_pillarModel));
                    case 1: return new PillarDataViewModel(_pillarModel, new Graphs.Confinement_GraphVM(_pillarModel));
                    case 2: return new MonteCarloViewModel(_pillarModel);
                    default: return new PillarDataViewModel(_pillarModel, new Graphs.WH_GraphVM(_pillarModel));
                }
            }
        }
    }
}
