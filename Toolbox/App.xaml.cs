using _SharedWpfLibrary.Models;
using _SharedWpfLibrary.Service;
using System.Windows;

namespace Toolbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SyncfusionLicanceModel licance = JsonService.SyncfusionLicance();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licance.SyncfusionLicance);
        }

    }
}
