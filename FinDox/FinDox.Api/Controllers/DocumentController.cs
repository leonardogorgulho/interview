using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        IConfiguration _configuration;

        public DocumentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string filePath = @$"{_configuration.GetValue<string>("SharedDirectory")}\{file.FileName}";
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
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

                var file = File(bytes, contentType, Path.GetFileName(url));
                return file;
            };

            return File(bytes, "text/plain", Path.GetFileName(url));
        }
    }
}
