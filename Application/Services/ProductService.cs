using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.Runtime.Internal.Util;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.DTOs.Entries;
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
        private async Task ControlCreateProduct(ProductInput product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<ProductInput, ProductValidator>();
                if (product.Quantity >= 0 && product.Price >= 0 
                    && product.Quantity <= Int32.MaxValue 
                    && product.Price <= Int32.MaxValue)
                    await _productRepository.CreateProductAsync(product);
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid),
                    nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid));
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
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }
       
        public async Task CreateProduct(ProductInput product)
        {
            await ControlCreateProduct(product);
        }

        /// <summary>
        /// Private method controls to get a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<ProductOutput> ControlGetProductById(string _id)
        {
            try
            {
                
                var productToGet = await _productRepository.GetProductByIdAsync(_id);
                return productToGet;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} getting product"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} getting product ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ProductIdIsNotValid),
                    nameof(GateWayBusinessException.ProductIdIsNotValid));
            }
        }

        public async Task<ProductOutput> GetProductById(string _id)
        {
            return await ControlGetProductById(_id);
        }

        /// <summary>
        /// Private method controls to get a product
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<List<ProductOutput>> ControlGetAllProducts()
        {
            try
            {
                List<ProductOutput> productsList = await _productRepository.GetAllProductsAsync();
                return productsList.Count == 0 ? throw new BusinessException(
                    nameof(GateWayBusinessException.ProductListCannotBeNull),
                    nameof(GateWayBusinessException.ProductListCannotBeNull)) : productsList;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} get all products"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} get all products ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }

        public async Task<List<ProductOutput>> GetAllProducts()
        {
            return await ControlGetAllProducts();
        }

        /// <summary>
        /// Private method controls to get a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<List<ProductInput>> ControlGetProductsPagination(int page, int size)
        {
            try
            {
                
                List<ProductInput> productsList = await _productRepository.GetProductsPaginationAsync(page, size);
                return productsList.Count == 0 ? throw new BusinessException(
                    nameof(GateWayBusinessException.ProductListCannotBeNull),
                    nameof(GateWayBusinessException.ProductListCannotBeNull)) : productsList;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating product"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating product ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }

        public async Task<List<ProductInput>> GetProductsPagination(int page, int size)
        {
            return await ControlGetProductsPagination(page, size);
        }

        /// <summary>
        /// Calling the business logic from the ProductAdapter
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<ProductOutput> UpdateProduct(ProductInput product, string _id)
        {
            try
            {
                await product.ValidateAndThrowsAsync<ProductInput, ProductValidator>();
                ProductOutput productOutput = new ProductOutput()
                {
                    _id = _id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Category = product.Category,
                    State = product.State,
                };
                if (product.Quantity >= 0 && product.Price >= 0)
                    return await _productRepository.UpdateProductAsync(productOutput);
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid),
                    nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid));
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
                throw new BusinessException(nameof(GateWayBusinessException.ProductIdIsNotValid),
                    nameof(GateWayBusinessException.ProductIdIsNotValid));
            }
        }
    }
}
