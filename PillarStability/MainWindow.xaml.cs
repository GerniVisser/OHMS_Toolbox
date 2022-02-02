
using PillarStability.Models;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PillarStability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PillarControl _pillarControl;
        private List<PillarModel> _pillarModelList;

        public MainWindow()
        {
            InitializeComponent();

            var combinedPillarModel = new PillarModel("Combined Pillar");

            _pillarModelList = new List<PillarModel>();
            _pillarModelList.Add(combinedPillarModel);

            _pillarControl = new PillarControl(_pillarModelList[0]);

            addPillar(_pillarModelList[0]);
        }

        private void MenuItemAdv_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonAdv_Click(object sender, RoutedEventArgs e)
        {
            var newPillarModel = new PillarModel("Pillar " + (_pillarModelList.Count));
            _pillarModelList.Add(newPillarModel);

            addPillar(_pillarModelList[_pillarModelList.Count - 1]);
        }

        private void addPillar(PillarModel pillarModel)
        {
            if (_pillarControl.setPillarModel(pillarModel))
            {
                TabItemExt tabItem = new TabItemExt()
                {
                    Content = _pillarControl,
                    CloseButtonState = Visibility.Visible,
                    Header = _pillarControl.getPillarModel.Name,

                };

                TabControleMain.Items.Add(tabItem);
                TabControleMain.SelectedItem = tabItem;
            }
        }

        private void TabControleMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _pillarControl.setPillarModel(_pillarModelList[TabControleMain.SelectedIndex]);
        }
    }
}
