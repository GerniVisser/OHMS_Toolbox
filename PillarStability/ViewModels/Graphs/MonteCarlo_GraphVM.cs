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
    public class MonteCarlo_GraphVM: GraphBaseViewModel
    {
        private PillarModel _pillarModel;

        public MonteCarlo_GraphVM(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
        }

        public override void Update()
        {
            OnPropertyChanged(nameof(MCChartLine));
            OnPropertyChanged(nameof(MCChartLineCumulative));
            OnPropertyChanged(nameof(MCChartLineFos));
            OnPropertyChanged(nameof(MCChartFOSLabel));
        }

        public string MCChartFOSLabel
        {
            get { return "FOS " + _pillarModel.DesiredFOS.ToString(); }
        }

        public List<Coord> MCChartLine
        {
            get { return MonteCarloService.generateMonteCarloChartLine(_pillarModel); }
        }

        public List<Coord> MCChartLineCumulative
        {
            get { return MonteCarloService.generateMonteCarloCumulativeChartLine(_pillarModel); }
        }

        public List<Coord> MCChartLineFos
        {
            get { return MonteCarloService.generateFosChartLine(_pillarModel.DesiredFOS); }
        }

        public List<Coord> MCChartLineFos1
        {
            get { return MonteCarloService.generateFos1ChartLine(); }
        }

    }
}
