using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class Q_Model
    {
        public Q_Model(float rqd, float jn, float jr, float ja)
        {
            this.rqd = rqd;
            this.jn = jn;
            this.jr = jr;
            this.ja = ja;
        }
        // Use models a primary strore and just dervive all values in Viewmodels from here
        public float rqd { get; set; }
        public float jn { get; set; }
        public float jr { get; set; }
        public float ja { get; set; }
    }
}
