using _SharedWpfLibrary.Models;
using _SharedWpfLibrary.Service;
using PillarStability.ViewModels;
using System.Windows;

namespace PillarStability
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(null)
            };
            MainWindow.Show();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicanseModel.getSyncfusionLicanse());

            base.OnStartup(e);
        }
    }
}
