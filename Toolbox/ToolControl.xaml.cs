using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string temp = System.IO.Path.Combine(AppDomain.
                CurrentDomain.BaseDirectory
                .SolutionFolder());

            Caveability.MainWindow mw = new Caveability.MainWindow();
            mw.Show();

            //var psi = new System.Diagnostics.ProcessStartInfo() { FileName = (temp + @"\Caveability\bin\Debug\net5.0-windows\Caveability.exe")
            //    , UseShellExecute = true };

            //System.Diagnostics.Process.Start(psi);
        }
    }
}
