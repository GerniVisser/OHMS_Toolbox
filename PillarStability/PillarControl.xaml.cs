using PillarStability.Helper;
using PillarStability.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PillarStability
{
    /// <summary>
    /// Interaction logic for PillarControl.xaml
    /// </summary>
    public partial class PillarControl : UserControl
    {
        private PillarModel _pillarModel;
        private ObservableCollection<OutputGridObject> _whGridObject;
        private ObservableCollection<MCGridObject> _mcGridObjects;

        public PillarControl()
        {
            InitializeComponent();

            _whGridObject = new ObservableCollection<OutputGridObject>();
            _mcGridObjects = new ObservableCollection<MCGridObject>();

            update();
        }

        public bool setPillarModel(PillarModel pillarModel)
        {
            try
            {
                _pillarModel = pillarModel;
                PropertyGrid.SelectedObject = _pillarModel;
                update();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void ButtonAdv_Click(object sender, RoutedEventArgs e)
        {
            update();
            updateMC();
        }

        public void update()
        {
            if(_pillarModel != null)
            {
                updateOutGrid();
                UpdateOutChart();
                UpdateMCGrid();
                UpdateMCChart();
            }
        }

        public void updateMC()
        {
            var t = Calculations.calculateMC(_pillarModel, 10000);
            _pillarModel.MCGridObject = t.Item1;
            _pillarModel.Bins = t.Item2;
            UpdateMCGrid();
            UpdateMCChart();
        }

        private void updateOutGrid()
        {
            whGridObject.Clear();
            var t = Calculations.calculate(_pillarModel);
            whGridObject.Add(t);

            dataGrid.ItemsSource = whGridObject;
        }

        private void UpdateMCGrid()
        {
            if(_pillarModel.MCGridObject != null)
            {
                mcGridObjects.Clear();
                mcGridObjects.Add(_pillarModel.MCGridObject);

                dataGridMonte.ItemsSource = mcGridObjects;
            }
            else
            {
                mcGridObjects.Clear();
            }
        }

        private void UpdateMCChart()
        {
            if (_pillarModel.MCGridObject != null)
            {
                mc_LineSerries.ItemsSource = SerriesBuilder.mcLineSerries(_pillarModel.Bins).coords;
                mc_LineSerriesCumalitive.ItemsSource = SerriesBuilder.mcCumalitiveLineSerries(_pillarModel.Bins).coords;

                CoordSerries fos = new CoordSerries();
                fos.coords.Add(new Coord() { x = 1, y = 0 });
                fos.coords.Add(new Coord() { x = 1, y = 1 });

                FOS1_LineSerries.ItemsSource = fos.coords;

                fos.coords.Clear();

                fos.coords.Add(new Coord() { x = 1.4, y = 0 });
                fos.coords.Add(new Coord() { x = 1.4, y = 1 });

                FOS14_LineSerries.ItemsSource = fos.coords;
            }
            else
            {
                // Glears the graphs if no data is available otherwise the previous pillar's data is displayed.
                mc_LineSerries.ItemsSource = null;
                mc_LineSerriesCumalitive.ItemsSource = null;
                FOS14_LineSerries.ItemsSource = null;
                FOS1_LineSerries.ItemsSource = null;
            }
        }

        private void UpdateOutChart()
        {
            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_pillarModel)[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_pillarModel)[1].coords;

            var wh_pointList = new List<PillarModel>();
            wh_pointList.Add(_pillarModel);
            wh_PointSerries.ItemsSource = SerriesBuilder.whPoint(wh_pointList).coords;

            ave_LineSerriesFail.ItemsSource = SerriesBuilder.apcGraph(_pillarModel)[0].coords;
            ave_LineSerriesStable.ItemsSource = SerriesBuilder.apcGraph(_pillarModel)[1].coords;

            var ave_pointList = new List<PillarModel>();
            ave_pointList.Add(_pillarModel);
            ave_PointSerries.ItemsSource = SerriesBuilder.apcPoint(ave_pointList).coords;
        }

        public PillarModel getPillarModel
        {
            get { return _pillarModel; }
        }

        public ObservableCollection<OutputGridObject> whGridObject
        {
            get { return _whGridObject; }
            set { _whGridObject = value; }
        }

        public ObservableCollection<MCGridObject> mcGridObjects
        {
            get { return _mcGridObjects; }
            set { _mcGridObjects = value; }
        }
    }
}
