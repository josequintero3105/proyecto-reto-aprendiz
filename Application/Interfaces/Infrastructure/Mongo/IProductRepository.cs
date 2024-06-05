using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
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
        Task<Product> CreateProductAsync(Product product);
        /// <summary>
        /// Defining contract from update product in the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(ProductToGet product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductToGet> GetProductByIdAsync(ProductToGet product);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetAllProductsAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<List<Product>> GetProductsPaginationAsync(int page);
    }
}
