using Microsoft.AspNetCore.Mvc;
using PdfCompressorAPI.Services;

namespace PdfCompressorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompressController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly PdfCompressor _compressor;
         private readonly GhostscriptCompressor _gsCompressor;

        public CompressController(IWebHostEnvironment env, PdfCompressor compressor, GhostscriptCompressor gsCompressor)
        {
            _env = env;
            _compressor = compressor;
            _gsCompressor = gsCompressor;
        }


        [HttpPost("compress-all")]
        public IActionResult CompressAll()
        {
            var inputDir = Path.Combine(_env.WebRootPath, "input");
            var outputDir = Path.Combine(_env.WebRootPath, "output");
            var outputMaxDir = Path.Combine(_env.WebRootPath, "output_max");


            if (!Directory.Exists(inputDir))
                return NotFound("Input folder not found.");

            Directory.CreateDirectory(outputDir);
            Directory.CreateDirectory(outputMaxDir);


            var inputFiles = Directory.GetFiles(inputDir, "*.pdf");
            

            foreach (var file in inputFiles)
            {
                var fileName = Path.GetFileName(file);
                var outputPath = Path.Combine(outputDir, 
                Path.GetFileNameWithoutExtension(fileName) + "_compressed" + Path.GetExtension(fileName));
                var maxOutputPath = Path.Combine(outputMaxDir, 
                Path.GetFileNameWithoutExtension(fileName) + "_compressed" + Path.GetExtension(fileName));

                try
                {
                    _compressor.CompressPdf(file, outputPath);
                    _gsCompressor.CompressPdf(file, maxOutputPath);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error compressing {fileName}: {ex.Message}");
                }
            }

            return Ok("Compression completed.");
        }
    }
}
