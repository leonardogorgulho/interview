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
    public class DocumentController : FinDoxDocumentSecurityController
    {
        IMediator _mediator;

        public DocumentController(IMediator mediator) : base(mediator)
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

        [HttpPut]
        [Route("{id}/GrantPermission")]
        public async Task<IActionResult> GrantPermission(int id, [FromBody] UsersAndGroupsIds entry)
        {
            var result = await _mediator.Send(new GrantDocumentPermission(new()
            {
                DocumentId = id,
                UserIds = entry.UserIds,
                GroupIds = entry.GroupIds
            }));

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}/RemovePermission")]
        public async Task<IActionResult> RemovePermission(int id, [FromBody] UsersAndGroupsIds entry)
        {
            var result = await _mediator.Send(new RemoveDocumentPermission(new()
            {
                DocumentId = id,
                UserIds = entry.UserIds,
                GroupIds = entry.GroupIds
            }));

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/Permissions")]
        public async Task<IActionResult> GetPermissions(int id)
        {
            var docs = await _mediator.Send(new GetDocumentPermissionQuery(id));

            return Ok(docs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!await CanLoggedUserDownload(id))
            {
                return Unauthorized("Current logged user does not have access to this document.");
            }

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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveDocumentCommand(id));

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
