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
using Common.Helpers.Exceptions;
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
        /// Private method controls the process of create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task ControlCreateProduct(Product product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<Product, ProductValidator>();
                await _productRepository.CreateProductAsync(product);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating product: {product}"
                    , bex.Code, bex.Message, product);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating product: {product} ", ex.Message, product);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }
       
        public async Task CreateProduct(Product product)
        {
            await ControlCreateProduct(product);
        }


        /// <summary>
        /// Calling the business logic from the ProductAdapter
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task ControlUpdateProduct(ProductUpdate product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<ProductUpdate, ProductUpdateValidator>();
                var update = await _productRepository.UpdateProductAsync(product);
                if (update == false)
                {
                    throw new BusinessException(nameof(GateWayBusinessException.ProductIdIsNotValid),
                    nameof(GateWayBusinessException.ProductIdIsNotValid));
                }
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} updating product: {product}",
                    bex.Code, bex.Message, product);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} updating product: {product} ", ex.Message, product);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }
        
        public async Task UpdateProduct(ProductUpdate product)
        {
            await ControlUpdateProduct(product);
        }
    }
}
