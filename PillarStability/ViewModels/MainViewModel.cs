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
        private PillarStabilityViewmodel _pillarStabilityViewModel;

        public PillarStabilityViewmodel PillarStabilityViewModel
        {
            get { return _pillarStabilityViewModel; }
            set { _pillarStabilityViewModel = value; }
        }

        public MainViewModel(PillarStabilityViewmodel pillarStabilityModel)
        {
            _pillarStabilityViewModel = new PillarStabilityViewmodel(new Models.PillarListModel());
        }
    }
}
