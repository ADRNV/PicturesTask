using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PicturesTask.Features.Images
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class ImagesController : ControllerBase
    {
        public readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] Guid id, string userName)
        {
            var image = await _mediator.Send(new GetImage.Command(id, userName));

            return File(image, "image/jpeg");
        }

        [HttpPost("create")]
        public async Task<Guid> CreateImage(IFormFile imageFile)
        {
            var userName = this.User.Identity.Name;

            return await _mediator.Send(new CreateImage.Command(userName, imageFile));
        }
    }
}
