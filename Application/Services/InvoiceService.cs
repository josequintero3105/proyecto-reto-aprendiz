using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Core.Entities.MongoDB;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<InvoiceService> _logger;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerRepository _customerRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCartRepository"></param>
        /// <param name="logger"></param>
        public InvoiceService(IInvoiceRepository invoiceRepository, 
            ILogger<InvoiceService> logger, 
            IShoppingCartRepository shoppingCartRepository, 
            ICustomerRepository customerRepository)
        {
            _invoiceRepository = invoiceRepository;
            _logger = logger;
            _shoppingCartRepository = shoppingCartRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Generate a new invoice for tests
        /// </summary>
        /// <param name="invoiceInput"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<InvoiceOutput> GenerateInvoice(InvoiceInput invoiceInput)
        {
            try
            {
                await invoiceInput.ValidateAndThrowsAsync<InvoiceInput, InvoiceValidator>();
                InvoiceOutput invoice = new()
                {
                    ShoppingCartId = invoiceInput.ShoppingCartId,
                    CustomerId = invoiceInput.CustomerId,
                    CreatedAt = DateTime.Now
                };
                CustomerOutput customer = new();
                ShoppingCart shoppingCart = new();
                customer._id = invoice.CustomerId;
                shoppingCart._id = invoice.ShoppingCartId;
                CustomerCollection customerCollection = _customerRepository.GetCustomer(customer);
                ShoppingCartCollection shoppingCartCollection = _shoppingCartRepository.GetShoppingCart(shoppingCart);

                if (shoppingCartCollection != null
                    && customerCollection != null
                    && shoppingCartCollection.ProductsInCart!.Count != 0)
                {
                    invoice.CustomerName = customerCollection.Name;
                    invoice.Total = shoppingCartCollection.PriceTotal;
                    return await _invoiceRepository.GenerateInvoiceAsync(invoice);
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIsEmpty),
                    nameof(GateWayBusinessException.ShoppingCartIsEmpty));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }
        /// <summary>
        /// Generate a new invoice
        /// </summary>
        /// <param name="invoiceInput"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<InvoiceCollection> Generate(InvoiceInput invoiceInput)
        {
            try
            {
                await invoiceInput.ValidateAndThrowsAsync<InvoiceInput, InvoiceValidator>();
                InvoiceOutput invoice = new()
                {
                    ShoppingCartId = invoiceInput.ShoppingCartId,
                    CustomerId = invoiceInput.CustomerId,
                    CreatedAt = DateTime.Now
                };

                CustomerOutput customer = new();
                ShoppingCart shoppingCart = new();
                customer._id = invoice.CustomerId;
                shoppingCart._id = invoice.ShoppingCartId;
                CustomerCollection customerCollection = _customerRepository.GetCustomer(customer);
                ShoppingCartCollection shoppingCartCollection = _shoppingCartRepository.GetShoppingCart(shoppingCart);

                if (shoppingCartCollection != null
                    && customerCollection != null
                    && shoppingCartCollection.ProductsInCart!.Count != 0)
                {
                    invoice.CustomerName = customerCollection.Name;
                    invoice.Total = shoppingCartCollection.PriceTotal;
                    await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);
                    return await _invoiceRepository.GenerateAsync(invoice);
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIsEmpty),
                    nameof(GateWayBusinessException.ShoppingCartIsEmpty));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }
        /// <summary>
        /// Delete an invoice
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task DeleteInvoice(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    var result = await _invoiceRepository.DeleteInvoiceAsync(_id);
                    if (result == false)
                        throw new BusinessException(nameof(GateWayBusinessException.InvoiceIdNotFound),
                        nameof(GateWayBusinessException.InvoiceIdNotFound));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.InvoiceIdCannotBeNull),
                    nameof(GateWayBusinessException.InvoiceIdCannotBeNull));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.InvoiceIdIsNotValid),
                    nameof(GateWayBusinessException.InvoiceIdIsNotValid));
            }
        }
    }
}
