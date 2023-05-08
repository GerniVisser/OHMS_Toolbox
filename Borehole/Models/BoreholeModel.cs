using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borehole.Models
{
    public class BoreholeModel
    {
        public BoreholeModel()
        {
            geoTechModel = new GeoTechModel();
            litModel = new LitModel();
        }

        public GeoTechModel geoTechModel { get; set; }
        public LitModel litModel { get; set; }
    }
}
