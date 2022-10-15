using FinDox.Application.Commands;
using FinDox.Application.Constants;
using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.All)]
    [Route("[controller]")]
    public class DocumentController : FinDoxDocumentSecurityController
    {
        IMediator _mediator;

        public DocumentController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = Roles.AdminAndManager)]
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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
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
                return Unauthorized("Logged user does not have access to this document.");
            }

            var result = await _mediator.Send(new GetDocumentQuery(id, false));

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/Download")]
        public async Task<IActionResult> GetDownload(int id)
        {
            if (!await CanLoggedUserDownload(id))
            {
                return Unauthorized("Logged user does not have access to this document.");
            }

            var result = await _mediator.Send(new GetDocumentQuery(id, true));
            var stream = new MemoryStream(result.Content);

            return File(stream, result.ContentType, result.Name);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin)]
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
