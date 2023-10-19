using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<Guid> CreateImage([FromBody]CoreImage image,[FromQuery]string userName)
        {
            return await _mediator.Send(new CreateImage.Command(image, userName));
        }
    }
}
