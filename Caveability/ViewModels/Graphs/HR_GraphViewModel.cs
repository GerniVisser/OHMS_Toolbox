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
    class HR_GraphViewModel : GraphViewModelBase
    {
        private HR_Service hr_Service;

        public HR_GraphViewModel(WallModel wallModel)
        {
            hr_Service = new HR_Service(wallModel);
        }

        public override string GraphName => "Stability for Gravity fall";

        public override float getX => hr_Service.getX();

        public override List<Coord> HRGraphLineTop => hr_Service.CoordSerriesCalc().coords;

        public override List<Coord> HRGraphLineBot => hr_Service.CoordSerriesCalcBot().coords;

        public override List<Coord> GraphPoint => new List<Coord> { new Coord() { x = hr_Service.CalculateXAxis(), y = hr_Service.Calc_N() } };

        public override List<Coord> HRGraphPointMax => new List<Coord> { new Coord() { x = hr_Service.getX(), y = hr_Service.Calc_N() } };

        
    }
}
