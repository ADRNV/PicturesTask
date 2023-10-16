using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PicturesTask.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/new")]
        public async Task<IdentityResult> CreateNew([FromBody]CoreUser user)
        {
            return await _mediator.Send(new CreateNew.Command(user));
        }

        [HttpPost("/sign")]
        public async Task<HttpStatusCode> SignIn(string login, string password)
        {
            return await _mediator.Send(new SignIn.Command(login, password));
        }
    }
}
