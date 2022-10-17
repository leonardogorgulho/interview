using FinDox.Application.Commands;
using FinDox.Application.Constants;
using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    public class GroupController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<GroupRequest> _groupRequestValidator;

        public GroupController(IMediator mediator, IValidator<GroupRequest> groupRequestValidator)
        {
            _mediator = mediator;
            _groupRequestValidator = groupRequestValidator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await _mediator.Send(new GetGroupQuery(id));

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpGet]
        [Route("{id}/documents")]
        public async Task<IActionResult> GetDocuments(int id)
        {
            var permissions = await _mediator.Send(new GetGroupDocumentsQuery(id));

            if (permissions == null)
            {
                return NotFound();
            }

            return Ok(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupRequest groupRequest)
        {
            var validation = await _groupRequestValidator.ValidateAsync(groupRequest);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var result = await _mediator.Send(new SaveGroupCommand(groupRequest));
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Created($"{Request.GetDisplayUrl()}/{result.Data.GroupId}", result.Data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GroupRequest groupRequest)
        {
            var validation = await _groupRequestValidator.ValidateAsync(groupRequest);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var result = await _mediator.Send(new SaveGroupCommand(groupRequest, id));
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _mediator.Send(new RemoveGroupCommand(id));

            if (!removed)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        [Route("AddUserToGroup")]
        public async Task<IActionResult> AddUser([FromBody] UserGroup userGroup)
        {
            var result = await _mediator.Send(new AddUserToGroupCommand(userGroup));

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            if (!result.Data)
            {
                return BadRequest($"User {userGroup.UserId} is already attached to group {userGroup.GroupId}");
            }

            return Ok(userGroup);
        }

        [HttpDelete]
        [Route("{groupId}/user/{userId}")]
        public async Task<IActionResult> RemoveUser(int groupId, int userId)
        {
            var isRemoved = await _mediator.Send(new RemoveUserFromGroupCommand(new() { GroupId = groupId, UserId = userId }));

            if (!isRemoved)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        [Route("{groupId}/users")]
        public async Task<IActionResult> GetUsersFromGroup(int groupId)
        {
            var groupWithUsers = await _mediator.Send(new GetUsersFromGroupQuery(groupId));

            if (groupWithUsers == null)
            {
                return NotFound();
            }

            return Ok(groupWithUsers);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetGroups(string name, int skip = 0, int take = 20)
        {
            var groups = await _mediator.Send(new GetGroupsQuery(name, skip, take));

            if (groups == null)
            {
                return NotFound();
            }

            return Ok(groups);
        }
    }
}
