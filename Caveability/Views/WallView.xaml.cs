using Caveability.ViewModels;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caveability.Views
{
    /// <summary>
    /// Interaction logic for WallView.xaml
    /// </summary>
    public partial class WallView : UserControl
    {
        public WallView()
        {
            InitializeComponent();
        }

        private void GraphTabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (WallViewModel)this.DataContext;

            if (vm == null) return;

            if (GraphTabList.SelectedIndex == 3)
            {
                chart.SecondaryAxis = new LogarithmicAxis()
                {
                    LogarithmicBase = 10,
                    Minimum = 0.1,
                    MaxHeight = 1000,
                    Header = vm.GraphViewModel.yAxisHeader,
                };
            }
            else
            {
                chart.SecondaryAxis = new NumericalAxis()
                {
                    StartRangeFromZero = true,
                    Header = vm.GraphViewModel.yAxisHeader,
                };
            }
        }

        private void MainGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var sb = (Storyboard)FindResource("BackgroundFade");
            sb.Begin();
        }
    }
}
