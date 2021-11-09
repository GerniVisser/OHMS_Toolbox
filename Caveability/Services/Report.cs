using Caveability.Helper;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class Report
    {
        public static bool GenerateReport(ChartStreamObject chartStream, string location)
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

                    var image = new PdfBitmap(chartStream.HR_chartStream);

                    double ratio = image.Width / page.Graphics.ClientSize.Width;

                    graphics.DrawImage(image, 0, 0, page.Graphics.ClientSize.Width, (int)(image.Height / ratio));

                    //Draw the text
                    graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

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
    }
}
