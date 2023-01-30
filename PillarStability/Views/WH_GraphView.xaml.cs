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

namespace PillarStability.Views
{
    /// <summary>
    /// Interaction logic for WH_GraphView.xaml
    /// </summary>
    public partial class WH_GraphView : UserControl
    {
        public WH_GraphView()
        {
            InitializeComponent();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            SharedWpfLibrary.Service.ClipboardService.CopyToClipboard(WhChart, (int)(WhChart.ActualWidth), (int)(WhChart.ActualHeight));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SharedWpfLibrary.Service.ClipboardService.SaveChartToImage(WhChart, (int)(WhChart.ActualWidth), (int)(WhChart.ActualHeight));
        }
    }
}
