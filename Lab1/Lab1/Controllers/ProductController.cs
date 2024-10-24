using Microsoft.AspNetCore.Mvc;
using Lab1.Data;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using univ_websystems_test.Data;

namespace Lab1.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            this._db = db;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            if (id <= 0)
                return BadRequest();

            var product = await _db.Products.FirstOrDefaultAsync(a => a.Id == id);
            //var product = ProductStore.Products.FirstOrDefault(a => a.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}
