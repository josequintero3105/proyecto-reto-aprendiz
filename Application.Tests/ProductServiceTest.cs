using Application.Interfaces.Services;
using WebApiHttp.Controllers;
using Moq;
using Application.DTOs;
using Application.Interfaces.Common;
using Application.Services;

namespace Application.Tests
{
    public class ProductServiceTest
    {
        private IProductService _productService;
        private ProductController _productController;
        private Product product;
        private IHandle _handle;

        public ProductServiceTest()
        {
            _productService = new ProductService();
            _productController = new ProductController();
        }

        [Fact]
        public void CreateProduct_IsValid()
        {
            // Arrange
            var MockProductService = new Mock<IProductService>();

            // Act
            MockProductService.Setup(sp => sp.InsertProduct(product));
            ProductController _productController = new ProductController();
            var result = _productController.Create(product);

            // Assert
            Assert.NotNull(result);

        }
    }
}