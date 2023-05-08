using Borehole.Commands;
using Borehole.Models;
using Borehole.Services;
using Microsoft.Win32;
using SharedWpfLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace Borehole.ViewModels
{
    public class BoreholeViewModel : ViewModelBase
    {
        private BoreholeModel _boreholeModel;
        private ICommand _litOpenFileCommand;
        private ICommand _geoOpenFileCommand;
        private ICommand _combineCommand;

        public ICommand LitOpenFileCommand
        {
            get { return _litOpenFileCommand; }
        }
        

        public ICommand GeoOpenFileCommand
        {
            get { return _geoOpenFileCommand; }
        }

        public ICommand CombineCommand
        {
            get { return _combineCommand; }
        }

        public BoreholeViewModel(BoreholeModel boreholeModel)
        {
            _boreholeModel = boreholeModel;
            _litOpenFileCommand = new DelegateCommand(loadLitFile);
            _geoOpenFileCommand = new DelegateCommand(loadGeoFile);
            _combineCommand = new DelegateCommand(Combine);
        }

        public List<string> GeoColumnSource
        {
            get { return _boreholeModel.geoTechModel.colomns; }
        }

        public int GeoIdIndex
        {
            get { return _boreholeModel.geoTechModel.IdColumn; }
            set { _boreholeModel.geoTechModel.IdColumn = value; }
        }

        public int GeoDistIndex
        {
            get { return _boreholeModel.geoTechModel.distColumn; }
            set { _boreholeModel.geoTechModel.distColumn = value; }
        }

        public List<string> LitColumnSource
        {
            get { return _boreholeModel.litModel.colomns; }
        }

        public int LitIdIndex
        {
            get { return _boreholeModel.litModel.IdColumn; }
            set { _boreholeModel.litModel.IdColumn = value; }
        }

        public int LitFromIndex
        {
            get { return _boreholeModel.litModel.fromColumn; }
            set { _boreholeModel.litModel.fromColumn = value; }
        }

        public int LitToIndex
        {
            get { return _boreholeModel.litModel.toColumn; }
            set { _boreholeModel.litModel.toColumn = value; }
        }

        public int LitTypeIndex
        {
            get { return _boreholeModel.litModel.typeColumn; }
            set { _boreholeModel.litModel.typeColumn = value; }
        }

        private void loadLitFile(object obj)
        {
            var filePath = OpenFile();
            // If something goes wrong with finding a failid file path
            if (filePath == null) return;

            var columns = getColumns(filePath);
            // If columns could not be extracted
            if (columns == null) return;

            LitModel litModel = new LitModel()
            {
                filepath = filePath,
                colomns = columns,
            };

            _boreholeModel.litModel = litModel;
            OnPropertyChanged(nameof(LitColumnSource));
            return;
        }

        private void loadGeoFile(object obj)
        {
            var filePath = OpenFile();
            // If something goes wrong with finding a failid file path
            if (filePath == null) return;

            var columns = getColumns(filePath);
            // If columns could not be extracted
            if (columns == null) return;

            GeoTechModel geoTechModel = new GeoTechModel
            {
                filepath = filePath,
                colomns = columns,
            };

            _boreholeModel.geoTechModel = geoTechModel;
            OnPropertyChanged(nameof(GeoColumnSource));
            return;
        } 

        private string OpenFile()
        {
            // Display the load file dialog to select the input file
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true)
                return null;

            return openFileDialog.FileName;
        }

        private List<string> getColumns(string filePath)
        {
            List<string> columns = new List<string>();
            using (var reader = new StreamReader(filePath))
            {
                if (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    return line.Split(',').ToList();
                }
            }
            return null;
        }

        private void Combine(object obj)
        {
            var combineService = new CombineFilesService(_boreholeModel.litModel, _boreholeModel.geoTechModel);

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set the default file name and extension filter
            saveFileDialog.FileName = "data.csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                // Get the selected file name and path
                string fileName = saveFileDialog.FileName;

                combineService.Combine(fileName);
            }

            return;
        }
    }
}
