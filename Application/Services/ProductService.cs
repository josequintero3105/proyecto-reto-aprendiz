using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Application.Common.FluentValidations.Validators;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.Interfaces.Infrastructure.Commands;
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

        private readonly IProductRepository _productRepository;
        //private IMongoCollection<ProductCollection> collection;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task CreateProduct(Product product)
        {
            await _productRepository.CreateProductAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
    }
}
