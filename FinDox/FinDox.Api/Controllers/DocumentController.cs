using FinDox.Application.Commands;
using FinDox.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(DateTime? postedData, string? name, string? description, string? category, IFormFile file)
        {
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            var bytes = reader.ReadBytes(Convert.ToInt32(file.Length));

            var result = await _mediator.Send(new AddDocumentCommand(new()
            {
                PostedDate = postedData,
                Name = name,
                Description = description,
                Category = category
            }, bytes));

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetDocumentQuery(id));
            var stream = new MemoryStream(result.Content);

            return File(stream, "application/pdf", "test.pdf");
        }
    }
}
