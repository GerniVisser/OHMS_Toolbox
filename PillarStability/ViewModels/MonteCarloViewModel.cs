using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.ViewModels
{
    public class MonteCarloViewModel: ViewModelBase
    {
        private PillarModel _pillarModel;
        private MonteCarloModel _monteCarloModel;

        public MonteCarloViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _monteCarloModel = _pillarModel.MonteCarloModel;
        }
    }
}
