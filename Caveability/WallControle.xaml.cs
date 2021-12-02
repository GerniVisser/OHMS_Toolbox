using Caveability.Helper;
using Caveability.Models;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caveability
{
    /// <summary>
    /// Interaction logic for WallControle.xaml
    /// </summary>
    public partial class WallControle : UserControl
    {

        public Wall _wall;

        public WallControle(bool isFootwall = false)
        {
            if (isFootwall) _wall = new Wall(isFootwall);
            else _wall = new Wall();

            InitializeComponent();

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
            txtMaxLength.Text = _wall.HR.GetMaxLenght(n).ToString();
        }


        // This method may perhaps have to be made async
        public ChartStreamObject ChartStreams()
        {
            ChartStreamObject chartStream = new ChartStreamObject();

            int currentIndex = TabContr.SelectedIndex;

            try
            {
                TabContr.SelectedIndex = 0;
                chartStream.A_chartStream = ChartStream(A_Chart);
                TabContr.SelectedIndex = 1;
                chartStream.B_chartStream = ChartStream(B_Chart);
                TabContr.SelectedIndex = 2;
                chartStream.C_chartStream = ChartStream(C_Chart);
                TabContr.SelectedIndex = 3;
                chartStream.HR_chartStream = ChartStream(HR_Chart);
                TabContr.SelectedIndex = currentIndex;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return chartStream;
        }

        private Stream ChartStream(SfChart chart)
        {

            Stream outStream = new MemoryStream();

            chart.Width = 824;
            chart.Height = 474;
            chart.Background = new SolidColorBrush(Colors.White);
            chart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            chart.Arrange(new Rect(0, 0, chart.Width, chart.Height));

            chart.Save(outStream, new BmpBitmapEncoder());

            return outStream;
        }

        static IntPtr ApplicationMessageFilter(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            return IntPtr.Zero;
        }

    }
}
