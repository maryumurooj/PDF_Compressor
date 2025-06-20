using iTextSharp.text.pdf;
using System.IO;

namespace PdfCompressorAPI.Services
{
    public class PdfCompressor
    {
        public void CompressPdf(string inputPath, string outputPath)
        {
            using var reader = new PdfReader(inputPath);
            using var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
            using var stamper = new PdfStamper(reader, fs, PdfWriter.VERSION_1_5);
            
            stamper.Writer.SetFullCompression();
            stamper.FormFlattening = true;
            stamper.Writer.CompressionLevel = PdfStream.BEST_COMPRESSION;

            for (int i = 1; i <= reader.NumberOfPages; i++)
                reader.SetPageContent(i, reader.GetPageContent(i));

            stamper.Close();
            reader.Close();
        }
    }
}
