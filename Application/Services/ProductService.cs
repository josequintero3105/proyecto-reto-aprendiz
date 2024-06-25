using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
        /// <param name="productInput"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ProductCollection> CreateProduct(ProductInput productInput)
        {
            try
            {
                await productInput.ValidateAndThrowsAsync<ProductInput, ProductValidator>();
                ProductOutput productOutput = new()
                {
                    Name = productInput.Name,
                    Price = productInput.Price,
                    Quantity = productInput.Quantity,
                    Description = productInput.Description,
                    Category = productInput.Category,
                    State = productInput.State,
                };

                if (productInput.Quantity >= 0 && productInput.Price >= 0 
                    && productInput.Quantity <= Int32.MaxValue
                    && productInput.Price <= Int32.MaxValue)
                    return await _productRepository.CreateAsync(productOutput);
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid),
                    nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating product: {productInput}"
                    , bex.Code, bex.Message, productInput);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating product: {productInput} ", ex.Message, productInput);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }

        /// <summary>
        /// Private method controls to get a product by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ProductOutput> GetProductById(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                    return await _productRepository.GetProductByIdAsync(_id);
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ProductIdCannotBeNull),
                    nameof(GateWayBusinessException.ProductIdCannotBeNull));
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

        /// <summary>
        /// Private method controls to list all products
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<List<ProductOutput>> GetAllProducts()
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

        private static bool IsValidInteger(string input)
        {
            var regex = new Regex(@"^[+-]?\d+$");
            if (regex.IsMatch(input) && int.TryParse(input, out _))
                return true;
            return false;
        }

        /// <summary>
        /// Private method controls to get a product from pages
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<List<ProductInput>> GetProductsPagination(string page, string size)
        {
            try
            {
                if (IsValidInteger(page) && IsValidInteger(size))
                {
                    int pageInt = Convert.ToInt32(page);
                    int sizeInt = Convert.ToInt32(size);
                    List<ProductInput> productsList = await _productRepository.GetProductsPaginationAsync(pageInt, sizeInt);
                    return productsList.Count == 0 ? throw new BusinessException(
                        nameof(GateWayBusinessException.ProductListCannotBeNull),
                        nameof(GateWayBusinessException.ProductListCannotBeNull)) : productsList;
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.PaginationParametersNotValid),
                    nameof(GateWayBusinessException.PaginationParametersNotValid));
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

        /// <summary>
        /// Update product data searching by id
        /// </summary>
        /// <param name="product"></param>
        /// <param name="_id"></param>
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
                if (!String.IsNullOrEmpty(_id))
                {
                    if (product.Quantity >= 0 && product.Price >= 0
                    && product.Quantity <= Int32.MaxValue
                    && product.Price <= Int32.MaxValue)
                        return await _productRepository.UpdateProductAsync(productOutput);
                    else
                        throw new BusinessException(nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid),
                        nameof(GateWayBusinessException.ProductQuantityOrPriceInvalid));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ProductIdCannotBeNull),
                    nameof(GateWayBusinessException.ProductIdCannotBeNull));
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
