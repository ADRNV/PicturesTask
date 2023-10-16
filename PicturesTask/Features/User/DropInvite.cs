using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PicturesTask.Core.Repositories;
using PicturesTask.Infrastructure.Repositories;
using PicturesTask.Models;
using System.Net;

namespace PicturesTask.Features.User
{
    public class DropInvite
    {
        public record Command(string From, string To) : IRequest<CoreUser>;

        public class Handler : IRequestHandler<Command, CoreUser>
        {
            private readonly InvitionsRepository _invationsRepository;

            private readonly UserManager<EntityUser> _userManager;

            private readonly IMapper _mapper;

            public Handler(InvitionsRepository repository, UserManager<EntityUser> userManager, IMapper mapper)
            {
                _invationsRepository = repository;

                _userManager = userManager;

                _mapper = mapper;
            }

            public async Task<CoreUser> Handle(Command request, CancellationToken cancellationToken)
            {
                var toUser = await _userManager.FindByNameAsync(request.To);

                if (toUser == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                else
                {
                    var fromUser = await _userManager.FindByNameAsync(request.From);

                    await _invationsRepository.Create(fromUser, toUser);

                    return _mapper.Map<CoreUser>(toUser);
                }
            }
        }
    }
}
