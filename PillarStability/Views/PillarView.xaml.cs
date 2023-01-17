﻿using PillarStability.Models;
using PillarStability.Services;
using SharedWpfLibrary.Tools;
using SharedWpfLibrary.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using _SharedWpfLibrary.Service;

namespace PillarStability.Views
{
    /// <summary>
    /// Interaction logic for PillarControl.xaml
    /// </summary>
    public partial class PillarView : UserControl
    {
        public PillarView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Propgrid.RefreshPropertygrid();
        }

        private void Propgrid_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }

        private void Propgrid_SelectedPropertyItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
