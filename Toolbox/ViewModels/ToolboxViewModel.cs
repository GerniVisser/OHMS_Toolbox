using SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Toolbox.Store;

namespace Toolbox.ViewModels
{
    public class ToolboxViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private int _selctedToolIndex;

        public int SelctedToolIndex
        {
            get { return _selctedToolIndex; }
            set 
            { 
                _selctedToolIndex = value;
                _navigationStore.setCurrentViewModelIndex(value);
            }
        }

        public ToolboxViewModel(NavigationStore navigationService)
        {
            _navigationStore = navigationService;
            _navigationStore.PropertyChanged += HandlePropGridChange;
            _selctedToolIndex = 0;
        }

        private void HandlePropGridChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
