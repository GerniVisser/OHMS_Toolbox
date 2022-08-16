using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class PillarListModel
    {
        private List<PillarModel> _pillarModels;

        public PillarListModel()
        {
            _pillarModels = new List<PillarModel>();
        }

        public List<PillarModel> PillarModels
        {
            get 
            {
                if (_pillarModels == null)
                    _pillarModels = new List<PillarModel>();

                return _pillarModels; 
            }
            set { _pillarModels = value; }
        }

    }
}
