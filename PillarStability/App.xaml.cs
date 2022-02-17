using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            base.OnStartup(e);

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTgwNDQ5QDMxMzkyZTM0MmUzMFRrR0RFajVDZEh2MG5FSGlqaE81T1A3YWVPUFAvTFpkNGROSDVIQmo0RVU9");
        }
    }
}
