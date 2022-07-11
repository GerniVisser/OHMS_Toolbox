using _SharedWpfLibrary.Interface;
using Caveability.Models;
using Microsoft.Win32;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using Syncfusion.Windows.PdfViewer;
using System.Data;
using System.Drawing;
using System.IO;
using Syncfusion.Pdf.Parsing;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace Caveability.Services
{
    public class Report : IReport
    {
        private ReportModel _model;

        public Report(ReportModel model)
        {
            _model = model;
        }

        public MemoryStream GenerateReport()
        {
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                if(_model.hangwall != null)
                {
                    PdfPage Hangwallpage = document.Pages.Add();

                    WallReport(Hangwallpage, "Hangwall - Report", _model.hangwall, _model.hangwallStream);

                }

                if (_model.footwall != null)
                {
                    PdfPage Footwallpage = document.Pages.Add();

                    WallReport(Footwallpage, "Footwall - Report", _model.footwall, _model.footwallStream);

                }

                if (_model.stopeback != null)
                {
                    PdfPage Stopebackpage = document.Pages.Add();

                    WallReport(Stopebackpage, "Stope Back - Report", _model.stopeback, _model.stopebackStream );

                }

                if (_model.strikeend != null)
                {
                    PdfPage Strikeendpage = document.Pages.Add();

                    WallReport(Strikeendpage, "Strike End - Report", _model.strikeend, _model.strikeendStream);

                }
                //Save the document

                MemoryStream stream = new MemoryStream();

                document.Save(stream);

                return stream;

            }
        }

        public void SaveReportImage()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image(.jpeg) | *.jpeg | Png Image(.png) | *.png";
            string filepath = "";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    PdfViewerControl pdfViewer = new PdfViewerControl();

                    MemoryStream tempStream = GenerateReport();

                    //Load the input PDF file
                    PdfLoadedDocument loadedDocument = new PdfLoadedDocument(tempStream);

                    pdfViewer.Load(loadedDocument);

                    //Export the particular PDF page as image at the page index of 0
                    for (int i = 0; i < pdfViewer.PageCount; i++)
                    {
                        BitmapSource image = pdfViewer.ExportAsImage(i);

                        //Set up the output path.
                        if (image != null)
                        {
                            //Initialize the new Jpeg bitmap encoder.
                            BitmapEncoder encoder = new JpegBitmapEncoder();
                            //Create the bitmap frame using the bitmap source and add it to the encoder.
                            encoder.Frames.Add(BitmapFrame.Create(image));
                            //Create the file stream for the output in the desired image format.
                            filepath = saveFileDialog.FileName.Insert(saveFileDialog.FileName.IndexOf("."), " (" + i + ")");
                            FileStream stream = new FileStream(filepath, FileMode.Create);
                            //Save the stream so that the image will be generated in the output location.
                            encoder.Save(stream);
                            stream.Close();
                        }
                    }

                    //Dispose the document.
                    tempStream.Dispose();
                    loadedDocument.Dispose();
                    loadedDocument = null;

                    string Location_ToOpen = filepath;
                    if (!File.Exists(Location_ToOpen))
                    {
                        return;
                    }

                    string argument = "/open, \"" + Location_ToOpen + "\"";

                    Process.Start("explorer.exe", argument);
                }
                catch
                {

                }
            }
        }

        public void SaveReportPDF()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF document(*.pdf)| *.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var tempStream = GenerateReport();
                    string filepath = saveFileDialog.FileName;
                    PdfLoadedDocument document = new PdfLoadedDocument(tempStream);
                    document.Save(filepath);
                    document.Close(true);
                    tempStream.Dispose();

                    string Location_ToOpen = filepath;
                    if (!File.Exists(Location_ToOpen))
                    {
                        return;
                    }

                    string argument = "/open, \"" + Location_ToOpen + "\"";

                    Process.Start("explorer.exe", argument);
                }
                catch { }
            }
        }

        private void WallReport(PdfPage page, string header, Wall wall, ChartStreamModel chartStreamObject)
        {
            int bodyTop = PageHeader(page, header);

            int chartTop = StatsPanel(page, bodyTop, wall);

            float pageHeight = page.Graphics.ClientSize.Height - bodyTop - chartTop;

            ChartPanel(page, bodyTop + chartTop, (int)(pageHeight / 4) - 8, chartStreamObject.A_chartStream, wall, "Calculate A");
            ChartPanel(page, bodyTop + chartTop + (int)(pageHeight / 4) - 4, (int)(pageHeight / 4) - 6, chartStreamObject.B_chartStream, wall, "Calculate B");
            ChartPanel(page, bodyTop + chartTop + (int)(pageHeight / 2) - 6, (int)(pageHeight / 4) - 6, chartStreamObject.C_chartStream, wall, "Calculate C");
            ChartPanel(page, bodyTop + chartTop + (int)((pageHeight / 4) * 3) - 6, (int)(pageHeight / 4) - 6, chartStreamObject.HR_chartStream, wall, "Calculate HR");
        }

        private int PageHeader(PdfPage page, string header)
        {
            PdfGraphics graphics = page.Graphics;

            //LOGO

            var logo = new PdfBitmap(AppDomain.CurrentDomain.BaseDirectory + @"\Icons\OHMS logo.png");

            graphics.DrawImage(logo, 0, 0, 100, 40);

            //Header

            PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 26, PdfFontStyle.Bold);

            graphics.DrawString(header, titleFont, PdfBrushes.OrangeRed, new PointF(120, 00));

            //Date

            string date = DateTime.Today.ToShortDateString();

            PdfFont dateFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Italic);

            graphics.DrawString(date, dateFont, PdfBrushes.Black, new PointF(0, 50));

            return 70;
        }

        private static int StatsPanel(PdfPage page, int top, Wall wall)
        {
            PdfGrid pdfgrid = new PdfGrid();

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(" Current HR ");
            dataTable.Columns.Add(" Max allowed HR ");
            dataTable.Columns.Add(" Max allowed Length ");

            var n = N_Model.Calculate(wall.Q.Calculate(), wall.A.Calculate(), wall.B.Calculate(), wall.C.Calculate());

            dataTable.Rows.Add(new object[] {
                "  " + Math.Round(wall.HR.Calculate(), 2).ToString(),
                "  " + Math.Round(wall.HR.CalculateXAxis(n), 2).ToString(),
                "  " + wall.HR.GetMaxLenght((float)(n)).ToString()
            });

            pdfgrid.DataSource = dataTable;

            PdfGridRowStyle pdfGridRowStyle = new PdfGridRowStyle();

            pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(240, 110, 62));

            pdfGridRowStyle.TextBrush = PdfBrushes.White;

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);


            PdfGridRow pdfGridRow = pdfgrid.Headers[0];

            pdfGridRow.Style = pdfGridRowStyle;

            pdfGridRow.Height = 16;

            pdfGridRowStyle = new PdfGridRowStyle();

            pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(230, 221, 218));

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

            pdfGridRowStyle.TextBrush = PdfBrushes.Black;

            pdfGridRow = pdfgrid.Rows[0];

            pdfGridRow.Style = pdfGridRowStyle;

            pdfGridRow.Height = 14;

            pdfgrid.Draw(page, new RectangleF(0, top + 10, (float)(page.Graphics.ClientSize.Width), 80));

            return 60;
        }

        private static void ChartPanel(PdfPage page, int top, int height, Stream chart, Wall wall, string header)
        {
            PdfGraphics graphics = page.Graphics;

            float pageWidth = page.Graphics.ClientSize.Width;

            RectangleF bounds = new RectangleF(0, top, pageWidth, height);

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
                dataTable.Rows.Add(new object[] { gridList[i][0], gridList[i][1] });
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

            if (name == "Calculate A")
            {
                objectList.Add(new List<string> { " oc", " " + wall.A._oc.ToString() });
                objectList.Add(new List<string> { " omax", " " + wall.A._omax.ToString() });
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
