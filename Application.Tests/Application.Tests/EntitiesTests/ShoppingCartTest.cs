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
            
            _shoppingCartController = new ShoppingCartController(_shoppingCartServiceMock.Object, _handleMock.Object);
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async void AddToShoppingCart_When_GetShoppingCartIdInvalid_ExpectsBusinessException()
        {
            // Arrange
            ShoppingCart response = ShoppingCartHelperModel.GetShoppingCartExistOnMongo();                   

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCartObject(It.IsAny<ShoppingCart>()))
                .Throws(new BusinessException("Error", GateWayBusinessException.ShoppingCartIdIsNotValid.ToString()))
                .Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => 
                await _shoppingCartService.AddToShoppingCart(response));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void AddToShoppingCart_When_ProductDataNotValid_Then_ExpectsBusinessException()
        {
            //Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
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
        public async void GetShoppingCartById_When_IdWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
            shoppingCart._id = "6644d*+77042e#$&63d~°^a600";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCart(It.IsAny<ShoppingCart>()))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(), 
                GateWayBusinessException.NotAllowSpecialCharacters.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async() =>
             await _shoppingCartService.AddToShoppingCart(shoppingCart));

            // Assert
            Assert.Equal(result.Message, GateWayBusinessException.ProductIdIsNotValid.ToString());
        }

        [Fact]
        public async void RemoveToShoppingCart_When_ProductIdNotFound_Then_ExpectsFalseRequest()
        {
            // Arrange
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForCreation();
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
