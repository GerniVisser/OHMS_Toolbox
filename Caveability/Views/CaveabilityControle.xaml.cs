using _SharedWpfLibrary.Service;
using Caveability.Helper;
using Caveability.Models;
using Caveability.Services;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Caveability.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Caveability : UserControl
    {

        private WallControle Footwall;
        private WallControle Hangwall;
        private WallControle StopeBack;
        private WallControle StrikeEnd;

        public Caveability()
        {
            InitializeComponent();

            Footwall = new WallControle(BrushService.getBrushFromHex("#e3e3e3"), true);
            Hangwall = new WallControle(BrushService.getBrushFromHex("#d4d4d4"));
            StopeBack = new WallControle(BrushService.getBrushFromHex("#e3e3e3"));
            StrikeEnd = new WallControle(BrushService.getBrushFromHex("#d4d4d4"));

            tabFootwall.Content = Footwall;
            tabHangwall.Content = Hangwall;
            tabStopeBack.Content = StopeBack;
            tabStrikeEnds.Content = StrikeEnd;
        }


        private void ExportAsPDF_Click(object sender, RoutedEventArgs e)
        {
            ReportModel reportModel = new ReportModel();

            reportModel.footwall = Footwall._wall;
            reportModel.hangwall = Hangwall._wall;
            reportModel.stopeback = StopeBack._wall;
            reportModel.strikeend = StrikeEnd._wall;

            reportModel.footwallStream = Footwall.ChartStreams();
            reportModel.hangwallStream = Hangwall.ChartStreams();
            reportModel.stopebackStream = StopeBack.ChartStreams();
            reportModel.strikeendStream = StrikeEnd.ChartStreams();

            //Report.GenerateReport(stopeStream, saveFileDialog.FileName);
            Report report = new Report(reportModel);

            report.SaveReportPDF();
        }

        private void ExportAsImage_Click(object sender, RoutedEventArgs e)
        {
            ReportModel reportModel = new ReportModel();

            reportModel.footwall = Footwall._wall;
            reportModel.hangwall = Hangwall._wall;
            reportModel.stopeback = StopeBack._wall;
            reportModel.strikeend = StrikeEnd._wall;

            reportModel.footwallStream = Footwall.ChartStreams();
            reportModel.hangwallStream = Hangwall.ChartStreams();
            reportModel.stopebackStream = StopeBack.ChartStreams();
            reportModel.strikeendStream = StrikeEnd.ChartStreams();

            //Report.GenerateReport(stopeStream, saveFileDialog.FileName);
            Report report = new Report(reportModel);

            report.SaveReportImage();
        }
    }
}
