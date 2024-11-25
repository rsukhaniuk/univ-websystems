using Microsoft.AspNetCore.Mvc;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using univ_websystems_test.Data;
using Microsoft.Extensions.Caching.Memory;

namespace Lab1.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public ProductController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();

            var cacheKey = $"Product_{id}";

            if (!_cache.TryGetValue(cacheKey, out Product? product))
            {
                product = ProductStore.Products.FirstOrDefault(a => a.Id == id);

                if (product == null)
                    return NotFound();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))  // TTL
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2)); // TTI

                _cache.Set(cacheKey, product, cacheEntryOptions);

                await Task.Delay(2000);
            }

            return Ok(product);
        }
    }
}
