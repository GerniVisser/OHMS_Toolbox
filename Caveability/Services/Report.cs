using Caveability.Helper;
using Caveability.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public partial class Report : WallControle
    {
        public static bool GenerateReport(StopeStreamObject chartStream, string location)
        {
            try
            {
                using (PdfDocument document = new PdfDocument())
                {
                    //Add a page to the document
                    PdfPage page = document.Pages.Add();

                    //Create PDF graphics for a page
                    PdfGraphics graphics = page.Graphics;

                    //Set the standard font
                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                    //ChartCollage(chartStream.footwallStreamObject, page);
                    graphics.DrawImage(new PdfBitmap(chartStream.footwallStreamObject.B_chartStream), 0, 0);

                    //Draw the text
                    graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

                    PdfLightTable pdfLightTable = new PdfLightTable();

                    //Save the document

                    document.Save(location);

                    document.Close(true);

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        static IntPtr ApplicationMessageFilter(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            return IntPtr.Zero;
        }

        private static void ChartCollage(ChartStreamObject chartStream, PdfPage page)
        {
            PdfGraphics graphics = page.Graphics;

            double pageWidth = page.Graphics.ClientSize.Width;
            double pageHeight = page.Graphics.ClientSize.Height;

            var A_image = new PdfBitmap(chartStream.B_chartStream);
            var B_image = new PdfBitmap(chartStream.A_chartStream);

            double ratio = A_image.Width / page.Graphics.ClientSize.Width;

            graphics.DrawImage(A_image, 0, (int)(pageHeight /2), (int)(pageWidth/2) - 10, (int)(A_image.Height / ratio)/2);

            graphics.DrawImage(B_image, (int)(pageWidth / 2) + 10, (int)(pageHeight / 2), (int)(pageWidth / 2) - 10, (int)(B_image.Height / ratio) / 2);
        }
    }
}
