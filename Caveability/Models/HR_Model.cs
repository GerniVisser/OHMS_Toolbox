using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class HR_Model
    {
        public HR_Model(float width, float height)
        {
            Width = width;
            Height = height;
        }
        // Use models a primary strore and just dervive all values in Viewmodels from here
        public float Width { get; set; }
        public float Height { get; set; }

    }
}
