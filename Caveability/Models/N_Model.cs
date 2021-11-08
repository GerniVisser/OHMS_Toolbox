using Caveability.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    public class N_Model
    {
        public static double Calculate(double q, double a, double b, double c)
        {
            return q * a * b * c;
        }
    }
}
