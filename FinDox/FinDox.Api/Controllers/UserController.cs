using FinDox.Application.Commands;
using FinDox.Application.Constants;
using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UserEntryRequest> _entryRequestValidator;

        public UserController(IMediator mediator, IValidator<UserEntryRequest> entryRequestValidator)
        {
            _mediator = mediator;
            _entryRequestValidator = entryRequestValidator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("{id}/documents")]
        public async Task<IActionResult> GetDocuments(int id)
        {
            var permissions = await _mediator.Send(new GetUserDocumentsQuery(id));

            if (permissions == null)
            {
                return NotFound();
            }

            return Ok(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserEntryRequest userRequest)
        {
            var validation = await _entryRequestValidator.ValidateAsync(userRequest);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var result = await _mediator.Send(new SaveUserCommand(userRequest));

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserEntryRequest userRequest)
        {
            var validation = await _entryRequestValidator.ValidateAsync(userRequest);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var result = await _mediator.Send(new SaveUserCommand(userRequest, id));

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _mediator.Send(new RemoveUserCommand(id));

            if (!removed)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetUsers(string name, string login, int skip = 0, int take = 20)
        {
            var users = await _mediator.Send(new GetUsersQuery(name, login, skip, take));

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}
