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
    public class Confinement_GraphVM: ViewModelBase
    {
        private PillarModel _pillarModel;
        private Confinement_Service _confinement_Service;

        public Confinement_GraphVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _confinement_Service = new Confinement_Service(pillarModel);
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

        public List<Coord> GraphLineFail
        {
            get { return _confinement_Service.graphFail(); }
        }

        public List<Coord> GraphLineStable
        {
            get { return _confinement_Service.graphStable(); }
        }

        public List<Coord> GraphPoint
        {
            get { return _confinement_Service.graphPoint(); }
        }

        public Brush GraphPointColor
        {
            get { return _pillarModel.Color; }
        }
    }
}
