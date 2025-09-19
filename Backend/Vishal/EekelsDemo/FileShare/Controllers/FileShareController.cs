using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileShareController : ControllerBase
    {
        private readonly IFileShareService _fileShareService;

        public FileShareController(IFileShareService fileShareService)
        {
            _fileShareService = fileShareService;
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            var fileContent = await _fileShareService.DownloadFileAsync(fileName);

            if (fileContent == null)
                return NotFound();

            return Ok(fileContent);
        }
    }
}
