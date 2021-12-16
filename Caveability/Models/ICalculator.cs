using Caveability.Helper;
using System.Collections.Generic;

namespace Caveability.Models
{
    interface ICalculator
    {
        float Calculate();
        List<Coord> GetGraphCoords();
    }
}
