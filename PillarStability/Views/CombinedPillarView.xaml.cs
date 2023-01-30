using SharedWpfLibrary.Service;
using PillarStability.Models;
using PillarStability.Services;
using SharedWpfLibrary.Tools;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PillarStability.Views
{
    /// <summary>
    /// Interaction logic for CombinedPillarControl.xaml
    /// </summary>
    public partial class CombinedPillarView : UserControl
    {
        public CombinedPillarView()
        {
            InitializeComponent();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            SharedWpfLibrary.Service.ClipboardService.CopyToClipboard(combined_Chart, (int)(combined_Chart.ActualWidth), (int)(combined_Chart.ActualHeight));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SharedWpfLibrary.Service.ClipboardService.SaveChartToImage(combined_Chart, (int)(combined_Chart.ActualWidth), (int)(combined_Chart.ActualHeight));
        }
    }
}
