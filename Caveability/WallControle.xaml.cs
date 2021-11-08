using Caveability.Helper;
using Caveability.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Wall _wall;

        public WallControle()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();

            _wall = new Wall();

            PropertyGrid.SelectedObject = _wall;

            A_LineSerries.ItemsSource = _wall.A.GetGraphCoords();
            B_LineSerries.ItemsSource = _wall.B.GetGraphCoords();
            C_LineSerries.ItemsSource = _wall.C.GetGraphCoords();
            HRTopAreSerries.ItemsSource = _wall.HR.GetTopGraphCoords();
            HRBotAreSerries.ItemsSource = _wall.HR.GetBottomGraphCoords();

            update(Catagory.All);
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

    }
}
