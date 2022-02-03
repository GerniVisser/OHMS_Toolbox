using PillarStability.Helper;
using PillarStability.Models;
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
        private ObservableCollection<OutputGridObject> _pillarOutputGrid;

        public PillarControl()
        {
            InitializeComponent();

            _pillarOutputGrid = new ObservableCollection<OutputGridObject>();


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
            PillarOut.Clear();
            var t = Calculations.calculate(_pillarModel);
            PillarOut.Add(t);

            dataGrid.ItemsSource = PillarOut;
        }

        private void UpdateMonteGrid()
        {
            PillarOut.Clear();
            var t = Calculations.calculate(_pillarModel);
            PillarOut.Add(t);

            dataGridMonte.ItemsSource = PillarOut;
        }

        private void UpdateOutChart()
        {
            var x = SerriesBuilder.whGraph(_pillarModel.UCS)[0].coords;
            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_pillarModel.UCS)[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_pillarModel.UCS)[1].coords;
        }

        public ObservableCollection<OutputGridObject> PillarOut
        {
            get { return _pillarOutputGrid; }
            set { _pillarOutputGrid = value; }
        }

        public PillarModel getPillarModel
        {
            get { return _pillarModel; }
        }

    }
}
