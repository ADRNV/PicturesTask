using MediatR;
using Microsoft.AspNetCore.Identity;

namespace PicturesTask.Features.User
{
    public class SignOut
    {
        public record Command() : IRequest<Unit>;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly SignInManager<EntityUser> _signinManager;

            public Handler(SignInManager<EntityUser> signinManager)
            {

                _signinManager = signinManager;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _signinManager.SignOutAsync();

                return await Unit.Task;
            }
        }
    }
}
