using _SharedWpfLibrary.ViewModels;
using PillarStability.Commands;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillarStability.ViewModels
{
    public class PillarStabilityViewmodel : ViewModelBase
    {
        private readonly PillarListModel _pillarListModel;

        public PillarStabilityViewmodel(PillarListModel pillarListModel)
        {
            _addPillarCommand = new DelegateCommand(AddPillar);

            _pillarListModel = pillarListModel;
            SelectedPillar = pillarListModel.PillarModels[0];
        }

        private ICommand _addPillarCommand;

        public ICommand AddPillarCommand
        {
            get { return _addPillarCommand; }
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
            get { return _selectedPillar; }
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
                return new PillarViewModel(_selectedPillar);
            }
        }

        private void AddPillar(object obj)
        {
            _pillarListModel.PillarModels.Add(new PillarModel(DateTime.Now.ToString()));
            OnPropertyChanged(nameof(PillarList));
        }

    }
}
