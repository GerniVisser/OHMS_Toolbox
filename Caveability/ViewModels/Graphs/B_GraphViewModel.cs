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
    class B_GraphViewModel : GraphViewModelBase
    {
        private B_Service b_Service;

        public B_GraphViewModel(B_Model b)
        {
            b_Service = new B_Service(b);
        }

        public override string GraphName => "Stress Factor B";

        public override float getX => b_Service.getX();

        public override List<Coord> GraphLine => b_Service.CoordSerriesCalc().coords;

        public override List<Coord> GraphPoint => new List<Coord> { b_Service.CalcCoordFromX(getX) };
    }
}
