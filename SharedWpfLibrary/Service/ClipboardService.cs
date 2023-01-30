using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static void SaveChartToImage(Visual chart, int width, int height)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif (*.gif)|*.gif|PNG(*.png)|*.png|TIFF(*.tif,*.tiff)|*.tif|All files (*.*)|*.*";

            if (sfd.ShowDialog() == true)
            {

                using (Stream fs = sfd.OpenFile())
                {

                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Pbgra32);

                    renderBitmap.Render(chart);

                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(fs);

                }

            }
        }
    }
}
