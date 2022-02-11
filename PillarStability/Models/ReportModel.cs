using PillarStability.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.Models
{
    public class ReportModel
    {
        public Stream whStream { get; set; }
        public Stream aveStream { get; set; }
        public List<PillarPrams> PillarPrams { get; set; }
        public List<OutputGridObject> outGridObjects { get; set; }
    }
}
