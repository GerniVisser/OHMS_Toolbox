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

namespace Toolbox.Views
{
    /// <summary>
    /// Interaction logic for ToolboxView.xaml
    /// </summary>
    public partial class ToolboxView : UserControl
    {
        private ToolView _toolView;
        public ToolboxView()
        {
            InitializeComponent();

            setupTools();
        }

        private void setupTools()
        {
            _toolView = new ToolView();
            ContentView.Children.Add(_toolView);

            loadTools();
        }

        private void loadTools()
        {
            //_toolView.AddTool("CaveabilityItem", new Caveability.Views.Caveability());
            _toolView.AddTool("PillarStabilityItem", new PillarStability.Views.PillarStabiltyControl());
        }

        private void navigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {
            _toolView.ShowTool(e.Item.Name);

        }
    }
}
