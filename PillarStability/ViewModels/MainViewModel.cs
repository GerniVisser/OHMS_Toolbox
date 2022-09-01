using _SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private PillarStabilityViewModel _pillarStabilityViewModel;

        public PillarStabilityViewModel PillarStabilityViewModel
        {
            get { return _pillarStabilityViewModel; }
            set { _pillarStabilityViewModel = value; }
        }

        public MainViewModel(PillarStabilityViewModel pillarStabilityModel)
        {
            _pillarStabilityViewModel = new PillarStabilityViewModel(new Models.PillarListModel());
        }
    }
}
