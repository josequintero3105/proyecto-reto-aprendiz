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
using Infrastructure.Services.MongoDB;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;

namespace Application.Tests.Application.Tests.EntitiesTests
{
    public class InvoiceTest
    {
        /// <summary>
        /// Intances
        /// </summary>
        private IInvoiceService _invoiceService;

        /// <summary>
        /// Mocks
        /// </summary>
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepositoryMock = new();
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<ILogger<InvoiceService>> _loggerMock = new();

        public InvoiceTest() 
        {
            _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            _invoiceService = new InvoiceService(_invoiceRepositoryMock.Object, 
                _loggerMock.Object, _shoppingCartRepositoryMock.Object, _customerRepositoryMock.Object);
        }

        [Fact]
        public async void CreateInvoice_When_CartIdAndCustomerIdAreValid_Then_ExpectsResultEqualInvoice()
        {
            // Arrange
            Invoice invoice = InvoiceHelperModel.GetInvoiceFromCreation();
            _invoiceRepositoryMock.Setup(x => x.GenerateInvoiceAsync(invoice))
                .ReturnsAsync(invoice).Verifiable();

            // Act
            await _invoiceService.GenerateInvoice(invoice);

            // Assert
            Assert.IsType<Invoice>(invoice);
        }

        [Fact]
        public async void CreateInvoice_When_CustomerIdNotValid_Then_ExpectsBusinessException()
        {
            // Arrange
            Invoice invoice = InvoiceHelperModel.GetInvoiceFromCreationCustomerIdInvalid();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _invoiceService.GenerateInvoice(invoice));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }

        [Fact]
        public async void CreateInvoice_When_ShoppingCartIdNotValid_Then_ExpectsBusinessException()
        {
            // Arrange
            Invoice invoice = InvoiceHelperModel.GetInvoiceFromCreationShoppingCartIdInvalid();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>
                (async () => await _invoiceService.GenerateInvoice(invoice));

            // Assert
            Assert.Equal(typeof(BusinessException), result.GetType());
        }
    }
}
