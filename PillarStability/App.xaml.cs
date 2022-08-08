﻿using _SharedWpfLibrary.Models;
using _SharedWpfLibrary.Service;
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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicanseModel.getSyncfusionLicanse());
        }
    }
}
