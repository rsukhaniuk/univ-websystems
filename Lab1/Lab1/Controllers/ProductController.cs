using Microsoft.AspNetCore.Mvc;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using univ_websystems_test.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Lab1.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public ProductController(IDistributedCache cache)
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

            string cacheKey = $"Product_{id}";

            string cachedProduct = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProduct))
            {
                var productFromCache = JsonSerializer.Deserialize<Product>(cachedProduct);
                return Ok(productFromCache);
            }

            await Task.Delay(2000);

            var product = ProductStore.Products.FirstOrDefault(a => a.Id == id);

            if (product == null)
                return NotFound();

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            string serializedProduct = JsonSerializer.Serialize(product);
            await _cache.SetStringAsync(cacheKey, serializedProduct, cacheOptions);

            return Ok(product);
        }

        [HttpGet("StaticImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImage()
        {
            await Task.Delay(5000);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image.png");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);


            return File(fileBytes, "image/png"); 
        }
    }
}
