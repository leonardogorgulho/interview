using FinDox.Application.Commands;
using FinDox.Application.Constants;
using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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

        /// <summary>
        /// Creates a new document record with a file object in the database
        /// </summary>
        /// <param name="description"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Roles.AdminAndManager)]
        public async Task<IActionResult> Post([FromHeader] string description, IFormFile file)
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

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Created($"{Request.GetDisplayUrl()}/{result.Data.DocumentId}", result.Data);
        }

        /// <summary>
        /// Grant user and/or group permission to the document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
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

            return Ok(entry);
        }

        /// <summary>
        /// Remove the document permission from user and/or group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
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

            return Ok(entry);
        }

        /// <summary>
        /// Gets all the permissions related to the document
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/Permissions")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetPermissions(int id)
        {
            var docs = await _mediator.Send(new GetDocumentPermissionQuery(id));

            return Ok(docs);
        }

        /// <summary>
        /// Get all the document's information, less the file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Downloads the file attached to the document record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the document record along with the relation with users and document permissions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveDocumentCommand(id));

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
