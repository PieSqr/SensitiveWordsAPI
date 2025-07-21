using Microsoft.AspNetCore.Mvc;
using SensitiveWordsAPI.Models;
using SensitiveWordsAPI.Services;

namespace SensitiveWordsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanitizeController : ControllerBase
    {
        private readonly ISensitiveWordsService _service;

        public SanitizeController(ISensitiveWordsService service)
        {
            _service = service;
        }

        [HttpPost("sanitize")]
        public async Task<IActionResult> Sanitize([FromBody] SanitizeRequest request)
        {
            var result = await _service.SanitizeMessageAsync(request.Message);
            return Ok(new SanitizeResponse { SanitizedMessage = result });
        }
    }
}