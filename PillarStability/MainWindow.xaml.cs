
using Syncfusion.Windows.Tools.Controls;
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

namespace PillarStability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

        }

        private void MenuItemAdv_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonAdv_Click(object sender, RoutedEventArgs e)
        {
            var newPillar = new PillarControl();

            TabItemExt tabItem = new TabItemExt()
            {
                Content = newPillar,
                CloseButtonState = Visibility.Visible,
                Header = newPillar.getPillarModel.Name,

            };

            TabControleMain.Items.Add(tabItem);
            TabControleMain.SelectedItem = tabItem;
            TabControleMain.EnableLabelEdit = true;
        }
    }
}
