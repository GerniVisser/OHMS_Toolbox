using System.Windows;

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToolControl CavabilityTool;
        private ToolControl PillarStressTool;
        public MainWindow()
        {

            InitializeComponent();

            setupTools();
            loadTools(CavabilityTool);
            loadTools(PillarStressTool);
            //LicensingService.ValidateLicense();
        }

        private void setupTools()
        {
            CavabilityTool = new ToolControl(@"Caveability\bin\Debug\net5.0-windows\win-x86\Caveability.exe");

            CavabilityTool.ToolObject.Name = "Caveability Calculator";
            CavabilityTool.ToolObject.Icon = "/Icons/calculator_white.png";
            CavabilityTool.ToolObject.Description = "Calculator tool to measure and assess Stress in a Stope";

            PillarStressTool = new ToolControl(@"PillarStability\bin\Debug\net5.0-windows\PillarStability.exe");

            PillarStressTool.ToolObject.Name = "Pillar Stress Calculator";
            PillarStressTool.ToolObject.Icon = "/Icons/calculator_white.png";
            PillarStressTool.ToolObject.Description = "Calculator tool to measure and assess Stress in a Pillar";
        }

        private void loadTools(ToolControl tool)
        {
            ContentView.Children.Clear();
            ContentView.Children.Add(tool);
        }

        private void navigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {
            if (ContentView.Children.Count != 0)
            {
                ContentView.Children.RemoveAt(0);
            }

            if (e.Item.Name == "Caveability")
            {
                ContentView.Children.Add(CavabilityTool);
            }
            if (e.Item.Name == "PillarStability")
            {
                ContentView.Children.Add(PillarStressTool);
            }

        }
    }
}
