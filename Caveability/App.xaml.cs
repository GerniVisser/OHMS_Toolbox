using _SharedWpfLibrary.Models;
using _SharedWpfLibrary.Service;
using Caveability.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caveability.Models;

namespace Caveability
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CaveabilityModel _caveabilityModel;

        public App()
        {
            _caveabilityModel = new CaveabilityModel();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_caveabilityModel)
            };
            MainWindow.Show();

            SyncfusionLicanceModel licance = JsonService.SyncfusionLicance();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licance.SyncfusionLicance);

            base.OnStartup(e);
        }
    }
}
