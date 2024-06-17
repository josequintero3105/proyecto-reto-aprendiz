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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;

namespace Application.Tests.Application.Tests.EntitiesTests
{
    public class CustomerTest
    {
        /// <summary>
        /// Intances
        /// </summary>
        private ICustomerService _customerService;
        private CustomerController _customerController;
        /// <summary>
        /// Mocks
        /// </summary>
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<ILogger<CustomerService>> _loggerMock = new();
        private readonly Mock<IHandle> _handleMock = new();

        public CustomerTest()
        {
            _customerService = new CustomerService(_customerRepositoryMock.Object, _loggerMock.Object);
            _customerController = new CustomerController(_customerService, _handleMock.Object);
            _customerController.ControllerContext.RouteData = new RouteData();
            _customerController.ControllerContext.RouteData.Values.Add("Controller", "Create");
        }

        [Fact]
        public async void CreateCustomer_When_AllFieldNotEmpty_Then_ExpectsResultEqualCustomer()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreation();
            _customerRepositoryMock.Setup(x => x.CreateCustomerAsync(customer))
                .ReturnsAsync(customer).Verifiable();

            // Act
            await _customerService.CreateCustomer(customer);

            // Assert
            Assert.IsType<Customer>(customer);
        }

        [Fact]
        public async void CreateCustomer_When_CustomerNameIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithNameEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.CreateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateCustomer_When_CustomerEmailIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithEmailEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.CreateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateCustomer_When_CustomerPhoneIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithPhoneEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.CreateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerAllFieldsAreValid_Then_ExpectsResultEqualCustomer()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForUpdate();
            _customerRepositoryMock.Setup(x => x.UpdateCustomerAsync(customer)).ReturnsAsync(true).Verifiable();

            // Act
            await _customerService.UpdateCustomer(customer);

            // Assert
            Assert.IsType<Customer>(customer);
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerNameIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithNameEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.UpdateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerEmailIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithEmailEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.UpdateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerPhoneIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithPhoneEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.UpdateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void DeleteCustomer_When_CustomerIdIsValid_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForDelete();
            customer._id = "6644d3d6a20a7c5dc4ed2680";
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.DeleteCustomer(customer._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void GetCustomer_When_CustomerIdIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForUpdate();
            customer._id = "";
            _customerRepositoryMock.Setup(x => x.DeleteCustomerAsync(customer._id)).ReturnsAsync(false).Verifiable();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.DeleteCustomer(customer._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateCustomer_Then_CodeStatusIsCreated()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreation();
            _customerController.ControllerContext.RouteData.Values.Add("action", "Post");
            _customerRepositoryMock.Setup(x => x.CreateCustomerAsync(customer)).Returns(Task.FromResult(customer));

            // Act
            var result = await _customerController.Create(customer);
            var objectResult = result as CreatedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, objectResult?.StatusCode);
        }

        [Fact]
        public async void GetCustomer_Then_CodeStatusIsOk()
        {
            // Arrange
            Customer customer = CustomerHelperModel.GetCustomerForCreation();
            customer._id = "6644d3d6a20a7c5dc4ed2680";
            _customerController.ControllerContext.RouteData.Values.Add("action", "Get");
            _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(customer._id)).Returns(Task.FromResult(customer));

            // Act
            var result = await _customerController.Get(customer._id);
            var objectResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objectResult?.StatusCode);
        }
    }
}
