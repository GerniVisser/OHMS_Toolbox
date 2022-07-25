using Caveability.Models;
using Caveability.Services;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.ViewModels.Graphs
{
    class C_GraphViewModel : GraphViewModelBase
    {
        private C_Service c_Service;

        public C_GraphViewModel(C_Model c)
        {
            c_Service = new C_Service(c);
        }

        public override string GraphName => "Stress Factor C";

        public override float getX => c_Service.getX();

        public override List<Coord> GraphLine => c_Service.CoordSerriesCalc().coords;

        public override List<Coord> GraphPoint => new List<Coord> { c_Service.CalcCoordFromX(getX) };
    }
}
