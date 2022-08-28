using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarStability.DataObjects
{
    public class MonteCarloDataObject
    {
        public string Pillar { get; set; }
        public float DSF { get; set; }
        public float AveSF { get; set; }
        public float StandardDev { get; set; }
        public float mfSF { get; set; }
        public float probSF { get; set; }
    }
}
