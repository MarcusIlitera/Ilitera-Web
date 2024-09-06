using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace Ilitera.Common
{
    public abstract class PdfSharpUtility
    {
        #region MergeReports Stream[] streams,

        public static MemoryStream MergeReports(Stream[] streams, 
                                                string fileName, 
                                                string watermark, 
                                                bool RenumerarPaginas)
        {
            // Open the output document
            using (PdfDocument outputDocument = new PdfDocument())
            {
                // Iterate files
                foreach (Stream stream in streams)
                {
                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = inputDocument.PageCount;

                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                }

                if (RenumerarPaginas || watermark != string.Empty)
                {
                    // Set version to PDF 1.4 (Acrobat 5) because we use transparency.
                    if (watermark != string.Empty && outputDocument.Version < 14)
                        outputDocument.Version = 14;

                    int number = 1;

                    foreach (PdfPage page in outputDocument.Pages)
                    {
                        XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

                        if (RenumerarPaginas)
                            AddPageNumber(gfx, page, number++, outputDocument.PageCount);

                        if (watermark != string.Empty)
                            AddWaterMark(gfx, page, watermark);
                    }
                }

                AssinarPdf(outputDocument);

                //Ilitera: 11/10/2010 - Código comentado até verificar onde é populada essa propriedade
                //if (Ilitera.Data.Table.IsWeb)
                //{
                    MemoryStream ret = new MemoryStream();
                    outputDocument.Save(ret, false);
                    outputDocument.Close();
                    return ret;
                //}
                //else
                //{
                    //outputDocument.Save(fileName);
                    //outputDocument.Close();
                    //return null;
                //}
            }
        }
        #endregion

        #region MergeReports string[] paths

        public static MemoryStream MergeReports(string[] paths, 
                                                string fileName, 
                                                string watermark, 
                                                bool RenumerarPaginas)
        {
            // Open the output document
            using (PdfDocument outputDocument = new PdfDocument())
            {
                // Iterate files
                foreach (string path in paths)
                {
                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(path, PdfDocumentOpenMode.Import);

                    // Iterate pages
                    int count = inputDocument.PageCount;

                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                }

                if (RenumerarPaginas || watermark != string.Empty)
                {
                    // Set version to PDF 1.4 (Acrobat 5) because we use transparency.
                    if (watermark != string.Empty && outputDocument.Version < 14)
                        outputDocument.Version = 14;

                    int number = 1;

                    foreach (PdfPage page in outputDocument.Pages)
                    {
                        XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

                        if (RenumerarPaginas)
                            AddPageNumber(gfx, page, number++, outputDocument.PageCount);

                        if (watermark != string.Empty)
                            AddWaterMark(gfx, page, watermark);
                    }
                }

                AssinarPdf(outputDocument);

                if (Ilitera.Data.Table.IsWeb)
                {
                    MemoryStream ret = new MemoryStream();
                    outputDocument.Save(ret, false);
                    outputDocument.Close();
                    return ret;
                }
                else
                {
                    outputDocument.Save(fileName);
                    outputDocument.Close();
                    return null;
                }
            }
        }
        #endregion

        #region AddPageNumber

        private static void AddPageNumber(XGraphics gfx, PdfPage page, int numberPage, int totalPage)
        {
            string pageNumber = "Página " + numberPage.ToString() + " de " + totalPage.ToString();

            XFont font = new XFont("Verdana", 7, XFontStyle.Regular);

            XBrush brush = new XSolidBrush(XColor.FromArgb(0, 64, 0));

            //XRect rect = new XRect(0, 0, page.Width - 10, page.Height - 10);
            XRect rect = new XRect(0, 0, page.Width - 35, page.Height - 10);

            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Far;
            format.LineAlignment = XLineAlignment.Far;

            gfx.DrawString(pageNumber, font, brush, rect, format);
        }
        #endregion

        #region AddWaterMark

        #region AddWaterMark(string path, string watermark

        public static MemoryStream AddWaterMark(string path, string watermark, bool SomenteNaPrimeiraPagina)
        {
           // Open the output document
            using (PdfDocument outputDocument = new PdfDocument())
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(path, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;

                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }

                foreach (PdfPage page in outputDocument.Pages)
                {
                    XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

                    if (watermark != string.Empty)
                        AddWaterMark(gfx, page, watermark);

                    if (SomenteNaPrimeiraPagina)
                        break;
                }

                if (Ilitera.Data.Table.IsWeb)
                {
                    MemoryStream stream = new MemoryStream();
                    outputDocument.Save(stream, false);
                    outputDocument.Close();
                    return stream;
                }
                else
                {
                    outputDocument.Save(path);
                    outputDocument.Close();
                    return null;
                }
            }
        }
        #endregion

        #region AddWaterMark(XGraphics gfx, PdfPage page, string watermark)

        private static void AddWaterMark(XGraphics gfx, PdfPage page, string watermark)
        {
            char[] seps = { '\n' };

            string[] watermarks = watermark.Split(seps);

            if (watermarks.Length == 3)
                Add3WaterMark(gfx, page, watermarks);
            else
                AddWaterMark1(gfx, page, watermark);
        }
        #endregion

        #region Add3WaterMark

        private static void Add3WaterMark(XGraphics gfx, PdfPage page, string[] watermarks)
        {
            // Create the font for drawing the watermark
            XFont font = new XFont("Arial", 18, XFontStyle.Bold);

            // Define a rotation transformation at the center of the page
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

            // Create a string format
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;

            // Create a dimmed red brush
            XBrush brush = new XSolidBrush(XColor.FromArgb(128, 255, 0, 0));

            int i = 1;

            foreach (string watermark in watermarks)
            {
                // Get the size (in point) of the text
                XSize size = gfx.MeasureString(watermark, font);

                // Create a point 
                XPoint point;
                    
                if(i == 1)
                    point = new XPoint((page.Width - size.Width) / 2 , 
                                        ((page.Height - 200) - size.Height) / 2);
                else if (i == 3)
                    point = new XPoint((page.Width - size.Width) / 2,
                                        ((page.Height + 200) - size.Height) / 2);
                else
                    point = new XPoint((page.Width - size.Width) / 2,
                                        ((page.Height) - size.Height) / 2);

                gfx.DrawString(watermark, font, brush, point, format);

                i++;
            }
        }
        #endregion

        #region AddWaterMark1

        private static void AddWaterMark1(XGraphics gfx, PdfPage page, string watermark)
        {
            // Variation 1: Draw watermark as text string

            // Create the font for drawing the watermark
            XFont font = new XFont("Arial", 24, XFontStyle.Bold);

            // Get the size (in point) of the text
            XSize size = gfx.MeasureString(watermark, font);

            // Define a rotation transformation at the center of the page
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

            // Create a string format
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;

            // Create a dimmed red brush
            XBrush brush = new XSolidBrush(XColor.FromArgb(128, 255, 0, 0));

            // Create a point 
            XPoint point = new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2);

            // Draw the string
            gfx.DrawString(watermark, font, brush, point, format);
        }
        #endregion

        #region AddWaterMark2

        private static void AddWaterMark2(XGraphics gfx, PdfPage page, string watermark)
        {
            // Variation 2: Draw watermark as outlined graphical path

            // Create the font for drawing the watermark
            XFont font = new XFont("Arial", 48, XFontStyle.Regular);

            // Get the size (in point) of the text
            XSize size = gfx.MeasureString(watermark, font);

            // Define a rotation transformation at the center of the page
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

            // Create a graphical path
            XGraphicsPath path = new XGraphicsPath();

            // Add the text to the path
            path.AddString(watermark, font.FontFamily, XFontStyle.BoldItalic, font.Size,
              new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
              XStringFormats.Default);

            // Create a dimmed red pen
            XPen pen = new XPen(XColor.FromArgb(128, 255, 0, 0), 2);

            // Stroke the outline of the path
            gfx.DrawPath(pen, path);
        }
        #endregion

        #region AddWaterMark3

        private static void AddWaterMark3(XGraphics gfx, PdfPage page, string watermark)
        {
            // Variation 3: Draw watermark as transparent graphical path above text

            // Create the font for drawing the watermark
            XFont font = new XFont("Verdana", 48, XFontStyle.Regular);

            // Get the size (in point) of the text
            XSize size = gfx.MeasureString(watermark, font);

            // Define a rotation transformation at the center of the page
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

            // Create a graphical path
            XGraphicsPath path = new XGraphicsPath();

            // Add the text to the path
            path.AddString(watermark, font.FontFamily, XFontStyle.BoldItalic, font.Size,
              new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
              XStringFormats.Default);

            // Create a dimmed red pen and brush
            XPen pen = new XPen(XColor.FromArgb(50, 75, 0, 130), 3);
            XBrush brush = new XSolidBrush(XColor.FromArgb(50, 106, 90, 205));

            // Stroke the outline of the path
            gfx.DrawPath(pen, brush, path);
        }
        #endregion

        #endregion

        #region AssinarPdf

        private static void AssinarPdf(PdfDocument document)
        {
            document.Info.Title = "";
            document.Info.Author = "";
            document.Info.Subject = "";
            document.Info.Keywords = "";
        }
        #endregion

        #region ToWord

        private static void ToWord(PdfDocument document, string path)
        {

        }
        #endregion
    }
}
