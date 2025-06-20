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

        public CompressController(IWebHostEnvironment env, PdfCompressor compressor)
        {
            _env = env;
            _compressor = compressor;
        }

        [HttpPost("compress-all")]
        public IActionResult CompressAll()
        {
            var inputDir = Path.Combine(_env.WebRootPath, "input");
            var outputDir = Path.Combine(_env.WebRootPath, "output");

            if (!Directory.Exists(inputDir))
                return NotFound("Input folder not found.");

            Directory.CreateDirectory(outputDir);

            var inputFiles = Directory.GetFiles(inputDir, "*.pdf");

            foreach (var file in inputFiles)
            {
                var fileName = Path.GetFileName(file);
                var outputPath = Path.Combine(outputDir, fileName);

                try
                {
                    _compressor.CompressPdf(file, outputPath);
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
