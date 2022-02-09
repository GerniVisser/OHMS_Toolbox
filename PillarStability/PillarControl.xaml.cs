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
            UpdateMonteGrid();
        }

        public void update()
        {
            if(_pillarModel != null)
            {
                updateOutGrid();
                UpdateOutChart();
            }
        }

        private void updateOutGrid()
        {
            whGridObject.Clear();
            var t = Calculations.calculate(_pillarModel);
            whGridObject.Add(t);

            dataGrid.ItemsSource = whGridObject;
        }

        private void UpdateMonteGrid()
        {
            mcGridObjects.Clear();
            var t = Calculations.calculateMC(_pillarModel, 100);
            mcGridObjects.Add(t);

            dataGridMonte.ItemsSource = mcGridObjects;
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
