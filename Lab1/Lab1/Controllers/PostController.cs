using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Lab1.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            if (id <= 0)
                return BadRequest();

            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var post = await response.Content.ReadFromJsonAsync<Post>();

            if (post == null)
                return NotFound();

            return Ok(post);
        }
    }
}
