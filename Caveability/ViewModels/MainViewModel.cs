using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedWpfLibrary.ViewModels;
using Caveability.Models;
using Caveability.ViewModels;

namespace Caveability.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private CaveabilityViewModel _caveabilityViewModel;

        public CaveabilityViewModel CaveabilityViewModel
        {
            get { return _caveabilityViewModel; }
            set { _caveabilityViewModel = value; }
        }

        public MainViewModel(CaveabilityModel caveabilityModel)
        {
            _caveabilityViewModel = new CaveabilityViewModel(caveabilityModel);

        }
    }
}
