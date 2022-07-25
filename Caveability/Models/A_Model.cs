using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class A_Model
    {
        public A_Model(float oc, float omax)
        {
            this.oc = oc;
            this.omax = omax;
        }

        // Use models a primary strore and just dervive all values in Viewmodels from here

        public float oc { get; set; }
        public float omax { get; set; }
    }
}
