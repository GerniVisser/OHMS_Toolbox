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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjAzNTcyQDMxMzkyZTM0MmUzMGV5VWFwVnN4eiszU1VtcFBRdUtrZE91ME1kMVJuVm51RFJmcTNnVFl6VDA9");
        }

    }
}
