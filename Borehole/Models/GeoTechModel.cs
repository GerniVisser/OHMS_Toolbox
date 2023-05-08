using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borehole.Models
{
    public class GeoTechModel
    {
        public string filepath { get; set; }
        public List<string> colomns { get; set; }
        public int IdColumn { get; set; }
        public int distColumn { get; set; }

    }
}
