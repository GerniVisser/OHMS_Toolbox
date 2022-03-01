using PillarStability.Helper;
using System.Collections.Generic;
using System.IO;

namespace PillarStability.Models
{
    public class ReportModel
    {
        public Stream whStream { get; set; }
        public Stream aveStream { get; set; }
        public Stream mcStream { get; set; }
        public List<PillarPrams> pillarPrams { get; set; }
        public List<OutputGridObject> outGridObjects { get; set; }
        public MCGridObject mcGridObject { get; set; }
    }
}
