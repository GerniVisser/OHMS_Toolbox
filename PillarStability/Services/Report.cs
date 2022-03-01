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
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace PillarStability.Services
{
    public class Report
    {
        private ReportModel _model;

        public Report(ReportModel reportModel)
        {
            ReportModel = reportModel;
        }
        public ReportModel ReportModel
        {
            get { return _model; }
            set { _model = value; }
        }

        public void SaveReportImage()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image(.jpeg) | *.jpeg | Png Image(.png) | *.png";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    PdfViewerControl pdfViewer = new PdfViewerControl();

                    MemoryStream tempStream = GenerateReportPdf();

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
                            FileStream stream = new FileStream(saveFileDialog.FileName.Insert(saveFileDialog.FileName.IndexOf("."), " (" + i + ")"), FileMode.Create);
                            //Save the stream so that the image will be generated in the output location.
                            encoder.Save(stream);
                        }
                    }

                    //Dispose the document.
                    tempStream.Dispose();
                    loadedDocument.Dispose();
                    loadedDocument = null;
                }
                catch (Exception ex)
                {

                }
            }
        }

        public MemoryStream GenerateReportPdf()
        {
            using (PdfDocument document = new PdfDocument())
            {
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                //Add a page to the document
                if(_model.whStream != null)
                {
                    PdfPage whPage = document.Pages.Add();

                    whReport(whPage, "Height / Width - Report");
                }
                
                if(_model.aveStream != null)
                {
                    PdfPage avePage = document.Pages.Add();

                    aveReport(avePage, "Pillar Confinement - Report");
                }

                if(_model.mcStream != null)
                {
                    PdfPage mcPage = document.Pages.Add();

                    mcReport(mcPage, "Monte Carlo Simulation - Report");
                }

                MemoryStream stream = new MemoryStream();

                document.Save(stream);

                return stream;
            }

        }

        public void SaveReportPDf()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF document(*.pdf)| *.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var tempStream = GenerateReportPdf();
                    PdfLoadedDocument document = new PdfLoadedDocument(tempStream);
                    document.Save(saveFileDialog.FileName);
                    document.Close(true);
                    tempStream.Dispose();
                }
                catch { }
            }
        }

        private void whReport(PdfPage page, string header)
        {
            int bodyTop = PageHeader(page, header);

            int chartTop = 0;

            float pageHeight = page.Graphics.ClientSize.Height - bodyTop - chartTop;

            ChartPanel(page, bodyTop - chartTop, (int)(pageHeight) - 8, _model.whStream, "Width / Height");

            PramsPanel(page, 380, 30, _model.pillarPrams);

            ResultPanel(page, 430, 30, _model.outGridObjects, null);
        }

        private void aveReport(PdfPage page, string header)
        {
            int bodyTop = PageHeader(page, header);

            int chartTop = 0;

            float pageHeight = page.Graphics.ClientSize.Height - bodyTop - chartTop;

            ChartPanel(page, bodyTop - chartTop, (int)(pageHeight) - 8, _model.aveStream, "Pillar Confinement");

            PramsPanel(page, 380, 30, _model.pillarPrams);

            ResultPanel(page, 430, 30, _model.outGridObjects, null);
        }

        private void mcReport(PdfPage page, string header)
        {
            int bodyTop = PageHeader(page, header);

            int chartTop = 0;

            float pageHeight = page.Graphics.ClientSize.Height - bodyTop - chartTop;

            ChartPanel(page, bodyTop - chartTop, (int)(pageHeight) - 8, _model.mcStream, "Monte Carlo Simulation");

            PramsPanel(page, 380, 30, _model.pillarPrams);

            ResultPanel(page, 430, 30, null, _model.mcGridObject);
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

        private void ChartPanel(PdfPage page, int top, int height, Stream chart, string header)
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

            graphics.DrawImage(image, 30, top + 30, (int)(590), (int)(height / 1.5));

        }

        private void PramsPanel(PdfPage page, int top, int left, List<PillarPrams> pillarPrams)
        {
            PdfGraphics graphics = page.Graphics;

            float pageWidth = page.Graphics.ClientSize.Width;
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

            PdfGridRowStyle pdfGridRowStyle = new PdfGridRowStyle();

            pdfGridRowStyle.BackgroundBrush = new PdfSolidBrush(Color.FromArgb(240, 110, 62));

            pdfGridRowStyle.TextBrush = PdfBrushes.White;

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);


            PdfGridRow pdfGridRow = pdfGrid.Headers[0];

            pdfGridRow.Style = pdfGridRowStyle;

            pdfGridRow.Height = 16;

            for (int i = 0; i < pillarPrams.Count; i++)
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

            pdfGrid.Draw(page, new RectangleF((float)(left), top + 30, 590, 100));
        }

        private void ResultPanel(PdfPage page, int top, int left, List<OutputGridObject>? outputGridObject, MCGridObject? mCGridObject)
        {
            PdfGraphics graphics = page.Graphics;
            int counter = 0;

            float pageWidth = page.Graphics.ClientSize.Width;
            // Grid 

            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();

            if (outputGridObject != null)
            {
                counter = outputGridObject.Count;

                dataTable.Columns.Add(" Effective Width");
                dataTable.Columns.Add(" Width / Height");
                dataTable.Columns.Add(" Average Stress");
                dataTable.Columns.Add(" Average Confinement");

                for (int i = 0; i < counter; i++)
                {
                    dataTable.Rows.Add(new object[]
                    {
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

                dataTable.Columns.Add(" DSF");
                dataTable.Columns.Add(" AveSF");
                dataTable.Columns.Add(" StandardDev");
                dataTable.Columns.Add(" mfSF");
                dataTable.Columns.Add(" probSF");

                dataTable.Rows.Add(new object[]
                {
                MathF.Round(mCGridObject.DSF,3),
                MathF.Round(mCGridObject.AveSF,3),
                MathF.Round(mCGridObject.StandardDev,3),
                MathF.Round(mCGridObject.mfSF,3),
                MathF.Round(mCGridObject.probSF,3),
                });
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

            for (int i = 0; i < counter; i++)
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

            pdfGrid.Draw(page, new RectangleF((float)(left), top + 30, 590, 100));
        }
    }
}
