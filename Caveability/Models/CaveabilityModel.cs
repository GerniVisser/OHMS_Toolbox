using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class CaveabilityModel
    {
        private List<WallModel> _wallModels;

        public CaveabilityModel()
        {
            _wallModels = new List<WallModel>()
            {
                new WallModel("Hangwall"),
                new WallModel("Footwall", true),
                new WallModel("Stope Back"),
                new WallModel("Strike End"),
            };
        }

        public WallModel Hangwall { get => _wallModels[0]; }
        public WallModel Footwall { get => _wallModels[1]; }
        public WallModel StopeBack { get => _wallModels[2]; }
        public WallModel StrikeEnd { get => _wallModels[3]; }
    }
}
