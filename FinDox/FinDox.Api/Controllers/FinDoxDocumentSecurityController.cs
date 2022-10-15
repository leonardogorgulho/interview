using FinDox.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinDox.Api.Controllers
{
    public class FinDoxDocumentSecurityController : Controller
    {
        private readonly IMediator _mediator;
        public FinDoxDocumentSecurityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected int GetUserId()
        {
            return Convert.ToInt32(User.Claims.First(i => i.Type == "UserId").Value);
        }

        protected async Task<bool> CanLoggedUserDownload(int documentId)
        {
            return await _mediator.Send(new CanUserDownloadDocumentQuery(GetUserId(), documentId));
        }
    }
}
