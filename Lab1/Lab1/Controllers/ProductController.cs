using Microsoft.AspNetCore.Mvc;
using Lab1.Data;
using Lab1.Models;

namespace Lab1.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public Product GetProduct(int id)
        {
            return ProductStore.Products.FirstOrDefault(a => a.Id == id);
        }
    }
}
