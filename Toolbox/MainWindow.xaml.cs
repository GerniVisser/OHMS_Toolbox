using System.Collections.Generic;
using System.Windows;
using Caveability;

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Views.ToolView _toolView;
        public MainWindow()
        {

            InitializeComponent();

            setupTools();
        }

        private void setupTools()
        {
            _toolView = new Views.ToolView();
            ContentView.Children.Add(_toolView);

            loadTools();
        }

        private void loadTools()
        {
            _toolView.AddTool("CaveabilityItem", new Caveability.Views.Caveability());
        }

        private void navigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {
            _toolView.ShowTool(e.Item.Name);
            
        }
    }
}
