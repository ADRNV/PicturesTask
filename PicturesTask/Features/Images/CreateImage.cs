using MediatR;
using PicturesTask.Core.Repositories;

namespace PicturesTask.Features.Images
{
    public class CreateImage
    {
        public record Command(CoreImage Image, string UserName) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IImagesRepository _imagesRepository;

            public Handler(IImagesRepository imagesRepository)
            {
                _imagesRepository = imagesRepository;
            }

            public Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                return _imagesRepository.Create(request.Image, request.UserName);
            }
        }
    }
}
