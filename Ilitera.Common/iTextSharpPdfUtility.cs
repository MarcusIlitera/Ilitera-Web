using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.rtf;

namespace Ilitera.Common
{
    public class iTextSharpPdfUtility
    {
        public static void MergeReports(Stream[] streams, string fileName, string watermark, bool RenumerarPaginas)
        {
            int f = 0;

            // we create a reader for a certain document
            PdfReader reader = new PdfReader(streams[f]);

            // we retrieve the total number of pages
            int n = reader.NumberOfPages;

            // step 1: creation of a document-object
            Document document = new Document(reader.GetPageSizeWithRotation(1));

            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            // step 3: we open the document
            document.Open();
            
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;
            int rotation;

            // step 4: we add content
            while (f < streams.Length)
            {
                int i = 0;

                while (i < n)
                {
                    i++;
                    
                    document.SetPageSize(reader.GetPageSizeWithRotation(i));
                    
                    document.NewPage();
                    
                    page = writer.GetImportedPage(reader, i);
                    
                    rotation = reader.GetPageRotation(i);
                   
                    if (rotation == 90 || rotation == 270)
                        cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                    else
                        cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                }
                f++;
                if (f < streams.Length)
                {
                    reader = new PdfReader(streams[f]);
                    // we retrieve the total number of pages
                    n = reader.NumberOfPages;
                }
            }

            if(RenumerarPaginas)
                AddPageNumber(document);

            // step 5: we close the document
            document.Close();
        }

        private static void AddWaterMark(Document document)
        {
            //Watermark watermark = new Watermark(Image.GetInstance("watermark.jpg"), 200, 320);
            //document.Add(watermark);
        }

        private static void AddPageNumber(Document document)
        {
            Font font = FontFactory.GetFont("");

            HeaderFooter footer = new HeaderFooter(new Phrase("This is page: "), true);
            footer.Border = Rectangle.NO_BORDER;
            document.Footer = footer;
        }

        private static void ToWord(Document document, string path)
        {
            //RtfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
        }

        //public static string Parse(string fileName)
        //{
        //    PdfReader reader = new PdfReader(fileName);
        //    reader.GetPageContent(
        //}
    }
}
