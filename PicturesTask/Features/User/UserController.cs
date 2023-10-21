using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PicturesTask.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/new")]
        [AllowAnonymous]
        public async Task<IdentityResult> CreateNew([FromBody] CoreUser user)
        {
            return await _mediator.Send(new CreateNew.Command(user));
        }

        [HttpPost("/sign")]
        [AllowAnonymous]
        public async Task<HttpStatusCode> SignIn(string login, string password)
        {
            return await _mediator.Send(new SignIn.Command(login, password));
        }

        [HttpPost("/dropinvite/to/{to}")]
        public async Task<CoreUser> DropInvite([FromRoute] string to)
        {
            var fromUser = this.User.Identity.Name;

            return await _mediator.Send(new DropInvite.Command(fromUser, to));
        }

        [HttpGet("invites")]
        public async Task<IEnumerable<CoreInvation>> GetUserInvation([FromQuery] int page, [FromQuery] int size)
        {
            var userName = this.User.Identity.Name;

            return await _mediator.Send(new GetInvations.Command(userName, page, size));
        }

        [HttpPut("invites/handle/{id}")]
        public async Task<CoreInvation> Handle([FromRoute] Guid id, [FromQuery] bool accept)
        {
            var userName = this.User.Identity.Name;

            return await _mediator.Send(new HandleInvion.Command(userName, id, accept));
        }
    }
}
