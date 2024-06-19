using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly Mock<IHandle> _handleMock = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartTest() 
        {
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryMock.Object, _loggerMock.Object);
            _shoppingCartController = new ShoppingCartController(_shoppingCartServiceMock.Object, _handleMock.Object);
            
            _shoppingCartController.ControllerContext.RouteData = new RouteData();
            _shoppingCartController.ControllerContext.RouteData.Values.Add("Controller", "Create");
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateShoppingCart_Then_ExpectsResultEqualShoppingCart()
        {
            // Act
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
            _shoppingCartRepositoryMock.Setup(x => x.CreateShoppingCartAsync(shoppingCart))
                .ReturnsAsync(shoppingCart).Verifiable();

            // Assert
            await _shoppingCartService.CreateShoppingCart(shoppingCart);

            // Arrange
            Assert.IsType<ShoppingCart>(shoppingCart);
        }

        [Fact]
        public async Task CreateShoppingCart_Then_ResultCartIsNotNull()
        {
            // Act
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
            _shoppingCartRepositoryMock.Setup(x => x.CreateShoppingCartAsync(shoppingCart))
                .ReturnsAsync(shoppingCart).Verifiable();

            // Assert
            await _shoppingCartService.CreateShoppingCart(shoppingCart);

            // Arrange
            Assert.NotNull(shoppingCart);
        }

        [Fact]
        public async Task CreateShoppingCart_Then_ExpectsVerifyCreation()
        {
            // Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
            _shoppingCartRepositoryMock.Setup(x => x.CreateShoppingCartAsync(It.Is<ShoppingCart>
                (x => x.ProductsInCart == shoppingCart.ProductsInCart &&
                x.PriceTotal == shoppingCart.PriceTotal &&
                x.Active == shoppingCart.Active
                ))).Verifiable();

            // Act
            await _shoppingCartService.CreateShoppingCart(shoppingCart);

            // Arrange
            _shoppingCartRepositoryMock.Verify();
        }

        [Fact]
        public async Task CreateShoppingCart_When_ResponseIsCorrect_Then_ExpectsResponseOk()
        {
            // Act
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
            _shoppingCartController.ControllerContext.RouteData.Values.Add("action", "Post");
            _shoppingCartRepositoryMock.Setup(x => x.CreateShoppingCartAsync(shoppingCart))
                .Returns(Task.FromResult(shoppingCart));

            // Assert
            var result = await _shoppingCartController.Create(shoppingCart);
            var objectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objectResult?.StatusCode);
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
        public async void GetShoppingCartById_When_IdWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCart._id = "6644d*+77042e#$&63d~°^a600";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCart(It.IsAny<ShoppingCart>()))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                    GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () =>
             await _shoppingCartService.AddToShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(result.Message, GateWayBusinessException.NotAllowSpecialCharacters.ToString());
        }
    }
}
