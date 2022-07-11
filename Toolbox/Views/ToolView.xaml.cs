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
    /// Interaction logic for ToolView.xaml
    /// </summary>
    public partial class ToolView : UserControl
    {
        private Dictionary<string, UserControl> _tools;
        public ToolView()
        {
            _tools = new Dictionary<string, UserControl>();
            InitializeComponent();
        }

        public void AddTool(string toolName, UserControl tool)
        {
            _tools.Add(toolName,tool);
        }

        public void ShowTool(string toolName)
        {
            ContentView.Children.Clear();

            ContentView.Children.Add(_tools[toolName]);
        }

    }
}
