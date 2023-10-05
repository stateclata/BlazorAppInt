using BlazorApp.Server.Services.BingService;
using BlazorApp.Shared;
using BlazorApp.Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BingController : ControllerBase
    {
        private readonly IBingService _bingService;

        public BingController(IBingService bingService)
        {
            _bingService = bingService;
        }


        [HttpPost("create"), Authorize]
        public async Task<ActionResult<ServiceResponse<BingMapResponse>>> SearchBingAsync([FromBody] string query)
        {
            var response = await _bingService.SearchBing(query);
            return Ok(response);
        }
    }
}