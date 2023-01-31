using Microsoft.AspNetCore.Mvc;
using MongoExample.Models;
using MongoExample.Services;

namespace MongoExample.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class SystemsController : Controller
    {

        private readonly MongoDBService _mongoDBService;

        public SystemsController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Systems>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<Systems> GetFilterUnique(string id)
        {
            return await _mongoDBService.GetFilterUniqueAsync(id);
        }

        [HttpGet("filter")]
        public async Task<List<Systems>> GetFilter([FromQuery] string column, string value)
        {
            return await _mongoDBService.GetFilterAsync(column, value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Systems playlist)
        {
            await _mongoDBService.CreateAsync(playlist);
            return CreatedAtAction(nameof(Get), new { id = playlist.ClientId }, playlist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId)
        {
            await _mongoDBService.AddToPlaylistAsync(id, movieId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}
