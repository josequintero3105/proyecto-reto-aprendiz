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
using Core.Entities.MongoDB;
using Infrastructure.Services.MongoDB;
using Infrastructure.Services.MongoDB.Adapters;
using Microsoft.Extensions.Logging;
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
        private IShoppingCartRepository _shoppingCartRepository;
        
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
            _shoppingCartRepository = new ShoppingCartAdapter(_contextMock.Object, _mapperMock.Object);
            _shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryMock.Object, _loggerMock.Object);
            _shoppingCartController = new ShoppingCartController(_shoppingCartServiceMock.Object, _handleMock.Object);
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async void AddToShoppingCart_When_ProductDataNotValid_Then_ExpectsBusinessException()
        {
            //Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForAddProducts();
            ShoppingCartCollection shoppingCartCollection = new ShoppingCartCollection();
            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>())).Returns(shoppingCartCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.AddToShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateShoppingCart_WithoutProducts_Then_ReturnsResultEqualsShoppingCart()
        {
            // Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
            _shoppingCartRepositoryMock.Setup(repo => repo.CreateShoppingCartAsync(shoppingCart))
                .ReturnsAsync(shoppingCart).Verifiable();

            // Act
            await _shoppingCartService.CreateShoppingCart(shoppingCart);

            // Assert
            Assert.IsType<ShoppingCart>(shoppingCart);
        }

        [Fact]
        public async void GetShoppingCartById_When_IdWrongFormat_Then_ExpectsBusinessException()
        {
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartByIdCorrectFormat();
            shoppingCart._id = "663d3587652?58_c*36263e";
            _shoppingCartRepositoryMock.Setup(x => x);
        }

        [Fact]
        public async void RemoveToShoppingCart_When_ProductIdNotFound_Then_ExpectsFalseRequest()
        {
            // Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForAddProducts();
            ShoppingCartCollection shoppingCartCollection = new ShoppingCartCollection();

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>())).Returns(shoppingCartCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.RemoveFromShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }
    }
}
