using PillarStability.Helper;
using PillarStability.Models;
using PillarStability.Services;
using SharedWpfLibrary.Service;
using SharedWpfLibrary.Tools;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public void update()
        {
            UpdateOutChart();
            updateOutGrid();
        }

        private void updateOutGrid()
        {
            whGrid.Clear();
            if (_model.PillarModels.Count > 0)
            {
                for (int i = 0; i < _model.PillarModels.Count; i++)
                {
                    var t = Calculations.calculate(_model.PillarModels[i]);
                    whGrid.Add(t);
                }

                dataGrid.ItemsSource = whGrid;
            }
        }

        private void UpdateOutChart()
        {
            if (_model.PillarModels.Count > 0)
            {
                wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_model.PillarModels[0])[0].coords;
                wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_model.PillarModels[0])[1].coords;

                ave_LineSerriesFail.ItemsSource = SerriesBuilder.apcGraph(_model.PillarModels[0])[0].coords;
                ave_LineSerriesStable.ItemsSource = SerriesBuilder.apcGraph(_model.PillarModels[0])[1].coords;

                while (wh_Chart.Series.Count > 2)
                {
                    wh_Chart.Series.RemoveAt(2);
                    ave_Chart.Series.RemoveAt(2);
                }

                for (int i = 0; i <= _model.PillarModels.Count - 1; i++)
                {
                
                    Coord coord = SerriesBuilder.whPoint(_model.PillarModels[i]);
                    var t = addScarrterSerries(coord, _model.PillarModels[i].Name, _model.PillarModels[i].Color);
                    wh_Chart.Series.Add(t);

                    coord = SerriesBuilder.apcPoint(_model.PillarModels[i]);
                    t = addScarrterSerries(coord, _model.PillarModels[i].Name, _model.PillarModels[i].Color);
                    ave_Chart.Series.Add(t);
                }
            }
            else
            {
                while (wh_Chart.Series.Count > 2)
                {
                    wh_Chart.Series.RemoveAt(2);
                    ave_Chart.Series.RemoveAt(2);
                }
            }
            
        }

        private ScatterSeries addScarrterSerries(Coord coord, string label, Brush brushColor)
        {
            List<Coord> coords = new List<Coord>();
            coords.Add(coord);

            var converter = new System.Windows.Media.BrushConverter();

            ScatterSeries series = new ScatterSeries()
            {
                ItemsSource = coords,
                XBindingPath = "x",
                YBindingPath = "y",
                ScatterHeight = 12,
                ScatterWidth = 12,
                Interior = brushColor,
                Label = label,
                LegendIcon = ChartLegendIcon.Circle
            };

            return series;
        }

        public ReportModel getChartStreams()
        {
            var wh = ReportHelper.ChartStream(wh_Chart);
            var ave = ReportHelper.ChartStream(ave_Chart);

            updateOutGrid();
            List<OutputGridObject> outputGridObjects = new List<OutputGridObject>(whGrid);

            List<PillarPrams> pillarPrams = new List<PillarPrams>();

            for (int i = 0; i <= _model.PillarModels.Count - 1; i++)
            {
                pillarPrams.Add(new PillarPrams(_model.PillarModels[i]));

            }

            ReportModel model = new ReportModel()
            {
                whStream = wh,
                aveStream = ave,
                outGridObjects = outputGridObjects,
                pillarPrams = pillarPrams
            };

            return model;
        }

        private void ExportWhImage_Click(object sender, RoutedEventArgs e)
        {
            var data = getChartStreams();
            data.aveStream = null;

            Report rep = new Report(data);
            rep.SaveReportImage();
        }

        private void ExportAveImage_Click(object sender, RoutedEventArgs e)
        {
            var data = getChartStreams();
            data.whStream = null;

            Report rep = new Report(data);
            rep.SaveReportImage();
        }

        private void AddWHToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            ClipboardService.CopyToClipboard(wh_Chart, (int)(wh_Chart.ActualWidth), (int)(wh_Chart.ActualHeight));
        }

        private void AddAveToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            ClipboardService.CopyToClipboard(ave_Chart, (int)(ave_Chart.ActualWidth), (int)(ave_Chart.ActualHeight));
        }

        public ObservableCollection<OutputGridObject> whGrid
        {
            get { return _pillarOutputGrid; }
            set { _pillarOutputGrid = value; }
        }
    }
}
