using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace PicturesTask.Features.Images
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        public readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{id}")]
        public async Task<CoreImage> GetImage([FromRoute]Guid id, string userName)
        {
            return await _mediator.Send(new GetImage.Command(id, userName));
        }

        [HttpPost("create")]
        public async Task<Guid> CreateImage(IFormFile imageFile)
        {
            var userName = this.User.Identity.Name;

            return await _mediator.Send(new CreateImage.Command(userName, imageFile));
        }
    }
}
