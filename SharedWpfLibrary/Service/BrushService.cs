using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _SharedWpfLibrary.Service
{
    public static class BrushService
    {
        public static Brush getRandomBrush()
        {
            string[] brushArray = typeof(Brushes).GetProperties().
                                        Select(c => c.Name).ToArray();

            Random randomGen = new Random();
            string randomColorName = brushArray[randomGen.Next(brushArray.Length)];
            SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString(randomColorName);

            return color;
        }
    }
}
