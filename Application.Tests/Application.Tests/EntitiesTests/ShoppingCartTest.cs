using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs.ApiEntities.Output;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
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
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ShoppingCartController _shoppingCartController;
        private readonly TransactionController _transactionController;
        /// <summary>
        /// Mocks
        /// </summary>
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock = new();
        private readonly Mock<IShoppingCartService> _shoppingCartServiceMock = new();
        private readonly Mock<ITransactionService> _transacionServiceMock = new();
        private readonly Mock<ILogger<ShoppingCartService>> _loggerCartMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IHandle> _handleMock = new();        

        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartTest() 
        {
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryMock.Object, _loggerCartMock.Object, _transacionServiceMock.Object);
            _shoppingCartController = new ShoppingCartController(_shoppingCartServiceMock.Object, _handleMock.Object);
            _transactionController = new TransactionController(_shoppingCartServiceMock.Object);
            _shoppingCartController.ControllerContext.RouteData = new RouteData();
            _shoppingCartController.ControllerContext.RouteData.Values.Add("Controller", "Create");
            _transactionController.ControllerContext.RouteData = new RouteData();
            _transactionController.ControllerContext.RouteData.Values.Add("Controller", "Process");
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateShoppingCart_Then_ExpectsBussinessException()
        {
            // Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            List<string> strings = ShoppingCartHelperModel.GetProductIds();
            List<WriteModel<ProductCollection>> writeModels = ShoppingCartHelperModel.WriteModels();
            List<ProductCollection> productCollections = ShoppingCartHelperModel.ProductCollections();
            ShoppingCartCollection shoppingCartCollection = ShoppingCartHelperModel.GetShoppingCartCollectionFromMongo();
            ProductInCart productInCart = ShoppingCartHelperModel.ProductInCart();
            ShoppingCart shoppingCartOutPut = new()
            {
                ProductsInCart = shoppingCartInput.ProductsInCart
            };
            _shoppingCartRepositoryMock.Setup(x => x.ListSpecificProducts(strings)).Verifiable();
            _shoppingCartRepositoryMock.Setup(x => x.FilterToGetProduct(writeModels, productCollections[0])).Verifiable();
            _shoppingCartRepositoryMock.Setup(x => x.AddAnotherProductInCartAsync(shoppingCartCollection, productInCart)).Verifiable();
            _shoppingCartRepositoryMock.Setup(x => x.UpdateQuantityForProduct(productCollections[0])).Verifiable();
            _shoppingCartRepositoryMock.Setup(x => x.UpdatePriceTotalFromShoppingCart(shoppingCartCollection)).Verifiable();
            _shoppingCartRepositoryMock.Setup(x => x.CreateAsync(shoppingCartOutPut)).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.CreateShoppingCart(shoppingCartInput));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async Task CreateShoppingCart_WhenProductListIsNull_Then_ExpectsBusinessException()
        {
            // Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            ShoppingCart shoppingCartOutPut = new();
            _shoppingCartRepositoryMock.Setup(x => x.CreateShoppingCartAsync(shoppingCartOutPut))
                .ReturnsAsync(shoppingCartOutPut).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.CreateShoppingCart(shoppingCartInput));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async Task CreateShoppingCart_Then_ExpectsVerifyCreation()
        {
            // Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            _shoppingCartRepositoryMock.Setup(x => x.CreateShoppingCartAsync(It.Is<ShoppingCart>
                (x => x.ProductsInCart == shoppingCartInput.ProductsInCart))).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.CreateShoppingCart(shoppingCartInput));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async Task AddToShoppingCart_When_ProductDataNotValid_Then_ExpectsBusinessException()
        {
            //Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            ShoppingCartCollection shoppingCartCollection = new();
            shoppingCart._id = "66574ea38d0535a677a3e029";

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>()))
                .Returns(shoppingCartCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.AddToShoppingCart(shoppingCartInput, shoppingCart._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void RemoveToShoppingCart_When_ProductIdNotFound_Then_ExpectsFalseRequest()
        {
            // Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            ShoppingCart shoppingCart = ShoppingCartHelperModel.GetShoppingCartForRemoveProducts();
            ShoppingCartCollection shoppingCartCollection = new();
            shoppingCart._id = "66574ea38d0535a677a3e029";

            _mapperMock.Setup(x => x.Map<ShoppingCartCollection>(It.IsAny<ShoppingCart>())).Returns(shoppingCartCollection);

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.RemoveFromShoppingCart(shoppingCartInput, shoppingCart._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void GetShoppingCartById_When_IdWrongFormat_Then_ExpectsBusinessException()
        {
            // Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCart._id = "6644d*+77042e#$&63d~°^a600";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCart(It.IsAny<ShoppingCart>()))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                    GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () =>
             await _shoppingCartService.AddToShoppingCart(shoppingCartInput, shoppingCart._id));

            // Assert
            Assert.Equal(result.Message, GateWayBusinessException.NotAllowSpecialCharacters.ToString());
        }

        [Fact]
        public async void ListShoppingCarts_When_CartStatusIsPending_Then_ExpectsBusinessException()
        {
            // Arrange
            ShoppingCartInput shoppingCartInput = ShoppingCartHelperModel.GetShoppingCartForCreation();
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCart._id = "66574ea38d0535a677a3e029";
            var listCarts = ShoppingCartHelperModel.ShoppingCartCollections();
            _shoppingCartRepositoryMock.Setup(x => x.ListAllCarts())
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.AddToShoppingCart(shoppingCartInput, shoppingCart._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void ResetShoppingCart_When_CartIdIsNull_Then_ExpectsBusinessException()
        {
            // Arrange
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCart._id = "66574ea38d0535a677a3e029";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCartForReset(shoppingCart._id))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.ResetShoppingCart(shoppingCart._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void ProcessTransaction_When_ShoppingCartIsInvalid_Then_ExpectsBusinessException()
        {
            // Arrange
            var shoppingCart = ShoppingCartHelperModel.GetShoppingCartFromMongo();
            shoppingCart._id = "66574ea38d0535a677a3e029";

            _shoppingCartRepositoryMock.Setup(x => x.GetShoppingCartForReset(shoppingCart._id))
                .Throws(new BusinessException(GateWayBusinessException.ShoppingCartIdIsNotValid.ToString(),
                GateWayBusinessException.ShoppingCartIdIsNotValid.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.ProcessCartForTransaction(null!));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void GetTransactionResponse_When_TransactionIdIsNull_Then_ExpectsBusinessException()
        {
            // Arrange
            var transactionInput = ShoppingCartHelperModel.TransactionInput();
            var transactionOutput = ShoppingCartHelperModel.TransactionOutput();
            transactionOutput._id = "";

            _transacionServiceMock.Setup(x => x.GetTransaction(transactionOutput._id))
                .Throws(new BusinessException(GateWayBusinessException.TransactionAttemptFailed.ToString(),
                GateWayBusinessException.TransactionAttemptFailed.ToString())).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _shoppingCartService.ProcessCartForTransaction(transactionInput));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }
    }
}
