using MediatR;
using Microsoft.AspNetCore.Mvc;
using SEOAutoWebApi.Features;

namespace SEOAutoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchRequest.Command request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("get-support-browsers")]
        public async Task<IActionResult> GetSupportBrowsersAsync([FromQuery] GetSupportBrowsersRequest.Command request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
