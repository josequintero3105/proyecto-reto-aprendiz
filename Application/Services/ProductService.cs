using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Core.Entities.MongoDB;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using MongoDB.Driver;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IProductRepository _productRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
            catch(BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
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
            catch(BusinessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
