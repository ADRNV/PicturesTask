using AutoMapper;
using MediatR;
using PicturesTask.Core.Models;
using PicturesTask.Core.Repositories;
using PicturesTask.Infrastructure.Repositories;
using PicturesTask.Models;
using System.Net;

namespace PicturesTask.Features.User
{
    public class HandleInvion
    {
        public record Command(string UserName, Guid Id, bool Accept) : IRequest<CoreInvation>;

        public class Handler : IRequestHandler<Command, CoreInvation>
        {
            private readonly InvitionsRepository _invitesRepository;

            private readonly IRepository<Friend> _friendsRepository;

            private readonly IMapper _mapper;

            public Handler(InvitionsRepository invitionsRepository, IRepository<Friend> friendsRepository, IMapper mapper)
            {
                _invitesRepository = invitionsRepository;

                _friendsRepository = friendsRepository;

                _mapper = mapper;
            }

            public async Task<CoreInvation> Handle(Command request, CancellationToken cancellationToken)
            {
                var invite = await _invitesRepository.Get(request.UserName, request.Id.ToString()) ?? throw new RestException(HttpStatusCode.NotFound);
                
                invite.Accepted = request.Accept;

                await _invitesRepository.Update(invite);

                if(invite.Accepted)
                {
                    await _friendsRepository.Create(new Friend {
                        User1 = invite.From,
                        User2 = invite.To,
                    });   
                }

                return invite;
            }
        }

    }
}
