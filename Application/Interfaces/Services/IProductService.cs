using System.Reflection.Metadata;
using Application.DTOs;
using Application.DTOs.Entries;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Defining contract for the business logic into the product service 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task CreateProduct(ProductInput product);
        /// <summary>
        /// Defining contract for the business logic into the product service
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task<ProductOutput> UpdateProduct(ProductInput product, string _id);
        /// <summary>
        /// product getting by id
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task<ProductOutput> GetProductById(string _id);
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public Task<List<ProductOutput>> GetAllProducts();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<List<ProductInput>> GetProductsPagination(int page, int size);
    }
}
