using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PicturesTask.Models;
using System.Net;

namespace PicturesTask.Features.User
{
    public class CreateNew
    {
        public record Command(CoreUser User) : IRequest<IdentityResult>;

        public class Handler : IRequestHandler<Command, IdentityResult>
        {
            private readonly SignInManager<EntityUser> _signInManager;

            private readonly IPasswordHasher<EntityUser> _passwordHasher;

            private readonly IMapper _mapper;

            public Handler(SignInManager<EntityUser> signInManager, IPasswordHasher<EntityUser> passwordHasher, IMapper mapper)
            {
                _signInManager = signInManager;

                _passwordHasher = passwordHasher;

                _mapper = mapper;
            }

            public async Task<IdentityResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(request.User.Name);

                var userExist = user is not null;

                if (userExist)
                {
                    throw new RestException(HttpStatusCode.Conflict, "User are exists");
                }
                else
                {
                    user = _mapper.Map<EntityUser>(request.User);   

                    return await _signInManager.UserManager.CreateAsync(user);
                }
            }
        }
    }
}
