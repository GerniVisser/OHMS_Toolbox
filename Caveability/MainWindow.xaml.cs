using Caveability.Helper;
using Caveability.Services;
using Microsoft.Win32;
using Syncfusion.Windows.Shared;
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

namespace Caveability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WallControle Footwall;
        private WallControle Hangwall;
        private WallControle StopEBack;
        private WallControle StrikeEnd;

        public MainWindow()
        {
            InitializeComponent();

            Footwall = new WallControle(true);
            Hangwall = new WallControle();
            StopEBack = new WallControle();
            StrikeEnd = new WallControle();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            tabFootwall.Content = Footwall;
            tabHangwall.Content = Hangwall;
            tabStopeBack.Content = StopEBack;
            tabStrikeEnds.Content = StrikeEnd;
        }

        private void MenuItemAdv_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF document (*.pdf)|*.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                ChartStreamObject Footwall_chartStream = Footwall.ChartStreams();

                Report.GenerateReport(Footwall_chartStream, saveFileDialog.FileName);
            }
        }
    }
}
