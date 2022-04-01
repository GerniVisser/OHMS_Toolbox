using _SharedWpfLibrary.Interface;
using Microsoft.Win32;
using PillarStability.Helper;
using PillarStability.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace PillarStability.Services
{
    public class Report : IReport
    {
        private ReportModel _model;

        public Report(ReportModel reportModel)
        {
            _model = reportModel;
        }

        public MemoryStream GenerateReport()
        {
            using (PdfDocument document = new PdfDocument())
            {

                //Add a page to the document
                if (_model.whStream != null)
                {
                    int width = 0;
                    // More than 1 pram means it is a combined report 
                    if (_model.pillarPrams.Count > 1)
                    {
                        document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                        PdfPage whPage = document.Pages.Add();
                        width = (int)(whPage.Graphics.ClientSize.Width - 60);
                        whReport(whPage, width, "Height / Width - Report");
                    }
                    else
                    {
                        document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                        PdfPage whPage = document.Pages.Add();
                        width = (int)(whPage.Graphics.ClientSize.Width - 250);
                        whReport(whPage, width, "Height / Width - Report");
                    }

                }

                if (_model.aveStream != null)
                {
                    int width = 0;

                    if (_model.pillarPrams.Count > 1)
                    {
                        document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                        PdfPage avePage = document.Pages.Add();
                        width = (int)(avePage.Graphics.ClientSize.Width - 60);
                        aveReport(avePage, width, "Pillar Confinement - Report");
                    }
                    else
                    {
                        document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                        PdfPage avePage = document.Pages.Add();
                        width = (int)(avePage.Graphics.ClientSize.Width - 250);
                        aveReport(avePage, width, "Pillar Confinement - Report");
                    }
                }

                if (_model.mcStream != null)
                {
                    document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                    PdfPage mcPage = document.Pages.Add();
                    int width = (int)(mcPage.Graphics.ClientSize.Width - 250);
                    mcReport(mcPage, width, "Monte Carlo Simulation - Report");
                }

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
                catch (Exception ex)
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

        private void whReport(PdfPage page, int width, string header)
        {
            int bodyTop = PageHeader(page, header);

            int pramTop = ChartPanel(page, bodyTop, width, _model.whStream, "Width / Height");

            int resultTop = PramsPanel(page, pramTop, 30, width, _model.pillarPrams);

            ResultPanel(page, resultTop, 30, width, _model.outGridObjects, null);
        }

        private void aveReport(PdfPage page, int width, string header)
        {
            int bodyTop = PageHeader(page, header);

            int pramTop = ChartPanel(page, bodyTop, width, _model.aveStream, "Pillar Confinement");

            int resultTop = PramsPanel(page, pramTop, 30, width, _model.pillarPrams);

            ResultPanel(page, resultTop, 30, width, _model.outGridObjects, null);
        }

        private void mcReport(PdfPage page, int width, string header)
        {
            int bodyTop = PageHeader(page, header);

            int pramTop = ChartPanel(page, bodyTop, width, _model.mcStream, "Monte Carlo Simulation");

            int resultTop = PramsPanel(page, pramTop, 30, width, _model.pillarPrams);

            ResultPanel(page, resultTop, 30, width, null, _model.mcGridObject);
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

        private int ChartPanel(PdfPage page, int top, int width, Stream chart, string header)
        {
            PdfGraphics graphics = page.Graphics;

            float pageWidth = page.Graphics.ClientSize.Width;

            float pageHeight = page.Graphics.ClientSize.Height;

            RectangleF bounds = new RectangleF(0, top, pageWidth, pageHeight);

            PdfSolidBrush brush = new PdfSolidBrush(Color.WhiteSmoke);

            //Draw the rectangle on PDF document
            page.Graphics.DrawRectangle(brush, bounds);

            // Header 

            PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);

            graphics.DrawString(header, headerFont, PdfBrushes.Black, new PointF(30, top + 10));

            // Chart
            var image = new PdfBitmap(chart);

            float ratio = image.Width / width;

            graphics.DrawImage(image, 30, top + 30, width, (int)((width) / ratio));

            return top + 30 + (int)((width) / ratio);

        }

        private int PramsPanel(PdfPage page, int top, int left, int width, List<PillarPrams> pillarPrams)
        {
            PdfGraphics graphics = page.Graphics;

            float pageWidth = page.Graphics.ClientSize.Width;

            PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);

            graphics.DrawString("Pillar parameters", headerFont, PdfBrushes.Black, new PointF(30, top + 5));

            top += 10;
            // Grid 

            PdfGrid pdfGrid = new PdfGrid();

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(" Name");
            dataTable.Columns.Add(" Width");
            dataTable.Columns.Add(" Height");
            dataTable.Columns.Add(" Length");
            dataTable.Columns.Add(" APS");
            dataTable.Columns.Add(" UCS");

            for (int i = 0; i < pillarPrams.Count; i++)
            {
                dataTable.Rows.Add(new object[]
                {
                    pillarPrams[i].Name,
                    pillarPrams[i].Width,
                    pillarPrams[i].Height,
                    pillarPrams[i].Length,
                    pillarPrams[i].APS,
                    pillarPrams[i].UCS
                });
            }

            pdfGrid.DataSource = dataTable;
            // Grid Styling

            PdfGridCellStyle pdfGridRowStyle = new PdfGridCellStyle();

            pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(240, 110, 62));

            pdfGridRowStyle.TextBrush = PdfBrushes.White;

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;

            pdfGridRowStyle.StringFormat = format;

            PdfGridRow pdfGridRow = pdfGrid.Headers[0];

            pdfGridRow.Style = pdfGridRowStyle;

            pdfGridRow.Height = 16;

            for (int i = 0; i < pillarPrams.Count; i++)
            {
                pdfGridRowStyle = new PdfGridCellStyle();

                if (i % 2 == 0)
                {
                    pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(230, 221, 218));
                }
                else
                {
                    pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(227, 182, 166));
                }
                pdfGridRowStyle.StringFormat = format;

                pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                pdfGridRowStyle.TextBrush = PdfBrushes.Black;

                pdfGridRow = pdfGrid.Rows[i];

                pdfGridRow.Style = pdfGridRowStyle;

                pdfGridRow.Height = 14;
            }

            PdfGridCellStyle headerstyle = new PdfGridCellStyle();
            headerstyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            headerstyle.StringFormat = format;

            pdfGrid.Rows.ApplyStyle(headerstyle);

            int heigth = 14 * (_model.pillarPrams.Count + 1) + 14;

            pdfGrid.Draw(page, new RectangleF((float)(left), top + 15, width, heigth));

            return top + heigth;
        }

        private void ResultPanel(PdfPage page, int top, int left, int width, List<OutputGridObject>? outputGridObject, MCGridObject? mCGridObject)
        {
            PdfGraphics graphics = page.Graphics;
            int counter = 0;

            float pageWidth = page.Graphics.ClientSize.Width;

            PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);

            graphics.DrawString("Pillar results", headerFont, PdfBrushes.Black, new PointF(30, top + 10));

            top += 15;

            // Grid 

            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();

            if (outputGridObject != null)
            {
                counter = outputGridObject.Count;

                dataTable.Columns.Add(" Name");
                dataTable.Columns.Add(" Effective Width");
                dataTable.Columns.Add(" Width / Height");
                dataTable.Columns.Add(" Average Stress");
                dataTable.Columns.Add(" Average Confinement");

                for (int i = 0; i < counter; i++)
                {
                    dataTable.Rows.Add(new object[]
                    {
                        outputGridObject[i].Pillar,
                        MathF.Round(outputGridObject[i].Width,3),
                        MathF.Round(outputGridObject[i].WidthtHeight, 3),
                        MathF.Round(outputGridObject[i].AveStress,3),
                        MathF.Round(outputGridObject[i].AveConfinement,3)
                    });
                }
                
            }
            else if(mCGridObject != null)
            {
                counter = 1;

                dataTable.Columns.Add(" Name");
                dataTable.Columns.Add(" DSF");
                dataTable.Columns.Add(" AveSF");
                dataTable.Columns.Add(" StandardDev");
                dataTable.Columns.Add(" mfSF");
                dataTable.Columns.Add(" probSF");

                dataTable.Rows.Add(new object[]
                {
                    mCGridObject.Pillar,
                    MathF.Round(mCGridObject.DSF,3),
                    MathF.Round(mCGridObject.AveSF,3),
                    MathF.Round(mCGridObject.StandardDev,3),
                    MathF.Round(mCGridObject.mfSF,3),
                    MathF.Round(mCGridObject.probSF,3),
                });
            }
            

            pdfGrid.DataSource = dataTable;

            // Grid Styling

            PdfGridCellStyle pdfGridRowStyle = new PdfGridCellStyle();

            pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(240, 110, 62));

            pdfGridRowStyle.TextBrush = PdfBrushes.White;

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;

            pdfGridRowStyle.StringFormat = format;

            PdfGridRow pdfGridRow = pdfGrid.Headers[0];

            pdfGridRow.Style = pdfGridRowStyle;

            pdfGridRow.Height = 16;

            for (int i = 0; i < counter; i++)
            {
                pdfGridRowStyle = new PdfGridCellStyle();

                if (i % 2 == 0)
                {
                    pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(230, 221, 218));
                }
                else
                {
                    pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(227, 182, 166));
                }

                pdfGridRowStyle.StringFormat = format;

                pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                pdfGridRowStyle.TextBrush = PdfBrushes.Black;

                pdfGridRow = pdfGrid.Rows[i];

                pdfGridRow.Style = pdfGridRowStyle;

                pdfGridRow.Height = 14;
            }

            PdfGridCellStyle headerstyle = new PdfGridCellStyle();
            headerstyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            headerstyle.StringFormat = format;

            pdfGrid.Rows.ApplyStyle(headerstyle);

            pdfGrid.Draw(page, new RectangleF((float)(left), top + 15, width, 100));
        }

    }
}
