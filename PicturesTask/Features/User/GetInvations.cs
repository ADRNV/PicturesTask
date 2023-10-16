using MediatR;
using PicturesTask.Infrastructure.Repositories;

namespace PicturesTask.Features.User
{
    public class GetInvations
    {
        public record Command(string UserName, int Page, int Size) : IRequest<IEnumerable<CoreInvation>>;

        public class Handler : IRequestHandler<Command, IEnumerable<CoreInvation>>
        {
            private readonly InvitionsRepository _invitionsRepository;

            public Handler(InvitionsRepository invitionsRepository)
            {
                _invitionsRepository = invitionsRepository;
            }

            public Task<IEnumerable<CoreInvation>> Handle(Command request, CancellationToken cancellationToken)
            {
               return _invitionsRepository.Get(request.UserName, request.Page, request.Size);
            }
        }


    }
}
