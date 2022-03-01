using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharedWpfLibrary.Service
{
    public static class ClipboardService
    {
        public static void CopyToClipboard(Visual chart, int width, int height)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Pbgra32);

            renderBitmap.Render(chart);

            Clipboard.SetImage(renderBitmap);
        }
    }
}
