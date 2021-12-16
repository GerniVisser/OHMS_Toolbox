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

            Caveability.MainWindow mw = new Caveability.MainWindow();
            mw.Show();

            //var psi = new System.Diagnostics.ProcessStartInfo() { FileName = (temp + @"\Caveability\bin\Debug\net5.0-windows\Caveability.exe")
            //    , UseShellExecute = true };

            //System.Diagnostics.Process.Start(psi);
        }
    }
}
