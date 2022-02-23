using PillarStability.Helper;
using PillarStability.Models;
using SharedWpfLibrary.Tools;
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
        private PillarModel _pillarModel = new PillarModel("0");
        private ObservableCollection<OutputGridObject> _whGridObject;
        private ObservableCollection<MCGridObject> _mcGridObjects;
        private Coord _whGraphPoint;
        private Coord _outGraphPoint;

        public PillarControl()
        {
            InitializeComponent();

            _whGridObject = new ObservableCollection<OutputGridObject>();
            _mcGridObjects = new ObservableCollection<MCGridObject>();

            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_pillarModel)[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_pillarModel)[1].coords;

            ave_LineSerriesFail.ItemsSource = SerriesBuilder.apcGraph(_pillarModel)[0].coords;
            ave_LineSerriesStable.ItemsSource = SerriesBuilder.apcGraph(_pillarModel)[1].coords;

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
            UpdateMC();
            update();
        }

        // refresh will be used to only refresh the user controles with precalculated values so calcs doesn't have to occure every time  
        public void refresh()
        {
            // refreshes the values in the datagrid 
            dataGrid.ItemsSource = whGridObject;

            // refreshes the values of the wh graph
            CoordSerries serries = new CoordSerries();
            serries.coords.Add(_whGraphPoint);
            wh_PointSerries.ItemsSource = serries.coords;

            // refreshes the values ot the outGraph
            serries = new CoordSerries();
            serries.coords.Add(_outGraphPoint);
            ave_PointSerries.ItemsSource = serries.coords;

            // refreshes the values in the MC datagrid
            if (_pillarModel.MCGridObject != null)
            {
                mcGridObjects.Clear();
                mcGridObjects.Add(_pillarModel.MCGridObject);

                dataGridMonte.ItemsSource = mcGridObjects;
            }
            else
            {
                mcGridObjects.Clear();
            }

            // refreshes the values on MC Graph 
            RefreshMCChart();
        }

        // Updated will run all calcs and should be used sparingly as it uses alot of resources
        public void update()
        {
            if(_pillarModel != null)
            {
                UpdateOutGrid();
                UpdateOutChart();

                refresh();
            }
        }

        public void UpdateMC()
        {
            // Runs the Monte Carlo sim and save the values in the pillar model
            var t = Calculations.calculateMC(_pillarModel, 10000);
            _pillarModel.MCGridObject = t.Item1;
            _pillarModel.Bins = t.Item2;
        }

        private void UpdateOutGrid()
        {
            whGridObject.Clear();
            var t = Calculations.calculate(_pillarModel);
            whGridObject.Add(t);
        }

        private void UpdateOutChart()
        {
            _whGraphPoint = SerriesBuilder.whPoint(_pillarModel);

            _outGraphPoint = SerriesBuilder.apcPoint(_pillarModel);
        }

        // No MC sim is ran here just refreshing the chart values strored in the Pillar model
        private void RefreshMCChart()
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
                // Clears the graphs if no data is available otherwise the previous pillar's data is displayed.
                mc_LineSerries.ItemsSource = null;
                mc_LineSerriesCumalitive.ItemsSource = null;
                FOS14_LineSerries.ItemsSource = null;
                FOS1_LineSerries.ItemsSource = null;
            }
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
