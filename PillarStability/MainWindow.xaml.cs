
using Microsoft.Win32;
using PillarStability.Models;
using PillarStability.Services;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
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
        private CombinedPillarControl _combinedPillarControl;
        private List<PillarModel> _pillarModelList;
        private string _saveFileName = "";

        public MainWindow()
        {
            InitializeComponent();

            _pillarModelList = new List<PillarModel>();
            _pillarControl = new PillarControl();

            PopulateEmptyControle();
        }

        private void ExportAsPDF_Click(object sender, RoutedEventArgs e)
        {
            var reportModel = _combinedPillarControl.getChartStreams();

            Report rep = new Report(reportModel);
            rep.SaveReportPDf();
        }

        private void ExportAsImage_Click(object sender, RoutedEventArgs e)
        {
            var reportModel = _combinedPillarControl.getChartStreams();

            Report rep = new Report(reportModel);
            rep.SaveReportImage();
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            _pillarModelList.Clear();

            PopulateEmptyControle();
        }

        private void AddPillar_Click(object sender, RoutedEventArgs e)
        {
            var newPillarModel = new PillarModel("Pillar " + (TabControleMain.Items.Count));
            _pillarModelList.Add(newPillarModel);

            addPillar(_pillarModelList[_pillarModelList.Count - 1]);
        }

        private void OpenFromCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    List<PillarModel> pillarModels = csvLoader.LoadCsvToPillarModels(saveFileDialog.FileName);
                    LoadPillarModdels(pillarModels);
                    _saveFileName = saveFileDialog.FileName;
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_saveFileName != "")
            {
                csvLoader.SavePillarModel(_saveFileName, _pillarModelList);
            }
            else
            {
                SaveAs_Click(null, null);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                _saveFileName = saveFileDialog.FileName;
                csvLoader.SavePillarModel(_saveFileName, _pillarModelList);
            }
        }

        private void PopulateEmptyControle()
        {
            _combinedPillarControl = new CombinedPillarControl(_pillarModelList);

            TabItemExt tabItem = new TabItemExt()
            {
                Content = _combinedPillarControl,
                Header = "Summary",
                CanClose = false
            };

            TabControleMain.Items.Clear();
            TabControleMain.Items.Add(tabItem);
        }

        private void LoadPillarModdels(List<PillarModel> pillarModels)
        {
            _pillarModelList = pillarModels;

            PopulateEmptyControle();

            for (int i = 0; i <= _pillarModelList.Count - 1; i++)
            {
                addPillar(_pillarModelList[i]);
            }
        }

        private void addPillar(PillarModel pillarModel)
        {
            if (_pillarControl.setPillarModel(pillarModel))
            {
                TabItemExt tabItem = new TabItemExt()
                {
                    Content = _pillarControl,
                    Header = _pillarControl.getPillarModel.Name,
                };

                TabControleMain.Items.Add(tabItem);
                TabControleMain.SelectedItem = tabItem;
            }
            else
            {
                throw new Exception("Pillar model could not be set correctly");
            }

        }

        private void TabControleMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControleMain.SelectedIndex > 0)
            {
                _pillarControl.setPillarModel(_pillarModelList[TabControleMain.SelectedIndex - 1]);
            }
            else
            {
                TabControleMain.SelectedIndex = 0;
            }

            if (TabControleMain.SelectedIndex == 0)
            {
                _combinedPillarControl.update();
            }
        }

        private void TabControlExt_OnCloseButtonClick(object sender, CloseTabEventArgs e)
        {
            int index = TabControleMain.SelectedIndex - 1;

            _pillarModelList.RemoveAt(index);

            TabControleMain.SelectedIndex = index - 1;
        }

        private void TabControlExt_OnCloseAllTabs(object sender, CloseTabEventArgs e)
        {
            _pillarModelList.Clear();

            TabControleMain.SelectedIndex = 0;
        }

        private void TabControlExt_OnCloseOtherTabs(object sender, CloseTabEventArgs e)
        {
            _pillarModelList.RemoveRange(0, TabControleMain.SelectedIndex);
            _pillarModelList.RemoveRange(1, _pillarModelList.Count - 1);

            TabControleMain.SelectedIndex = 0;
        }
    }
}
