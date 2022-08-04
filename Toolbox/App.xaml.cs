﻿using _SharedWpfLibrary.Models;
using _SharedWpfLibrary.Service;
using _SharedWpfLibrary.ViewModels;
using System.Collections.Generic;
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
            List<ViewModelBase> viewModelBases = new List<ViewModelBase>()
            {
                new HomeViewModel(),
                new Caveability.ViewModels.CaveabilityViewModel(new Caveability.Models.CaveabilityModel()),
                new WIPViewModel("Pillar Stability calculator")
            };

            _navigationStore = new NavigationStore(viewModelBases);

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
