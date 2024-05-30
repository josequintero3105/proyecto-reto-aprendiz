using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<ProductService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public CustomerService(ICustomerRepository customerRepository, ILogger<ProductService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        /// <summary>
        /// Private method controls the process of create a product
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task CreateCustomer(Customer customer)
        {
            try
            {
                await customer.ValidateAndThrowsAsync<Customer, CustomerValidator>();
                await _customerRepository.CreateCustomerAsync(customer);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating customer: {customer}"
                    , bex.Code, bex.Message, customer);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating customer: {customer} ", ex.Message, customer);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }

        public async Task<Customer> GetCustomerById(Customer customer)
        {
            try
            {
                var result = await _customerRepository.GetCustomerByIdAsync(customer);
                return result;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating customer: {customer}"
                    , bex.Code, bex.Message, customer);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating customer: {customer} ", ex.Message, customer);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }

        /// <summary>
        /// Private method controls the process of create a product
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                await customer.ValidateAndThrowsAsync<Customer, CustomerValidator>();
                await _customerRepository.UpdateCustomerAsync(customer);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating customer: {customer}"
                    , bex.Code, bex.Message, customer);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating customer: {customer} ", ex.Message, customer);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }
    }
}
