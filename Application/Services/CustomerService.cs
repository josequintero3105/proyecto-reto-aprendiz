using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
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
using MongoDB.Driver;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }
        /// <summary>
        /// Get the document types
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDocumentTypes()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>()
            {
                {"TI", ""},
                {"CC", ""},
                {"NIT", ""}
            };
            return keyValues;
        }

        /// <summary>
        /// Private method controls the process of create a product
        /// </summary>
        /// <param name="customerInput"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<CustomerCollection> CreateCustomer(CustomerInput customerInput)
        {
            try
            {
                await customerInput.ValidateAndThrowsAsync<CustomerInput, CustomerValidator>();
                Dictionary<string, string> keyValues = GetDocumentTypes();
                CustomerOutput customer = new()
                {
                    DocumentType = customerInput.DocumentType,
                    Document = customerInput.Document,
                    Name = customerInput.Name,
                    Email = customerInput.Email,
                    Phone = customerInput.Phone
                };
                if (keyValues.ContainsKey(customerInput.DocumentType))
                    return await _customerRepository.CreateAsync(customer);
                else
                    throw new BusinessException(nameof(GateWayBusinessException.CustomerDocumentTypeIsInvalid),
                    nameof(GateWayBusinessException.CustomerDocumentTypeIsInvalid));

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
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }
        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<CustomerOutput> GetCustomerById(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    var result = await _customerRepository.GetCustomerByIdAsync(_id);
                    if (result != null)
                        return result;
                    else
                        throw new BusinessException(nameof(GateWayBusinessException.CustomerIdNotFound),
                        nameof(GateWayBusinessException.CustomerIdNotFound));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.CustomerIdCannotBeNull),
                    nameof(GateWayBusinessException.CustomerIdCannotBeNull));
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
                throw new BusinessException(nameof(GateWayBusinessException.CustomerIdIsNotValid),
                    nameof(GateWayBusinessException.CustomerIdIsNotValid));
            }
        }

        /// <summary>
        /// Private method controls the process of create a product
        /// </summary>
        /// <param name="customerInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<CustomerOutput> UpdateCustomerData(CustomerInput customerInput, string _id)
        {
            try
            {
                await customerInput.ValidateAndThrowsAsync<CustomerInput, CustomerValidator>();
                Dictionary<string, string> keyValues = GetDocumentTypes();
                CustomerOutput customer = new()
                {
                    _id = _id,
                    DocumentType = customerInput.DocumentType,
                    Document = customerInput.Document,
                    Name = customerInput.Name,
                    Email = customerInput.Email,
                    Phone = customerInput.Phone
                };
                if (!String.IsNullOrEmpty(_id))
                {
                    var result = await _customerRepository.GetCustomerByIdAsync(_id);
                    if (result != null)
                    {
                        if (keyValues.ContainsKey(customerInput.DocumentType))
                            return await _customerRepository.UpdateCustomerDataAsync(customer);
                        else
                            throw new BusinessException(nameof(GateWayBusinessException.CustomerDocumentTypeIsInvalid),
                            nameof(GateWayBusinessException.CustomerDocumentTypeIsInvalid));
                    }
                    else                    
                        throw new BusinessException(nameof(GateWayBusinessException.CustomerIdNotFound),
                        nameof(GateWayBusinessException.CustomerIdNotFound));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.CustomerIdCannotBeNull),
                    nameof(GateWayBusinessException.CustomerIdCannotBeNull));
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
                throw new BusinessException(nameof(GateWayBusinessException.CustomerIdIsNotValid),
                    nameof(GateWayBusinessException.CustomerIdIsNotValid));
            }
        }

        /// <summary>
        /// Private method controls the process of create a product
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task DeleteCustomer(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    var result = await _customerRepository.DeleteCustomerAsync(_id);
                    if (result == false)
                        throw new BusinessException(nameof(GateWayBusinessException.CustomerIdNotFound),
                        nameof(GateWayBusinessException.CustomerIdNotFound));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.CustomerIdCannotBeNull),
                    nameof(GateWayBusinessException.CustomerIdCannotBeNull));
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
                throw new BusinessException(nameof(GateWayBusinessException.CustomerIdIsNotValid),
                    nameof(GateWayBusinessException.CustomerIdIsNotValid));
            }
        }
    }
}
