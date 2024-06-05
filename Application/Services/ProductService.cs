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
        /// Private method controls to get a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<ProductToGet> ControlGetProductById(ProductToGet product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<ProductToGet, ProductUpdateValidator>();
                ProductToGet productToGet = await _productRepository.GetProductByIdAsync(product);
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
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }

        public async Task<ProductToGet> GetProductById(ProductToGet product)
        {
            return await ControlGetProductById(product);
        }

        /// <summary>
        /// Private method controls to get a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<List<Product>> ControlGetAllProducts()
        {
            try
            {
                List<Product> productsList = await _productRepository.GetAllProductsAsync();
                return productsList.Count == 0 ? throw new BusinessException(
                    nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException)) : productsList;
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
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await ControlGetAllProducts();
        }

        /// <summary>
        /// Private method controls to get a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<List<Product>> ControlGetProductsPagination(int page)
        {
            try
            {
                List<Product> productsList = await _productRepository.GetProductsPaginationAsync(page);
                return productsList.Count == 0 ? throw new BusinessException(
                    nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException)) : productsList;
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
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }

        public async Task<List<Product>> GetProductsPagination(int page)
        {
            return await ControlGetProductsPagination(page);
        }

        /// <summary>
        /// Calling the business logic from the ProductAdapter
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task ControlUpdateProduct(ProductToGet product)
        {
            try
            {
                await product.ValidateAndThrowsAsync<ProductToGet, ProductUpdateValidator>();
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
        
        public async Task UpdateProduct(ProductToGet product)
        {
            await ControlUpdateProduct(product);
        }
    }
}
