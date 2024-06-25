using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.Entries;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IProductRepository
    {
        /// <summary>
        /// Defining contract for create product in the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductInput> CreateProductAsync(ProductInput product);
        /// <summary>
        /// Defining contract from update product in the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductOutput> UpdateProductAsync(ProductOutput product);
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ProductOutput> GetProductByIdAsync(string _id);
        /// <summary>
        /// List All Products
        /// </summary>
        /// <returns></returns>
        Task<List<ProductOutput>> GetAllProductsAsync();
        /// <summary>
        /// List Products Per Pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Task<List<ProductInput>> GetProductsPaginationAsync(int page, int size);
        /// <summary>
        /// Create customer returns whole the document
        /// </summary>
        /// <param name="productToCreate"></param>
        /// <returns></returns>
        Task<ProductCollection> CreateAsync(ProductOutput productToCreate);
    }
}
