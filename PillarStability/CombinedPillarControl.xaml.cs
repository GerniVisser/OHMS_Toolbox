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
    /// Interaction logic for CombinedPillarControl.xaml
    /// </summary>
    public partial class CombinedPillarControl : UserControl
    {
        private ObservableCollection<OutputGridObject> _pillarOutputGrid;
        private CombinedPillarModel _model;

        public CombinedPillarControl(List<PillarModel> pillarModels)
        {
            InitializeComponent();

            _model = new CombinedPillarModel(pillarModels);
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
            }
        }

        private void UpdateOutChart()
        {
            wh_LineSerriesFail.ItemsSource = SerriesBuilder.whGraph(_model.PillarModels[0])[0].coords;
            wh_LineSerriesStable.ItemsSource = SerriesBuilder.whGraph(_model.PillarModels[0])[1].coords;

            wh_PointSerries.ItemsSource = SerriesBuilder.whPoint(_model.PillarModels).coords;
        }
    }
}
