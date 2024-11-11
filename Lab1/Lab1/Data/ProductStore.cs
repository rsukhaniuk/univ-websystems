using Lab1.Models;

namespace univ_websystems_test.Data
{
    public class ProductStore
    {
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "1 name" },
            new Product { Id = 2, Name = "2 name" },
            new Product { Id = 3, Name = "3 name" },
            new Product { Id = 4, Name = "4 name" },
            new Product { Id = 5, Name = "5 name" },
            new Product { Id = 6, Name = "6 name" }
        };
    }
}
