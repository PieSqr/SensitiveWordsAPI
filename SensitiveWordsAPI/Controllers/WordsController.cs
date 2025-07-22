using Microsoft.AspNetCore.Mvc;
using SensitiveWordsAPI.Models;
using SensitiveWordsAPI.Services;

namespace SensitiveWordsAPI.Controllers
{

    /// <summary>
    /// Provides CRUD operations for managing sensitive words in the database.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly ISensitiveWordsService _service;

        public WordsController(ISensitiveWordsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllWordsAsync());

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SensitiveWord word) => Ok(await _service.AddWordAsync(word.Word));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SensitiveWord word) => Ok(await _service.UpdateWordAsync(id, word.Word));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteWordAsync(id));
    }
}