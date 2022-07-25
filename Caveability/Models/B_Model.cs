using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class B_Model
    {
        public B_Model(float b)
        {
            this.b = b;
        }
        // Use models a primary strore and just dervive all values in Viewmodels from here

        public float b { get; set; }
    }
}
