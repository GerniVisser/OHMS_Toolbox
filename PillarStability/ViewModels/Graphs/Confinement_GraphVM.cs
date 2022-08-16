using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PillarStability.ViewModels.Graphs
{
    public class Confinement_GraphVM: ViewModelBase
    {
        private PillarModel _pillarModel;

        public Confinement_GraphVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        public string GraphHeader
        {
            get { return "Confinement Formula Stability Graph"; }
        }

        public string xAxisHeader
        {
            get { return "cPav"; }
        }

        public string yAxisHeader
        {
            get { return "APS/UCS"; }
        }

        public List<Coord> GraphLine
        {
            get { return new List<Coord>() { new Coord() { x = 0, y = 0 }, new Coord() { x = 1, y = 1 } }; }
        }

        public List<Coord> GraphPoint
        {
            get { return new List<Coord>() { new Coord() { x = 0, y = 0 }, new Coord() { x = 1, y = 1 } }; }
        }

        public Brush GraphPointColor
        {
            get { return _pillarModel.Color; }
        }
    }
}
