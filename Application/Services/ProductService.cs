using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.Runtime.Internal.Util;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Core.Entities.MongoDB;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        /// <summary>
        /// Calling the business logic from ProductAdapter
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreateProduct(Product product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<Product, ProductValidator>();
                await _productRepository.CreateProductAsync(product);
            } 
            catch(BusinessException bex)
            {
                _logger.LogError(bex, "Error: {code}-{message}", bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Calling the business logic from the ProductAdapter
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateProduct(Product product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<Product, ProductValidator>();
                await _productRepository.UpdateProductAsync(product);
            }
            catch(BusinessException bex)
            {
                _logger.LogError(bex, "Error: {code}-{message}", bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw;
            }
        }
    }
}
