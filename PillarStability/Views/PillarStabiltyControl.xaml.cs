using Microsoft.Win32;
using PillarStability.Models;
using PillarStability.Services;
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

namespace PillarStability.Views
{
    /// <summary>
    /// Interaction logic for PillarStabiltyControl.xaml
    /// </summary>
    public partial class PillarStabiltyControl : UserControl
    {
        private PillarControl _pillarControl;
        private CombinedPillarControl _combinedPillarControl;
        private List<PillarModel> _pillarModelList;
        private string _saveFileName = "";
        private int PillarNumber = 0;

        public PillarStabiltyControl()
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
            rep.SaveReportPDF();
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
            PillarNumber++;
            var newPillarModel = new PillarModel("Pillar " + PillarNumber);
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
                catch
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
                CanClose = false,
                CloseButtonState = Visibility.Hidden,
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
                    Header = pillarModel.Name,
                };

                TabControleMain.Items.Add(tabItem);
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
            for (int i = 0; i < TabControleMain.Items.Count; i++)
            {
                if (e.TargetTabItem == TabControleMain.Items[i])
                {
                    _pillarModelList.RemoveAt(i - 1);
                    TabControleMain.SelectedIndex = 0;

                    if (TabControleMain.SelectedIndex == 0)
                    {
                        _combinedPillarControl.update();
                    }
                    return;
                }
            }
        }

        private void TabControlExt_OnCloseAllTabs(object sender, CloseTabEventArgs e)
        {
            _pillarModelList.Clear();

            TabControleMain.SelectedIndex = 0;
            PillarNumber = 0;
        }

        private void TabControlExt_OnCloseOtherTabs(object sender, CloseTabEventArgs e)
        {
            _pillarModelList.RemoveRange(0, TabControleMain.SelectedIndex);

            if (_pillarModelList.Count > 0)
            {
                _pillarModelList.RemoveRange(1, _pillarModelList.Count - 1);
            }

            TabControleMain.SelectedIndex = 0;
        }

        private void TabControleMain_AfterLabelEdit(object sender, AfterLabelEditEventArgs e)
        {
            if (TabControleMain.SelectedIndex >= 0)
            {
                for (int i = 0; i < TabControleMain.Items.Count; i++)
                {
                    if (e.TabItem == TabControleMain.Items[i] && i != 0)
                    {
                        _pillarModelList[i - 1].Name = e.HeaderAfterEdit.ToString();
                        _pillarControl.update();
                        return;
                    }
                }
            }
        }
    }
}
