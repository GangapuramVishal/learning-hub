using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a file.");

            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var result = await _blobService.UploadFileBlobAsync(filePath, file.FileName);
            return Ok(new { path = result });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            var stream = await _blobService.GetBlobAsync(fileName);

            if (stream == null)
                return NotFound();

            return File(stream, "application/octet-stream", fileName);
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> Delete(string fileName)
        {
            await _blobService.DeleteBlobAsync(fileName);
            return Ok();
        }
    }
}
