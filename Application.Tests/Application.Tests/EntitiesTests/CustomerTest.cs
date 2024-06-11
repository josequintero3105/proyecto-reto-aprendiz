using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Application.Services;
using Application.Tests.Application.Tests.DTOs;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Application.Tests.EntitiesTests
{
    public class CustomerTest
    {
        /// <summary>
        /// Intances
        /// </summary>
        private ICustomerService _customerService;

        /// <summary>
        /// Mocks
        /// </summary>
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<ILogger<CustomerService>> _loggerMock = new();

        public CustomerTest()
        {
            _customerService = new CustomerService(_customerRepositoryMock.Object, _loggerMock.Object);
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
    }
}
