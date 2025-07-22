using Microsoft.AspNetCore.Mvc;
using SensitiveWordsAPI.Models;
using SensitiveWordsAPI.Services;

namespace SensitiveWordsAPI.Controllers
{
    /// <summary>
    /// Controller responsible for sanitizing messages using sensitive words list.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SanitizeController : ControllerBase
    {
        private readonly ISensitiveWordsService _service;

        public SanitizeController(ISensitiveWordsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Receives a raw message and returns a sanitized version where sensitive words are replaced with asterisks.
        /// </summary>
        /// <param name="request">Message payload to be sanitized</param>
        /// <returns>Sanitized message</returns>
        [HttpPost("sanitize")]
        public async Task<IActionResult> Sanitize([FromBody] SanitizeRequest request)
        {
            var result = await _service.SanitizeMessageAsync(request.Message);
            return Ok(new SanitizeResponse { SanitizedMessage = result });
        }
    }
}