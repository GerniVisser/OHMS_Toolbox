using Borehole.ViewModels;
using SharedWpfLibrary.Models;
using SharedWpfLibrary.Service;
using System.Windows;

namespace Borehole
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

            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicanseModel.getSyncfusionLicanse());

            base.OnStartup(e);
        }
    }
}
