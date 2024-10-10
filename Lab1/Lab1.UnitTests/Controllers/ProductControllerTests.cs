using Lab1.Controllers;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.UnitTests.Controllers
{
    public class ProductControllerTests
    {
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ProductController();
        }

        [Test]
        public void GetProduct_IdIsZero_ReturnsBadRequest()
        {
            var result = _controller.GetProduct(0);

            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void GetProduct_ProductNotFound_ReturnsNotFound()
        {
            var result = _controller.GetProduct(999);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetProduct_ProductFound_ReturnsOkWithProduct()
        {
            var result = _controller.GetProduct(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<Product>(okResult.Value);
            var product = okResult.Value as Product;
            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("1 name", product.Name);
        }
    }
}