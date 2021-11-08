using Caveability.Models;
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
    /// Interaction logic for WallControle.xaml
    /// </summary>
    public partial class WallControle : UserControl
    {
        public WallControle()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();

            PropertyGrid.SelectedObject = new Wall();
        }
    }
}
