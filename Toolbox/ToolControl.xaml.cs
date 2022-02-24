using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Toolbox.Helper;

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for ToolControl.xaml
    /// </summary>
    public partial class ToolControl : UserControl
    {
        private ToolObject _toolObject;
        private string _location;

        public ToolObject ToolObject
        {
            get { return _toolObject; }
            set
            {
                _toolObject = value;
            }
        }


        public ToolControl(string RelativeFileLocation)
        {
            _location = RelativeFileLocation;
            ToolObject = new ToolObject();

            InitializeComponent();

            this.DataContext = ToolObject;
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            var a = AppDomain.
                  CurrentDomain.BaseDirectory
                  .SolutionFolder();
            var b = Path.Combine(a,
                _location);
            Process.Start(b);
        }
    }
}
