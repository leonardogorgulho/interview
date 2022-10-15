using FinDox.Application.Queries;
using FinDox.Domain.DataTransfer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace FinDox.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<LoginRequest> _validator;

        public AccountController(IMediator mediator, IValidator<LoginRequest> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost]
        [Route("ConvertPassword")]
        public string ConvertPassword([FromBody] string password)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(password);

            using var md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(bytes);

            return Convert.ToHexString(hashBytes);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var validation = await _validator.ValidateAsync(login);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var user = await _mediator.Send(new LoginQuery(login));

            if (user == null)
            {
                return NotFound("User or Password is incorrect");
            }

            return Ok(user);
        }
    }
}
