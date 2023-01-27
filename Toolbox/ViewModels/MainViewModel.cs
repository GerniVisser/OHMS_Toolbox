using SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Store;

namespace Toolbox.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private ToolboxViewModel _toolboxViewModel;

        public ToolboxViewModel ToolboxViewModel
        {
            get { return _toolboxViewModel; }
            set { _toolboxViewModel = value; }
        }

        public MainViewModel(NavigationStore navigationStore)
        {
            _toolboxViewModel = new ToolboxViewModel(navigationStore);

        }
    }
}
