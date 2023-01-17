using System.Collections.Generic;

namespace PillarStability.Models
{
    // Not Inuse but keeping as feature might be reintroduced in future
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
