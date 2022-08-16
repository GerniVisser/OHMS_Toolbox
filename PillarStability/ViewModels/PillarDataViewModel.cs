using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels
{
    public class PillarDataViewModel: ViewModelBase
    {
        private PillarModel _pillarModel;
        private ViewModelBase _graphViewModel;

        public PillarDataViewModel(PillarModel pillarModel, ViewModelBase graphViewModel)
        {
            _pillarModel = pillarModel;
            _graphViewModel = graphViewModel;
        }

        public ViewModelBase GraphViewModel
        {
            get { return _graphViewModel; }
            set { _graphViewModel = value; }
        }

    }
}
