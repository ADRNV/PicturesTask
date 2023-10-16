using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PicturesTask.Models;
using System.Net;

namespace PicturesTask.Features.User
{
    public class SignIn
    {
        public record Command(string Login, string Password) : IRequest<HttpStatusCode>;

        public class Handler : IRequestHandler<Command, HttpStatusCode>
        {
            private readonly SignInManager<EntityUser> _signInManager;

            private readonly IMapper _mapper;

            public Handler(SignInManager<EntityUser> userManager)
            {
                _signInManager = userManager;
            }

            public async Task<HttpStatusCode> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(request.Login);

                var userExists = user is not null;

                if(userExists)
                {
                    await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

                    return HttpStatusCode.OK;
                }
                else
                {
                    throw new RestException(HttpStatusCode.NotFound, "User not found");
                }
            }
        }
    }
}
