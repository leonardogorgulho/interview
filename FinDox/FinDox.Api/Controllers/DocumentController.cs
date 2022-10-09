using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        [HttpPost("Upload")]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            string filePath = @$"D:\interviewFiles\{file.FileName}";
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);

                FileAttributes attributes = System.IO.File.GetAttributes(filePath);
            }

            return Ok();
        }



        [HttpGet]
        [Route("Download/{url}")]
        public async Task<IActionResult> Download(string url)
        {
            var bytes = await System.IO.File.ReadAllBytesAsync(url);
            string contentType;

            if (new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(url), out contentType))
            {
                return File(bytes, contentType, Path.GetFileName(url));
            };

            return File(bytes, "text/plain", Path.GetFileName(url));
        }
    }
}
