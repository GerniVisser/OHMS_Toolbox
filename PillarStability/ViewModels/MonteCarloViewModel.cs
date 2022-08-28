using _SharedWpfLibrary.ViewModels;
using PillarStability.Commands;
using PillarStability.DataObjects;
using PillarStability.Models;
using PillarStability.Services;
using SharedWpfLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillarStability.ViewModels
{
    public class MonteCarloViewModel : ViewModelBase
    {
        private PillarModel _pillarModel;
        private MonteCarloModel _monteCarloModel;
        private MonteCarloService _monteCarloService;

        public MonteCarloViewModel(PillarModel pillarModel)
        {
            _pillarModel = pillarModel;
            _monteCarloModel = _pillarModel.MonteCarloModel;
            _monteCarloService = new MonteCarloService(new LunderPakalnisService(), pillarModel, _monteCarloModel);
            _calculateMonteCarlo = new DelegateCommand(Calculate);
        }

        private void Calculate(object obj)
        {
            _monteCarloService.CalculateMonteCarlo();

            MCDataGrid = _monteCarloService.generateMonteCarloDataObject();
            MCChartLine = _monteCarloService.generateMonteCarloChartLine();
            MCChartLineCumulative = _monteCarloService.generateMonteCarloCumulativeChartLine();
        }

        private ICommand _calculateMonteCarlo;

        public ICommand CalculateMonteCarlo
        {
            get { return _calculateMonteCarlo; }
        }

        private List<Coord> _mcChartLine;

        public List<Coord> MCChartLine
        {
            get { return _mcChartLine; }
            set
            {
                _mcChartLine = value;
                OnPropertyChanged(nameof(MCChartLine));
            }
        }

        private List<Coord> _mcChartLineCumulative;

        public List<Coord> MCChartLineCumulative
        {
            get { return _mcChartLineCumulative; }
            set 
            { 
                _mcChartLineCumulative = value;
                OnPropertyChanged(nameof(MCChartLineCumulative));
            }
        }


        public List<Coord> MCChartLineFos1
        {
            get { return _monteCarloService.generateFos1ChartLine(); }
        }

        public List<Coord> MCChartLineFos14
        {
            get { return _monteCarloService.generateFos14ChartLine(); }
        }


        private MonteCarloDataObject _mcDataGrid;

        public MonteCarloDataObject MCDataGrid
        {
            get { return _mcDataGrid; }
            set 
            { 
                _mcDataGrid = value;
                OnPropertyChanged(nameof(MCDataGrid));
            }
        }

    }
}
