using Caveability.Models;
using Caveability.ViewModels;
using System;
using System.Collections.Generic;
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

namespace Caveability.Views
{
    /// <summary>
    /// Interaction logic for CaveabilityView.xaml
    /// </summary>
    public partial class CaveabilityView : UserControl
    {
        public CaveabilityView()
        {
            List<WallModel> wallModels = new List<WallModel>()
            {
                new WallModel("Hangwall"),
                new WallModel("Footwall", true),
                new WallModel("Stope Back"),
                new WallModel("Strike End")
            };
            this.DataContext = new CaveabilityViewModel(wallModels);

            InitializeComponent();
        }
    }
}
