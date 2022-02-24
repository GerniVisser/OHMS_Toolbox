using Syncfusion.UI.Xaml.Charts;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PillarStability.Helper
{
    public class ReportHelper
    {
        public static Stream ChartStream(SfChart chart)
        {
            Stream outStream = new MemoryStream();

            chart.Width = 824;
            chart.Height = 474;
            chart.Background = new SolidColorBrush(Colors.White);
            chart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            chart.Arrange(new Rect(0, 0, chart.Width, chart.Height));

            chart.Save(outStream, new BmpBitmapEncoder());

            return outStream;
        }

    }
}
