using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Application.Interfaces.Common;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Application.Services;
using Application.Tests.Application.Tests.DTOs;
using AutoMapper;
using Core.Entities.MongoDB;
using Infrastructure.Services.MongoDB;
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
        private readonly Mock<ICustomerService> _customerServiceMock = new();
        private readonly Mock<ILogger<CustomerService>> _loggerMock = new();
        private readonly Mock<IHandle> _handleMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IContext> _contextMock = new();

        public CustomerTest()
        {
            _customerService = new CustomerService(_customerRepositoryMock.Object, _loggerMock.Object);
            _customerServiceMock = new Mock<ICustomerService>();
            _customerController = new CustomerController(_customerService, _handleMock.Object);
            _customerController.ControllerContext.RouteData = new RouteData();
            _customerController.ControllerContext.RouteData.Values.Add("Controller", "Create");
        }

        [Fact]
        public async void CreateCustomer_When_AllFieldNotEmpty_Then_ExpectsResultEqualCustomer()
        {
            // Arrange
            CustomerInput customerInput = CustomerHelperModel.GetCustomerForCreation();
            CustomerOutput customerOutput = CustomerHelperModel.GetCustomerFromMongo();
            _customerRepositoryMock.Setup(x => x.CreateCustomerAsync(customerOutput))
                .ReturnsAsync(customerOutput).Verifiable();

            // Act
            await _customerService.CreateCustomer(customerInput);

            // Assert
            Assert.IsType<CustomerOutput>(customerOutput);
        }

        [Fact]
        public async void CreateCustomer_When_AllFieldsNotEmpty_Then_ExpectsVerifyResult()
        {
            // Arrange
            CustomerInput customer = new CustomerInput()
            {
                Name = "name",
                Document = "1000",
                DocumentType = "CC",
                Email = "email@email.com",
                Phone = "123"
            };

            _customerRepositoryMock.Setup(x => x.CreateAsync(It.Is<CustomerOutput>
                (x => x.Name == customer.Name &&
                x.Document == customer.Document &&
                x.DocumentType == customer.DocumentType &&
                x.Email == customer.Email &&
                x.Phone == customer.Phone
                )));

            // Act
            await _customerService.CreateCustomer(customer);

            // Assert
            _customerServiceMock.Verify();
        }

        [Fact]
        public async void CreateCustomer_When_CustomerNameIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithNameEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.CreateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateCustomer_When_CustomerDocumentIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithDocumentEmpty();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.CreateCustomer(customer));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateCustomer_When_CustomerDocumentTypeIsEmpty_Then_ExpectsBusinessException()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithDocumentTypeEmpty();

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
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithEmailEmpty();

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
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithPhoneEmpty();

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
            CustomerInput customerInput = CustomerHelperModel.GetCustomerForCreation();
            CustomerOutput customer = CustomerHelperModel.GetCustomerFromMongo();
            customer._id = "6644d3d6a20a7c5dc4ed2680";
            _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(customer._id)).ReturnsAsync(customer).Verifiable();
            _customerRepositoryMock.Setup(x => x.UpdateCustomerDataAsync(customer)).ReturnsAsync(customer).Verifiable();

            // Act
            await _customerService.UpdateCustomerData(customerInput, customer._id);

            // Assert
            Assert.IsType<CustomerOutput>(customer);
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerNameIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithNameEmpty();
            CustomerOutput customerOutput = CustomerHelperModel.GetCustomerFromMongo();
            customerOutput._id = "6644d3d6a20a7c5dc4ed2680";
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.UpdateCustomerData(customer, customerOutput._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerEmailIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithEmailEmpty();
            CustomerOutput customerOutput = CustomerHelperModel.GetCustomerFromMongo();
            customerOutput._id = "6644d3d6a20a7c5dc4ed2680";
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.UpdateCustomerData(customer, customerOutput._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void UpdateCustomer_When_CustomerPhoneIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreationOrUpdateWithPhoneEmpty();
            CustomerOutput customerOutput = CustomerHelperModel.GetCustomerFromMongo();
            customerOutput._id = "6644d3d6a20a7c5dc4ed2680";
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _customerService.UpdateCustomerData(customer, customerOutput._id));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void DeleteCustomer_When_CustomerIdIsEmpty_ExpectsBusinessException()
        {
            // Arrange
            CustomerOutput customer = CustomerHelperModel.GetCustomerFromMongo();
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
            CustomerInput customerInput = CustomerHelperModel.GetCustomerForCreation();
            CustomerOutput customerOutput = CustomerHelperModel.GetCustomerFromMongo();
            CustomerCollection customerCollection = CustomerHelperModel.GetCustomerCollection();
            _contextMock.Setup(x => x.CustomerCollection).Verifiable();
            _mapperMock.Setup(x => x.Map<CustomerCollection>(customerOutput)).Verifiable();
            _customerController.ControllerContext.RouteData.Values.Add("action", "Post");
            _customerServiceMock.Setup(x => x.CreateCustomer(customerInput)).ReturnsAsync(customerCollection).Verifiable();
            _customerRepositoryMock.Setup(x => x.CreateAsync(customerOutput)).Returns(Task.FromResult(customerCollection));

            // Act
            var result = await _customerController.Create(customerInput);
            var objectResult = result as CreatedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, objectResult?.StatusCode);
        }

        [Fact]
        public async void GetCustomer_Then_CodeStatusIsOk()
        {
            // Arrange
            CustomerInput customer = CustomerHelperModel.GetCustomerForCreation();
            CustomerOutput customerOutput = CustomerHelperModel.GetCustomerFromMongo();
            customerOutput._id = "6644d3d6a20a7c5dc4ed2680";
            _customerController.ControllerContext.RouteData.Values.Add("action", "Get");
            _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(customerOutput._id)).Returns(Task.FromResult(customerOutput));

            // Act
            var result = await _customerController.GetCustomerById(customerOutput._id);
            var objectResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objectResult?.StatusCode);
        }
    }
}
