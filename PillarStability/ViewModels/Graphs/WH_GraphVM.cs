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
    public class WH_GraphVM: ViewModelBase
    {
        private PillarModel _pillarModel;

        public WH_GraphVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        public string GraphHeader
        {
            get { return "Width / Height Stability Graph"; }
        }

        public string xAxisHeader
        {
            get { return "Width/Height"; }
        }

        public string yAxisHeader
        {
            get { return "APS/UCS"; }
        }

        public List<Coord> GraphLine
        {
            get { return null; }
        }

        public List<Coord> GraphPoint
        {
            get { return null; }
        }

        public Brush GraphPointColor
        {
            get { return _pillarModel.Color; }
        }

    }
}
