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
            SelectedViewIndex = 0;

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
                //if (_selectedViewIndex == value) return;
                _selectedViewIndex = value;

                SetViewModel();
            }
        }

        private void HandlePropGridChange(object sender, PropertyChangedEventArgs e)
        {
            _currentViewModel.Notify("GraphViewModel");
        }


        public ViewModelBase PropGridViewModel
        {
            get
            {
                return _propGridViewModel;
            }
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
        }

        private void SetViewModel()
        {
            // WH View
            if (_selectedViewIndex == 0)
            {
                _currentViewModel = new PillarDataViewModel(_pillarModel, new Graphs.WH_GraphVM(_pillarModel));
                _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);
            }
            // Confinement View
            else if (_selectedViewIndex == 1)
            {
                _currentViewModel = new PillarDataViewModel(_pillarModel, new Graphs.Confinement_GraphVM(_pillarModel));
                _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);
            }
            // MonteCarlo View
            else if (_selectedViewIndex == 2)
            {
                _currentViewModel = new MonteCarloViewModel(_pillarModel);
                _propGridViewModel = new PropGrid.MonteCarloPropGridVM(_pillarModel);
            }
            // Default data View - WH View
            else
            {
                _currentViewModel = new PillarDataViewModel(_pillarModel, new Graphs.WH_GraphVM(_pillarModel));
                _propGridViewModel = new PropGrid.PillarPropGridVM(_pillarModel);
            }

            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(PropGridViewModel));

            // Subscribe to PropGridModel Property Changed 
            PropGridViewModel.PropertyChanged += HandlePropGridChange;
        }
    }
}
