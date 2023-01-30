using SharedWpfLibrary.ViewModels;
using Microsoft.Win32;
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
using PillarStability.DataObjects;
using SharedWpfLibrary.Service;

namespace PillarStability.ViewModels
{
    public class PillarStabilityViewModel : ViewModelBase
    {
        private int _pillarCounter;
        private CombinedPillarViewModel _combinedPillarViewModel;
        private PillarModel _combinedPillarModel;
        private string _filePath = "";

        public PillarStabilityViewModel(PillarListModel pillarListModel)
        {
            _pillarCounter = 1;

            _addPillarCommand = new DelegateCommand(AddPillar);
            _removePillarCommand = new DelegateCommand(RemovePillar);

            _open = new DelegateCommand(open);
            _saveAs = new DelegateCommand(saveAs);
            _save = new DelegateCommand(save);
            _removePillarCommand = new DelegateCommand(RemovePillar);

            _combinedPillarModel = new PillarModel("Combined");
            _pillarModelList = new ObservableCollection<PillarModel>(pillarListModel.PillarModels);
            AddPillar(null);

            _combinedPillarViewModel = new CombinedPillarViewModel(_pillarModelList);
            SelectedPillar = _pillarModelList[0];

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

        private ICommand _open;

        public ICommand Open
        {
            get { return _open; }
        }

        private ICommand _saveAs;

        public ICommand SaveAs
        {
            get { return _saveAs; }
        }

        private ICommand _save;

        public ICommand Save
        {
            get { return _save; }
        }

        private ObservableCollection<PillarModel> _pillarModelList;
        public ObservableCollection<PillarModel> PillarModelList
        { 
            get 
            {
                return _pillarModelList;
            }
        }
        public List<PillarModel> PillarTabList
        {
            get
            {
                return new List<PillarModel>(_pillarModelList).Prepend(_combinedPillarModel).ToList();
            }
        }

        private PillarModel _selectedPillar;

        public PillarModel SelectedPillar
        {
            get 
            {
                if (!_pillarModelList.Contains(_selectedPillar))
                {
                    _selectedPillar = _combinedPillarModel;
                }
                return _selectedPillar; 
            }
            set 
            {
                _selectedPillar = value;
                OnPropertyChanged(nameof(SelectedPillar));
                OnPropertyChanged(nameof(ContentViewModel));
            }
        }

        public ViewModelBase ContentViewModel
        {
            get
            {
                // if null or combined pillar return Combined PillarViewModel
                if (_selectedPillar == null || _selectedPillar.Name == "Combined")
                {
                    return _combinedPillarViewModel;
                }
                else
                {
                    PillarViewModel newPVM = new PillarViewModel(_selectedPillar);
                    newPVM.PropertyChanged += HandleNameChange;
                    return newPVM;
                }
            }
        }

        private void open(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                List<PillarSaveDataObject> objectlist = SaveLoadService.LoadFromCsv<PillarSaveDataObject>(filePath);

                _pillarModelList.Clear();

                _pillarCounter = 0;

                foreach (var item in objectlist)
                {
                    _pillarModelList.Add(
                        new PillarModel("") 
                        {
                            Name = item.Name,
                            Height = item.Height,
                            Width = item.Width,
                            Length = item.Length,
                            APS = item.APS,
                            DesiredFOS = item.DesiredFOS,
                            Color = item.Color,
                        }
                    );
                    _pillarCounter++;
                }
                SelectedPillar = _pillarModelList[0];
                OnPropertyChanged(nameof(PillarTabList));
                _filePath = filePath;
            }

        }

        private void saveAs(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == true)
            {
                _filePath = saveFileDialog.FileName;

                save(null);
            }
        }

        private void save(object obj)
        {
            if (_filePath == null || _filePath == "")
            {
                saveAs(null);
                return;
            }

            try
            {
                List<PillarSaveDataObject> dataObjects = new List<PillarSaveDataObject>();

                foreach (var item in _pillarModelList)
                {
                    var dataobject = new PillarSaveDataObject
                    {
                        Name = item.Name,
                        Height = item.Height,
                        Width = item.Width,
                        Length = item.Length,
                        APS = item.APS,
                        DesiredFOS = item.DesiredFOS,
                        Color = item.Color,
                    };
                    dataObjects.Add(dataobject);
                }

                SaveLoadService.SaveListToCsv(dataObjects, _filePath);
            }
            catch
            {
                throw new Exception("File could not be saved to this filepath: " + _filePath);
            }
        }

        private void AddPillar(object obj)
        {
            _pillarModelList.Add(new PillarModel("Pillar " + _pillarCounter));
            _pillarCounter++;
            OnPropertyChanged(nameof(PillarTabList));
        }

        private void RemovePillar(object obj)
        {
            _pillarModelList.Remove((PillarModel)obj);
            OnPropertyChanged(nameof(SelectedPillar));
            OnPropertyChanged(nameof(PillarTabList));
        }

        private void HandleNameChange(object sender, PropertyChangedEventArgs e)
        {
            // If Name of pillar is changed from Propgrid this will trigger 
            if (e.PropertyName == "Name")
            {
                OnPropertyChanged(nameof(PillarListModel));
            }
        }
    }
}
