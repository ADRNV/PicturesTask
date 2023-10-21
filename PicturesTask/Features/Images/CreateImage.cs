using MediatR;
using PicturesTask.Core.Repositories;

namespace PicturesTask.Features.Images
{
    public class CreateImage
    {
        public record Command(string UserName, IFormFile ImageFile) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IImagesRepository _imagesRepository;

            public Handler(IImagesRepository imagesRepository)
            {
                _imagesRepository = imagesRepository;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                using var stream = new MemoryStream();

                await request.ImageFile.CopyToAsync(stream);

                return await _imagesRepository.Create(request.UserName, stream);
            }
        }
    }
}
