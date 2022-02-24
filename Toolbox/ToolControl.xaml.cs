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

        public ToolObject ToolObject
        {
            get { return _toolObject; }
            set
            {
                _toolObject = value;
            }
        }


        public ToolControl()
        {
            ToolObject = new ToolObject();

            InitializeComponent();

            this.DataContext = ToolObject;
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Path.Combine(AppDomain.
                CurrentDomain.BaseDirectory
                .SolutionFolder(),
                @"Caveability\bin\Debug\net5.0-windows\Caveability.exe"));
        }
    }
}
