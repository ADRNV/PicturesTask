﻿using MediatR;
using PicturesTask.Core.Repositories;
using PicturesTask.Models;

namespace PicturesTask.Features.Images
{
    public class GetImage
    {
        public record Command(Guid Id, string UserName) : IRequest<byte[]>;

        public class Handler : IRequestHandler<Command, byte[]>
        {
            private readonly IImagesRepository _imagesRepository;

            public Handler(IImagesRepository imagesRepository)
            {
                _imagesRepository = imagesRepository;
            }

            public async Task<byte[]> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _imagesRepository.Get(request.Id.ToString(), request.UserName);
                }
                catch
                {
                    throw new RestException(System.Net.HttpStatusCode.Forbidden, "You not have access to this image");
                }
            }
        }
    }
}
