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

        public PillarControl()
        {
            InitializeComponent();

            _whGridObject = new ObservableCollection<OutputGridObject>();

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
        }

        public void update()
        {
            if(_pillarModel != null)
            {
                updateOutGrid();
                UpdateMonteGrid();
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
            whGridObject.Clear();
            var t = Calculations.calculate(_pillarModel);
            whGridObject.Add(t);

            dataGridMonte.ItemsSource = whGridObject;
        }

        private void UpdateOutChart()
        {
            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_pillarModel)[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_pillarModel)[1].coords;

            var pointList = new List<PillarModel>();
            pointList.Add(_pillarModel);
            wh_PointSerries.ItemsSource = SerriesBuilder.whPoint(pointList).coords;
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

    }
}
