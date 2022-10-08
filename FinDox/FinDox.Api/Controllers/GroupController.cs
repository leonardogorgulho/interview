using FinDox.Application.Commands;
using FinDox.Application.Queries;
using FinDox.Domain.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    [ApiController]
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

            return Ok(group);
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
    }
}
