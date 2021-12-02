using Caveability.Helper;
using Caveability.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveability.Services
{
    public class Report
    {
        private StopeStreamObject _chartStream;

        public Report(StopeStreamObject chartStream)
        {
            _chartStream = chartStream;
        }

        public bool GenerateReport(Wall Footwall, Wall Hangwall, Wall StopeBack, Wall StrikeEnd)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "PDF document (*.pdf)|*.pdf";

            //if (saveFileDialog.ShowDialog() == true)

            try
            {
                using (PdfDocument document = new PdfDocument())
                {
                    //Add a page to the document
                    PdfPage Hangwallpage = document.Pages.Add();

                    WallReport(Hangwallpage, "Hangwall - Report", Hangwall, _chartStream.hangwallStreamObject);

                    PdfPage FootWallpage = document.Pages.Add();

                    WallReport(FootWallpage, "Footwall - Report", Footwall, _chartStream.footwallStreamObject);

                    PdfPage Stopebackpage = document.Pages.Add();

                    WallReport(Stopebackpage, "Stope Back - Report", StopeBack, _chartStream.stopebackStreamObject);

                    PdfPage StrikeEndpage = document.Pages.Add();

                    WallReport(StrikeEndpage, "Strike End - Report", StrikeEnd, _chartStream.strikeendStreamObject);

                    //Save the document

                    document.Save(@"C:\Users\gerni\Downloads\TestPDF.pdf");

                    document.Close(true);

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void WallReport(PdfPage page, string header, Wall wall, ChartStreamObject chartStreamObject)
        {
            int bodyTop = PageHeader(page, header);

            float pageHeight = page.Graphics.ClientSize.Height - bodyTop;

            ChartPanel(page, bodyTop, (int)(pageHeight / 4) - 8, chartStreamObject.A_chartStream, wall, "Calculate A");
            ChartPanel(page, bodyTop + (int)(pageHeight / 4) - 4, (int)(pageHeight / 4) - 6, chartStreamObject.B_chartStream, wall, "Calculate B");
            ChartPanel(page, bodyTop + (int)(pageHeight / 2) - 6, (int)(pageHeight / 4) - 6, chartStreamObject.C_chartStream, wall, "Calculate C");
            ChartPanel(page, bodyTop + (int)((pageHeight / 4) * 3) - 6, (int)(pageHeight / 4) - 6, chartStreamObject.HR_chartStream, wall, "Calculate HR");
        }

        private int PageHeader(PdfPage page, string header)
        {
            PdfGraphics graphics = page.Graphics;

            //LOGO

            var logo = new PdfBitmap(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\OHMS logo.png");

            graphics.DrawImage(logo, 0, 0, 100, 40);

            //Header

            PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 26, PdfFontStyle.Bold);

            graphics.DrawString(header, titleFont, PdfBrushes.OrangeRed, new PointF( 120, 00));

            //Date

            string date = DateTime.Today.ToShortDateString();

            PdfFont dateFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Italic);

            graphics.DrawString(date, dateFont, PdfBrushes.Black, new PointF(0, 50));

            return 70;
        }

        private static void ChartPanel(PdfPage page, int top, int height, Stream chart, Wall wall, string header)
        {
            PdfGraphics graphics = page.Graphics;

            float pageWidth = page.Graphics.ClientSize.Width;

            RectangleF bounds = new RectangleF(0, top, pageWidth, height );

            PdfSolidBrush brush = new PdfSolidBrush(Color.WhiteSmoke);

            //Draw the rectangle on PDF document
            page.Graphics.DrawRectangle(brush, bounds);

            // Header 

            PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);

            graphics.DrawString(header, headerFont, PdfBrushes.Black, new PointF(30, top + 10));

            // Chart
            var image = new PdfBitmap(chart);

            float ratio = image.Width / page.Graphics.ClientSize.Width;

            graphics.DrawImage(image, 30, top + 30, (int)(height * ratio / 1.2) - 10, (int)(height / 1.4));

            // Grid 

            PdfGrid pdfGrid = new PdfGrid();

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(" Property");
            dataTable.Columns.Add(" Value");

            List<List<String>> gridList = WallObjects(header, wall);

            for (int i = 0; i < gridList.Count; i++)
            {
                dataTable.Rows.Add(new object[] { gridList[i][0], gridList[i][1]});
            }

            pdfGrid.DataSource = dataTable;

            // Grid Styling

            PdfGridRowStyle pdfGridRowStyle = new PdfGridRowStyle();

            pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(240, 110, 62));

            pdfGridRowStyle.TextBrush = PdfBrushes.White;

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);


            PdfGridRow pdfGridRow = pdfGrid.Headers[0];

            pdfGridRow.Style = pdfGridRowStyle;

            pdfGridRow.Height = 16;

            for (int i = 0; i < gridList.Count; i++)
            {
                pdfGridRowStyle = new PdfGridRowStyle();

                if (i % 2 == 0)
                {
                    pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(230, 221, 218));
                }
                else
                {
                    pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(227, 182, 166));
                }

                pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                pdfGridRowStyle.TextBrush = PdfBrushes.Black;

                pdfGridRow = pdfGrid.Rows[i];

                pdfGridRow.Style = pdfGridRowStyle;

                pdfGridRow.Height = 14;
            }

            pdfGrid.Draw(page, new RectangleF((float)(pageWidth / 1.8), top + 30, 120, 160));
        }

        private static List<List<String>> WallObjects(string name, Wall wall)
        {
            List<List<String>> objectList = new List<List<String>>();

            if(name == "Calculate A")
            {
                objectList.Add(new List<string>{ " oc", " " + wall.A._oc.ToString() });
                objectList.Add(new List<string>{ " omax", " " + wall.A._omax.ToString() });
            }
            else if (name == "Calculate B")
            {
                objectList.Add(new List<string> { " b", " " + wall.B._b.ToString() });
            }
            else if (name == "Calculate C")
            {
                objectList.Add(new List<string> { " c", " " + wall.C._c.ToString() });
            }
            else
            {
                objectList.Add(new List<string> { " RQD", " " + wall.Q._rqd.ToString() });
                objectList.Add(new List<string> { " Jn", " " + wall.Q._jn.ToString() });
                objectList.Add(new List<string> { " Jr", " " + wall.Q._jr.ToString() });
                objectList.Add(new List<string> { " Ja", " " + wall.Q._ja.ToString() });
                objectList.Add(new List<string> { " Length", " " + wall.HR._length.ToString() });
                objectList.Add(new List<string> { " Width", " " + wall.HR._width.ToString() });
            }

            return objectList;


        }
    }
}
