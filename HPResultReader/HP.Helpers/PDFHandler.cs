using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.Pdf.Texts;
using System.IO;
using System.Drawing;

namespace HP.Helpers
{
    public class PDFHandler
    {
        public string ExtractResults(string filePath)
        {
            var doc = new PdfDocument();

            doc.LoadFromFile(filePath);

            var page = doc.Pages[0];

            var pdfExtractor = new PdfTextExtractor(page);
            string text = pdfExtractor.ExtractText(new PdfTextExtractOptions() 
            { 
                ExtractArea = new Rectangle(0,130,1000,595), //determens what part of the document to read.
                IsSimpleExtraction = false} //makes it easier to use with regex for this case.
            );

            return text;
        }
    }
}
