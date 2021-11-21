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

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToolControl CavabilityTool;
        private ToolControl RockTool;

        public MainWindow()
        {
            CavabilityTool = new ToolControl();
            RockTool = new ToolControl();
            RockTool.ToolObject.Name = "Rock";
            RockTool.ToolObject.Icon = "/Icons/calculator.png";
            RockTool.ToolObject.Description = "Jho mah bro";

            InitializeComponent();

            ContentView.Children.Add(CavabilityTool);
        }

        private void navigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {
            ContentView.Children.RemoveAt(0);
            ContentView.Children.Add(RockTool);
        }
    }

}
