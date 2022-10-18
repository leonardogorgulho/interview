using FinDox.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinDox.Api.Controllers
{
    public class FinDoxDocumentSecurityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public FinDoxDocumentSecurityController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        protected int GetUserId()
        {
            return Convert.ToInt32(User.Claims.First(i => i.Type == "UserId").Value);
        }

        protected async Task<bool> CanLoggedUserHaveAccess(int documentId)
        {
            var adminHasAccess = _configuration.GetValue<bool>("AdminHasAccessToAnyFile") &&
                string.Equals(User.Claims.First(i => i.Type == ClaimTypes.Role).Value.ToUpper(), "A", StringComparison.InvariantCultureIgnoreCase);

            return adminHasAccess || await _mediator.Send(new CanUserDownloadDocumentQuery(GetUserId(), documentId));
        }
    }
}
