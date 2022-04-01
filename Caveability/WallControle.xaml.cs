using _SharedWpfLibrary.Service;
using Caveability.Helper;
using Caveability.Models;
using Caveability.Services;
using SharedWpfLibrary.Service;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Caveability
{
    /// <summary>
    /// Interaction logic for WallControle.xaml
    /// </summary>
    public partial class WallControle : UserControl
    {

        public Wall _wall;

        public WallControle(Brush backgroundColor, bool isFootwall = false)
        {
            if (isFootwall) _wall = new Wall(isFootwall);
            else _wall = new Wall();

            InitializeComponent();

            Viewport.Background = backgroundColor;
            PropertyGrid.SelectedObject = _wall;

            A_LineSerries.ItemsSource = _wall.A.GetGraphCoords();
            B_LineSerries.ItemsSource = _wall.B.GetGraphCoords();
            C_LineSerries.ItemsSource = _wall.C.GetGraphCoords();
            HRTopAreSerries.ItemsSource = _wall.HR.GetTopGraphCoords();
            HRBotAreSerries.ItemsSource = _wall.HR.GetBottomGraphCoords();

            update(Catagory.All);

            ChartStreams();
        }

        private void PropertyGrid_ValueChanged(object sender, Syncfusion.Windows.PropertyGrid.ValueChangedEventArgs args)
        {
            if (args.Property.Category == "Calculate A") update(Catagory.A);
            else if (args.Property.Category == "Calculate B") update(Catagory.B);
            else if (args.Property.Category == "Calculate C") update(Catagory.C);
            else if (args.Property.Category == "Calculate Q") update(Catagory.HR);
            else if (args.Property.Category == "HR") update(Catagory.HR);
            else update(Catagory.All);
        }

        private void update(Catagory catagory)
        {
            if (catagory == Catagory.A || catagory == Catagory.All)
            {
                A_PointSerries.ItemsSource = new ObservableCollection<Coord> { new Coord { x = _wall.A.CalculateXAxis(), y = _wall.A.Calculate() } };
            }
            if (catagory == Catagory.B || catagory == Catagory.All)
            {
                B_PointSerries.ItemsSource = new ObservableCollection<Coord> { new Coord { x = _wall.B.CalculateXAxis(), y = _wall.B.Calculate() } };
            }
            if (catagory == Catagory.C || catagory == Catagory.All)
            {
                C_PointSerries.ItemsSource = new ObservableCollection<Coord> { new Coord { x = _wall.C.CalculateXAxis(), y = _wall.C.Calculate() } };
            }

            var n = N_Model.Calculate(_wall.Q.Calculate(), _wall.A.Calculate(), _wall.B.Calculate(), _wall.C.Calculate());

            HRmax_PointSerries.ItemsSource = new ObservableCollection<Coord> { new Coord { x = _wall.HR.Calculate(), y = n } };
            HR_PointSerries.ItemsSource = new ObservableCollection<Coord> { new Coord { x = _wall.HR.CalculateXAxis(n), y = n } };

            txtCurrentHR.Text = Math.Round(_wall.HR.Calculate(), 2).ToString();
            txtMaxHR.Text = Math.Round(_wall.HR.CalculateXAxis(n), 2).ToString();
            txtMaxLength.Text = _wall.HR.GetMaxLenght((float)(n)).ToString();
        }


        // This method may perhaps have to be made async
        public ChartStreamModel ChartStreams()
        {
            ChartStreamModel chartStream = new ChartStreamModel();

            int currentIndex = TabContr.SelectedIndex;

            try
            {
                TabContr.SelectedIndex = 0;
                chartStream.A_chartStream = ReportStreamService.ChartStream(A_Chart);
                TabContr.SelectedIndex = 1;
                chartStream.B_chartStream = ReportStreamService.ChartStream(B_Chart);
                TabContr.SelectedIndex = 2;
                chartStream.C_chartStream = ReportStreamService.ChartStream(C_Chart);
                TabContr.SelectedIndex = 3;
                chartStream.HR_chartStream = ReportStreamService.ChartStream(HR_Chart);
                TabContr.SelectedIndex = currentIndex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return chartStream;
        }

        private void ExportImage_Click(object sender, RoutedEventArgs e)
        {
            ReportModel reportModel = new ReportModel();

            reportModel.footwall = _wall;

            reportModel.footwallStream = ChartStreams();

            //Report.GenerateReport(stopeStream, saveFileDialog.FileName);
            Report report = new Report(reportModel);

            report.SaveReportImage();
        }

        private void AddAToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            ClipboardService.CopyToClipboard(A_Chart, (int)(A_Chart.ActualWidth), (int)(A_Chart.ActualHeight));
        }

        private void AddBToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            ClipboardService.CopyToClipboard(B_Chart, (int)(B_Chart.ActualWidth), (int)(B_Chart.ActualHeight));
        }
        private void AddCToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            ClipboardService.CopyToClipboard(C_Chart, (int)(C_Chart.ActualWidth), (int)(C_Chart.ActualHeight));
        }
        private void AddHRToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            ClipboardService.CopyToClipboard(HR_Chart, (int)(HR_Chart.ActualWidth), (int)(HR_Chart.ActualHeight));
        }

    }
}
