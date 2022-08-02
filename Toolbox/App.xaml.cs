using _SharedWpfLibrary.Models;
using _SharedWpfLibrary.Service;
using System.Windows;
using Toolbox.Store;
using Toolbox.ViewModels;

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = new Caveability.ViewModels.CaveabilityViewModel(new Caveability.Models.CaveabilityModel());

            SyncfusionLicanceModel licance = JsonService.SyncfusionLicance();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licance.SyncfusionLicance);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

    }
}
