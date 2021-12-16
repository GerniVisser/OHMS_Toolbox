using Caveability.Helper;
using Caveability.Services;
using System;
using System.Windows;

namespace Caveability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WallControle Footwall;
        private WallControle Hangwall;
        private WallControle StopeBack;
        private WallControle StrikeEnd;

        public MainWindow()
        {
            InitializeComponent();

            Footwall = new WallControle(true);
            Hangwall = new WallControle();
            StopeBack = new WallControle();
            StrikeEnd = new WallControle();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            tabFootwall.Content = Footwall;
            tabHangwall.Content = Hangwall;
            tabStopeBack.Content = StopeBack;
            tabStrikeEnds.Content = StrikeEnd;
        }

        private void MenuItemAdv_Click(object sender, RoutedEventArgs e)
        {
            StopeStreamObject stopeStream = new StopeStreamObject();

            stopeStream.footwallStreamObject = Footwall.ChartStreams();
            stopeStream.hangwallStreamObject = Hangwall.ChartStreams();
            stopeStream.stopebackStreamObject = StopeBack.ChartStreams();
            stopeStream.strikeendStreamObject = StrikeEnd.ChartStreams();

            //Report.GenerateReport(stopeStream, saveFileDialog.FileName);
            Report report = new Report(stopeStream);

            report.GenerateReport(Footwall._wall, Hangwall._wall, StopeBack._wall, StrikeEnd._wall);
        }
    }
}
