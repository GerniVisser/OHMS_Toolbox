using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _SharedWpfLibrary.Service
{
    public class ReportStreamService
    {
        public static Stream ChartStream(SfChart chart)
        {
            Stream outStream = new MemoryStream();

            var temp = chart.Clone() as SfChart;

            temp.Width = 1024;
            temp.Height = 768;
            temp.Background = new SolidColorBrush(Colors.White);
            temp.Measure(new Size(temp.Width, temp.Height));
            temp.Arrange(new Rect(0, 0, temp.Width, temp.Height));

            temp.Save(outStream, new BmpBitmapEncoder());
            temp.Dispose();

            return outStream;
        }
    }
}
