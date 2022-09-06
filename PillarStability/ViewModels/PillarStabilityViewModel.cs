using _SharedWpfLibrary.ViewModels;
using PillarStability.Commands;
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
    public class PillarStabilityViewModel : ViewModelBase
    {
        private readonly PillarListModel _pillarListModel;
        private int _pillarCounter;

        public PillarStabilityViewModel(PillarListModel pillarListModel)
        {
            _pillarCounter = 1;
            _addPillarCommand = new DelegateCommand(AddPillar);
            _removePillarCommand = new DelegateCommand(RemovePillar);

            _pillarListModel = pillarListModel;
            AddPillar(null);
            SelectedPillar = pillarListModel.PillarModels[0];
        }

        private ICommand _addPillarCommand;

        public ICommand AddPillarCommand
        {
            get { return _addPillarCommand; }
        }

        private ICommand _removePillarCommand;

        public ICommand RemovePillarCommand
        {
            get { return _removePillarCommand; }
        }


        public ObservableCollection<PillarModel> PillarList
        {
            // Check if this is good convention
            // Seems ineffecient to create new Observable collection everytime Model is Updated
            // List did not seem to work
            get
            {
                return new ObservableCollection<PillarModel>(_pillarListModel.PillarModels);
            }
        }

        private PillarModel _selectedPillar;

        public PillarModel SelectedPillar
        {
            get 
            {
                if (!PillarList.Contains(_selectedPillar))
                {
                    _selectedPillar = null;
                }
                return _selectedPillar; 
            }
            set 
            {
                _selectedPillar = value;
                OnPropertyChanged(nameof(SelectedPillar));
                OnPropertyChanged(nameof(PillarViewModel));
            }
        }

        public PillarViewModel PillarViewModel
        {
            get
            {
                if (_selectedPillar == null) return null;
                PillarViewModel newPVM = new PillarViewModel(_selectedPillar);
                newPVM.PropertyChanged += HandleNameChange;
                return newPVM;
            }
        }

        private void HandleNameChange(object sender, PropertyChangedEventArgs e)
        {
            // If Name of pillar is changed from Propgrid this will trigger 
            if( e.PropertyName == "Name")
            {
                OnPropertyChanged(nameof(PillarList));
            }
        }

        private void AddPillar(object obj)
        {
            _pillarListModel.PillarModels.Add(new PillarModel("Pillar " + _pillarCounter));
            OnPropertyChanged(nameof(PillarList));
            _pillarCounter++;
        }

        private void RemovePillar(object obj)
        {
            try
            {
                var t = _pillarListModel.PillarModels.Remove((PillarModel)obj);
                OnPropertyChanged(nameof(PillarList));
                OnPropertyChanged(nameof(SelectedPillar));
            }
            catch
            {

            }
        }
    }
}
