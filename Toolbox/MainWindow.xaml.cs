
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
using Toolbox.Services;

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToolControl CavabilityTool;

        public MainWindow()
        {

            CavabilityTool = new ToolControl();

            CavabilityTool.ToolObject.Name = "Caveability Calculator";
            CavabilityTool.ToolObject.Icon = "/Icons/calculator_white.png";
            CavabilityTool.ToolObject.Description = "Calculator tool to measure and assess Stress in a Stope";

            InitializeComponent();

            ContentView.Children.Add(CavabilityTool);

            //LicensingService.ValidateLicense();
        }

        private void navigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {
            if(ContentView.Children.Count != 0)
            {
                ContentView.Children.RemoveAt(0);
            }

            if (e.Item.Name == "Caveability")
            {
                ContentView.Children.Add(CavabilityTool);
            }

        }
    }

}
