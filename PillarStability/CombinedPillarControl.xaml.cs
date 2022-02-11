using PillarStability.Helper;
using PillarStability.Models;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PillarStability
{
    /// <summary>
    /// Interaction logic for CombinedPillarControl.xaml
    /// </summary>
    public partial class CombinedPillarControl : UserControl
    {
        private CombinedPillarModel _model;
        private ObservableCollection<OutputGridObject> _pillarOutputGrid;

        public CombinedPillarControl(List<PillarModel> pillarModels)
        {
            InitializeComponent();

            _model = new CombinedPillarModel(pillarModels);
            _pillarOutputGrid = new ObservableCollection<OutputGridObject>();
        }

        public ObservableCollection<OutputGridObject> PillarOut
        {
            get { return _pillarOutputGrid; }
            set { _pillarOutputGrid = value; }
        }

        public void update()
        {
            if(_model.PillarModels.Count > 0)
            { 
                UpdateOutChart();
                updateOutGrid();
            }
        }

        private void updateOutGrid()
        {
            whGrid.Clear();
            for (int i = 0; i < _model.PillarModels.Count; i++)
            {
                var t = Calculations.calculate(_model.PillarModels[i]);
                whGrid.Add(t);
            }

            dataGrid.ItemsSource = whGrid;
        }

        private void UpdateOutChart()
        {
            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_model.PillarModels[0])[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_model.PillarModels[0])[1].coords;

            ave_LineSerriesFail.ItemsSource = SerriesBuilder.apcGraph(_model.PillarModels[0])[0].coords;
            ave_LineSerriesStable.ItemsSource = SerriesBuilder.apcGraph(_model.PillarModels[0])[1].coords;

            while(wh_Chart.Series.Count > 2)
            {
                wh_Chart.Series.RemoveAt(2);
                ave_Chart.Series.RemoveAt(2);
            }

            var random = new Random();

            for (int i = 0; i <= _model.PillarModels.Count - 1; i++)
            {
                string randColor = String.Format("#{0:X6}", random.Next(0x1000000));

                Coord coord = SerriesBuilder.whPoint(_model.PillarModels[i]);
                var t = addScarrterSerries(coord, _model.PillarModels[i].Name, randColor);
                wh_Chart.Series.Add(t);

                coord = SerriesBuilder.apcPoint(_model.PillarModels[i]);
                t = addScarrterSerries(coord, _model.PillarModels[i].Name, randColor);
                ave_Chart.Series.Add(t);
            }
        }

        private ScatterSeries addScarrterSerries(Coord coord, string label, string hexColor)
        {
            List<Coord> coords = new List<Coord>();
            coords.Add(coord);

            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(hexColor);

            ScatterSeries series = new ScatterSeries()
            {
                ItemsSource = coords,
                XBindingPath = "x",
                YBindingPath = "y",
                ScatterHeight = 12,
                ScatterWidth = 12,
                Interior = brush,
                Label = label,
                LegendIcon = ChartLegendIcon.Circle
            };

            return series;
        }

        public ObservableCollection<OutputGridObject> whGrid
        {
            get { return _pillarOutputGrid; }
            set { _pillarOutputGrid = value; }
        }
    }
}
