using PillarStability.Helper;
using PillarStability.Models;
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
    /// Interaction logic for PillarControl.xaml
    /// </summary>
    public partial class PillarControl : UserControl
    {
        private PillarModel _pillar;
        private ObservableCollection<OutputGridObject> _pillarOutputGrid;

        public PillarControl()
        {
            InitializeComponent();

            _pillar = new PillarModel();
            _pillarOutputGrid = new ObservableCollection<OutputGridObject>();
            
            PropertyGrid.SelectedObject = _pillar;

            update();
        }

        private void ButtonAdv_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        public void update()
        {
            updateOutGrid();
        }

        private void updateOutGrid()
        {
            PillarOut.Clear();
            var t = Calculations.calculate(_pillar);
            PillarOut.Add(t);

            dataGrid.ItemsSource = PillarOut;
        }

        public ObservableCollection<OutputGridObject> PillarOut
        {
            get { return _pillarOutputGrid; }
            set { _pillarOutputGrid = value; }
        }

    }
}
