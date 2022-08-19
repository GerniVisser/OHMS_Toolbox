using _SharedWpfLibrary.ViewModels;
using PillarStability.Models;
using PillarStability.Services;
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
        private Wh_Service _wh_Service;

        public WH_GraphVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _wh_Service = new Wh_Service(pillarModel);
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

        public List<Coord> GraphLineFail
        {
            get { return _wh_Service.graphFail(); }
        }

        public List<Coord> GraphLineStable
        {
            get { return _wh_Service.graphStable(); }
        }

        public List<Coord> GraphPoint
        {
            get { return _wh_Service.graphPoint(); }
        }

        public Brush GraphPointColor
        {
            get { return _pillarModel.Color; }
        }

    }
}
