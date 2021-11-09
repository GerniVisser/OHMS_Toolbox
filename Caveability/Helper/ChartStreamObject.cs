using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Helper
{
    public class ChartStreamObject
    {
        public Stream A_chartStream { get; set; }
        public Stream B_chartStream { get; set; }
        public Stream C_chartStream { get; set; }
        public Stream HR_chartStream { get; set; }
    }
}
