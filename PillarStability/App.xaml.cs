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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTMxMjI4QDMxMzkyZTMzMmUzMFdVTEs3a1U0bnZCd2dweUd6L0l2L3pVK0JRTERPRXhubGE3Um9pNitUdm89");
        }
    }
}
