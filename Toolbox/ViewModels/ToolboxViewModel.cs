using _SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
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

        public ToolboxViewModel(NavigationStore navigationService)
        {
            _navigationStore = navigationService;
        }
    }
}
