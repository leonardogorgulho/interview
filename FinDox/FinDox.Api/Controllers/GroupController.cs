using FinDox.Application.Commands;
using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GroupController : Controller
    {
        IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupRequest groupRequest)
        {
            var group = await _mediator.Send(new SaveGroupCommand(groupRequest));

            if (group == null)
            {
                return BadRequest();
            }

            return Created($"{Request.GetDisplayUrl()}/{group.GroupId}", group);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GroupRequest groupRequest)
        {
            var group = await _mediator.Send(new SaveGroupCommand(groupRequest, id));

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
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
            var isAdded = await _mediator.Send(new AddUserToGroupCommand(userGroup));

            if (!isAdded)
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
    }
}
