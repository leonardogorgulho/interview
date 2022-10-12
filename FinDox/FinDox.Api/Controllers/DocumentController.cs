using FinDox.Application.Commands;
using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string? description, IFormFile file)
        {
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            var bytes = reader.ReadBytes(Convert.ToInt32(file.Length));

            var result = await _mediator.Send(new AddDocumentCommand(new()
            {
                PostedDate = DateTime.Now,
                Name = file.FileName,
                Description = description,
                Category = file.ContentType?.Split('/')[0],
                ContentType = file.ContentType,
                Size = file.Length
            }, bytes));

            return Ok(result);
        }

        [HttpPost]
        [Route("GrantPermission")]
        public async Task<IActionResult> GrantPermission([FromBody] DocumentPermissionEntry entry)
        {
            var result = await _mediator.Send(new GrantDocumentPermission(entry));

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("RemovePermission")]
        public async Task<IActionResult> RemovePermission([FromBody] DocumentPermissionEntry entry)
        {
            var result = await _mediator.Send(new RemoveDocumentPermission(entry));

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetDocumentQuery(id, false));

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/Download")]
        public async Task<IActionResult> GetDownload(int id)
        {
            var result = await _mediator.Send(new GetDocumentQuery(id, true));
            var stream = new MemoryStream(result.Content);

            return File(stream, result.ContentType, result.Name);
        }
    }
}
