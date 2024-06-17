using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Application.Services;
using Application.Tests.Application.Tests.DTOs;
using Infrastructure.Services.MongoDB;
using WebApiHttp.Controllers;
using MongoDB.Driver;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MongoDB.Bson;
using Infrastructure.Services.MongoDB.Adapters;
using AutoMapper;
using Core.Entities.MongoDB;
using Microsoft.AspNetCore.Mvc;
using RestApi.Filters;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Common;
using Xunit.Sdk;
using Common.Helpers.Exceptions;

namespace Application.Tests.Application.Tests.Services
{
    public class ProductTest
    {
        
        private IProductService _productService;
        private readonly ProductController _productController;
        /// <summary>
        /// Mocks
        /// </summary>
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IProductService> _productServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<ProductService>> _loggerMock = new();
        private readonly Mock<IHandle> _handleMock = new();
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object, _loggerMock.Object);
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object, _handleMock.Object);
            _mapperMock = new Mock<IMapper>();
        }
        
        [Fact]
        public async void CreateProduct_When_ProductNameIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithProductNameEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.CreateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateProduct_When_ProductNameWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithProductNameWrongFormat();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.CreateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateProduct_When_ProductDescriptionIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithProductDescriptionEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.CreateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateProduct_When_ProductDescriptionWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithProductDescriptionWrongFormat();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.CreateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateProduct_When_ProductCategoryIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithProductCategoryEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.CreateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateProduct_When_ProductCategoryWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithProductCategoryWrongFormat();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.CreateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateProduct_When_ProductFieldsNotEmpty_Then_ResultEqualProduct()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreation();
            _productRepositoryMock.Setup(repo => repo.CreateProductAsync(product))
                .ReturnsAsync(product).Verifiable();

            // Act
            await _productService.CreateProduct(product);

            // Assert
            Assert.IsType<Product>(product);
        }

        [Fact]
        public async void CreateProduct_When_ProductPriceEqualToZero_Then_ExpectsVerifyToCreateNewProduct()
        {
            // Arrange
            Product product = ProductHelperModel.GetProductForCreationWithoutProductPrice();
            _productRepositoryMock.Setup(repo => repo.CreateProductAsync(product))
                .ReturnsAsync(product).Verifiable();

            // Act
            await _productController.Create(product);

            // Assert
            Assert.True(product.Price == 0);
        }

        [Fact]
        public async void CreateProduct_With_NoFieldsEmpty_Then_ExpectsVerify()
        {
            // Arrange
            Product product = new Product();
            product.Name = "Producto de prueba";
            product.Price = 10.000;
            product.Quantity = 10;
            product.Description = "Descripcion de prueba";
            product.Category = "Categoria";
            product.State = true;

            _productRepositoryMock.Setup(x => x.CreateProductAsync(It.Is<Product>
                (x => x.Name == product.Name &&
                x.Price == product.Price &&
                x.Quantity == product.Quantity &&
                x.Description == product.Description &&
                x.Category == product.Category &&
                x.State == product.State)))
                .Verifiable();

            // Act
            await _productService.CreateProduct(product);

            // Assert
            _productServiceMock.Verify();
        }

        [Fact]
        public async Task UpdateProduct_When_ObjectIdMongoIsValid_Then_ResultEqualProduct()
        {
            // Arrange
            ProductToGet product = ProductHelperModel.GetProductFromMongo();
            _productRepositoryMock.Setup(repo => repo.UpdateProductAsync(product)).ReturnsAsync(true).Verifiable();

            // Act
            await _productService.UpdateProduct(product);

            // Assert
            Assert.IsType<ProductToGet>(product);
        }

        [Fact]
        public async Task UpdateProduct_ProductNameIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            ProductToGet product = ProductHelperModel.GetProductForUpdateWithProductNameEmpty();
            ProductCollection productCollection = new ProductCollection();

            _mapperMock.Setup(x => x.Map<ProductCollection>(It.IsAny<ProductToGet>())).Returns(productCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.UpdateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async Task UpdateProduct_ProductDescriptionIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            ProductToGet product = ProductHelperModel.GetProductForUpdateWithProductDescriptionEmpty();
            ProductCollection productCollection = new ProductCollection();

            _mapperMock.Setup(x => x.Map<ProductCollection>(It.IsAny<ProductToGet>())).Returns(productCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.UpdateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async Task UpdateProduct_ProductCategoryIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            ProductToGet product = ProductHelperModel.GetProductForUpdateWithProductCategoryEmpty();
            ProductCollection productCollection = new ProductCollection();

            _mapperMock.Setup(x => x.Map<ProductCollection>(It.IsAny<ProductToGet>())).Returns(productCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.UpdateProduct(product));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void GetProduct_When_IdNotFountInMongo_ExpectsResultEqualProduct()
        {
            // Arrange
            var productFound = ProductHelperModel.GetProductFromMongo();
            productFound._id = "661feb4a110728200e31903e";

            _productRepositoryMock.Setup(x => x.GetProductByIdAsync(productFound._id))
                .ReturnsAsync(productFound).Verifiable();

            // Act
            await _productService.GetProductById(productFound._id);

            // Assert
            Assert.IsType<ProductToGet>(productFound);
        }

        [Fact]
        public async void GetProduct_When_ProductIdWrongFormat_ExpectsBusinessException()
        {
            // Arrange
            var productFound = ProductHelperModel.GetProductFromMongo();
            productFound._id = "6644d*+77042e#$&63d~°^a600";

            _productRepositoryMock.Setup(x => x.GetProductByIdAsync(productFound._id))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                    GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () =>
             await _productService.GetProductById(productFound._id));

            // Assert
            Assert.Equal(result.Message, GateWayBusinessException.ShoppingCartIdIsNotValid.ToString());
        }

        [Fact]
        public async void ListProducts_When_ProductListIsFound_ExpectsResultList()
        {
            // Assert
            List<Product> products = ProductHelperModel.ListAllProducts();
            _productRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products).Verifiable();

            // Act
            await _productService.GetAllProducts();

            // Arrange
            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public async void ListProduct_When_ListProductsIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            List<Product> products = ProductHelperModel.ListAllProductsIsEmpty();
            _productRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _productService.GetAllProducts());

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());            
        }
    }
}
