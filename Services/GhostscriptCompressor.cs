using System.Diagnostics;

namespace PdfCompressorAPI.Services
{
    public class GhostscriptCompressor
    {
        public void CompressPdf(string inputPath, string outputPath)
        {
            var gsPath = "gswin64c"; // Or "gs" on Unix/Linux

            var args = $"-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/screen " +
                       $"-dNOPAUSE -dQUIET -dBATCH -sOutputFile=\"{outputPath}\" \"{inputPath}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = gsPath,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var process = Process.Start(startInfo);
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception("Ghostscript compression failed.");
            }
        }
    }
}
