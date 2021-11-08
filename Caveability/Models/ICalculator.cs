using System;
using Caveability.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Models
{
    interface ICalculator
    {
        double Calculate();
        List<Coord> GetGraphCoords();
    }
}
