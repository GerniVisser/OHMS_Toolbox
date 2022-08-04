using _SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Store
{
    public class NavigationStore : ViewModelBase
    {
        private List<ViewModelBase> _viewModelList;
        private ViewModelBase _currentViewModel;

        public NavigationStore(List<ViewModelBase> viewModelList)
        {
            _viewModelList = viewModelList;
            CurrentViewModel = _viewModelList[0];
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public void setCurrentViewModelIndex(int index) {
            CurrentViewModel = _viewModelList[index];
        }
    }
}
