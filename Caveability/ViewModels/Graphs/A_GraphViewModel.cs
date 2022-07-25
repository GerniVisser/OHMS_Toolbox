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
    class A_GraphViewModel : GraphViewModelBase
    {
        private A_Service a_Service;

        public A_GraphViewModel(A_Model a_Model)
        {
            a_Service = new A_Service(a_Model);
        }

        //Public Methods accessable by WallViewModel
        public override string GraphName => "Stress Factor A";
        public override float getX => a_Service.getX();
        public override List<Coord> GraphLine => a_Service.CoordSerriesCalc().coords;
        public override List<Coord> GraphPoint => new List<Coord>{ a_Service.CalcCoordFromX(getX)};

    }
}
