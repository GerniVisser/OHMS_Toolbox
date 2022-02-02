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
        private PillarModel _pillar;
        private ObservableCollection<OutputGridObject> _pillarOutputGrid;

        public PillarControl(PillarModel pillarModel)
        {
            InitializeComponent();

            _pillarOutputGrid = new ObservableCollection<OutputGridObject>();

            setPillarModel(pillarModel);

            update();
        }

        public bool setPillarModel(PillarModel pillarModel)
        {
            try
            {
                _pillar = pillarModel;
                PropertyGrid.SelectedObject = _pillar;
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
            updateOutGrid();
            UpdateMonteGrid();
            UpdateOutChart();
        }

        private void updateOutGrid()
        {
            PillarOut.Clear();
            var t = Calculations.calculate(_pillar);
            PillarOut.Add(t);

            dataGrid.ItemsSource = PillarOut;
        }

        private void UpdateMonteGrid()
        {
            PillarOut.Clear();
            var t = Calculations.calculate(_pillar);
            PillarOut.Add(t);

            dataGridMonte.ItemsSource = PillarOut;
        }

        private void UpdateOutChart()
        {
            var x = SerriesBuilder.whGraph(_pillar.UCS)[0].coords;
            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_pillar.UCS)[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_pillar.UCS)[1].coords;
        }

        public ObservableCollection<OutputGridObject> PillarOut
        {
            get { return _pillarOutputGrid; }
            set { _pillarOutputGrid = value; }
        }

        public PillarModel getPillarModel
        {
            get { return _pillar; }
        }

    }
}
