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
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductOutput> GetProductByIdAsync(string _id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<ProductOutput>> GetAllProductsAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<List<ProductInput>> GetProductsPaginationAsync(int page, int size);
    }
}
