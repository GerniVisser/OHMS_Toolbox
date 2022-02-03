using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class CombinedPillarModel
    {
        private List<PillarModel> _pillarModels;

        public CombinedPillarModel(List<PillarModel> pillarModels)
        {
            _pillarModels = pillarModels;
        }

        public List<PillarModel> PillarModels
        {
            get { return _pillarModels; }
            set { _pillarModels = value; }
        }
    }

}
