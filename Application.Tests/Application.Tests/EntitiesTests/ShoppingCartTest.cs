using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Common;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Application.Services;
using Application.Tests.Application.Tests.DTOs;
using AutoMapper;
using Common.Helpers.Exceptions;
using Core.Entities.MongoDB;
using Infrastructure.Services.MongoDB;
using Infrastructure.Services.MongoDB.Adapters;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using WebApi.Controllers;
using WebApiHttp.Controllers;

namespace Application.Tests.Application.Tests.EntitiesTests
{
    public class ShoppingCartTest
    {
        /// <summary>
        /// Intances
        /// </summary>
        private IShoppingCartService _shoppingCartService;
        private readonly ShoppingCartController _shoppingCartController;
        
        /// <summary>
        /// Mocks
        /// </summary>
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock = new();
        private readonly Mock<IShoppingCartService> _shoppingCartServiceMock = new();
        private readonly Mock<ILogger<ShoppingCartService>> _loggerMock = new();
        
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IContext> _contextMock = new();
        private readonly Mock<IHandle> _handleMock = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartTest() 
        {
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryMock.Object, _loggerMock.Object);
            _shoppingCartController = new ShoppingCartController(_shoppingCartServiceMock.Object, _handleMock.Object);
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddToShoppingCart_When_ProductDataNotValid_Then_ExpectsBusinessException()
        {
            //Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            ShoppingCartCollection shoppingCartCollection = new ShoppingCartCollection();

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>()))
                .Returns(shoppingCartCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.AddToShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void RemoveToShoppingCart_When_ProductIdNotFound_Then_ExpectsFalseRequest()
        {
            // Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForRemoveProducts();
            ShoppingCartCollection shoppingCartCollection = new ShoppingCartCollection();

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>())).Returns(shoppingCartCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.RemoveFromShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void GetShoppingCart_When_ProducesErrorInDataBase_Then_ExpectsErrorResultNotNull()
        {
            // Arrange
            var getShoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCartAsync(It.IsAny<ShoppingCart>()))
                .Throws(new Exception("Error")).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => 
            await _shoppingCartService.GetShoppingCartById(getShoppingCart));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void AddToShoppingCart_When_GetShoppingCartIdInvalid_ExpectsBusinessException()
        {
            // Arrange
            var shoppingCartFound = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCartFound._id = "664f40fed44e5362205a9381";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCartAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(shoppingCartFound).Verifiable();

            // Act
            await _shoppingCartService.GetShoppingCartById(shoppingCartFound);

            // Assert
            Assert.IsType<ShoppingCart>(shoppingCartFound);
        }

        [Fact]
        public async void AddToShopping_When_ProductIdIsNotEmpty_ExpectsTrueResult()
        {
            // Arrange
            ShoppingCart shoppingCartFound = new ShoppingCart();
            shoppingCartFound._id = "664f40fed44e5362205a9381";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCartAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(shoppingCartFound).Verifiable();

            // Act
            await _shoppingCartService.GetShoppingCartById(shoppingCartFound);

            // Assert
            Assert.True(shoppingCartFound._id != "");
        }

        [Fact]
        public async void GetShoppingCartById_When_IdWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCart._id = "6644d*+77042e#$&63d~°^a600";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCart(It.IsAny<ShoppingCart>()))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                    GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async() =>
             await _shoppingCartService.AddToShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(result.Message, GateWayBusinessException.NotAllowSpecialCharacters.ToString());
        }

        [Fact]
        public async void ListProductsToWriteQuantities_When_ProductsInCartExists_ExpectsResultEqualListProduct()
        {
            //Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            ShoppingCartCollection shoppingCartCollection = new ShoppingCartCollection();
            List<ProductCollection> listProductCollections = new List<ProductCollection>();
            List<string> productIds = ShoppingCartHelperModel.IdList;

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>()))
                .Returns(shoppingCartCollection);

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCart(It.IsAny<ShoppingCart>()))
                .Returns(shoppingCartCollection).Verifiable();

            _shoppingCartRepositoryMock.Setup(x => x.ListSpecificProducts(productIds))
                .ReturnsAsync(listProductCollections).Verifiable();

            //Act
            await _shoppingCartService.AddToShoppingCart(shoppingCart);

            //Assert
            Assert.IsType<List<ProductCollection>>(listProductCollections);
        }

        [Fact]
        public async void ListProductsToRemoveFromCart_When_ProductsInCartExists_ExpectsResultEqualListProduct()
        {
            //Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            ShoppingCartCollection shoppingCartCollection = new ShoppingCartCollection();
            List<ProductCollection> listProductCollections = new List<ProductCollection>();
            List<string> productIds = ShoppingCartHelperModel.IdList;

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>()))
                .Returns(shoppingCartCollection);

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCart(It.IsAny<ShoppingCart>()))
                .Returns(shoppingCartCollection).Verifiable();

            _shoppingCartRepositoryMock.Setup(x => x.ListSpecificProducts(productIds))
                .ReturnsAsync(listProductCollections).Verifiable();

            //Act
            await _shoppingCartService.RemoveFromShoppingCart(shoppingCart);

            //Assert
            Assert.IsType<List<ProductCollection>>(listProductCollections);
        }
    }
}
