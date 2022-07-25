using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class C_Model
    {
        public C_Model(float c, bool isFootwall)
        {
            this.c = c;
            this.isFootwall = isFootwall;
        }
        // Use models a primary strore and just dervive all values in Viewmodels from here

        public float c { get; set; }
        public bool isFootwall { get; set; }
    }
}
