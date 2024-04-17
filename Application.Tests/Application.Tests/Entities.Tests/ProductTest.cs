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

namespace Application.Tests.Application.Tests.Services
{
    public class ProductTest
    {
        
        private IProductService _productService;
        private readonly ProductController _productController;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IProductService> _productServiceMock = new();

        public ProductTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
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
        public async void CreateProduct_When_ProductFieldsNotEmpty_Then_ExpectsBusinessException()
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
    }
}
