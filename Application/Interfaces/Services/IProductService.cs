using System.Reflection.Metadata;
using Application.DTOs;
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
        public Task CreateProduct(Product product);
        /// <summary>
        /// Defining contract for the business logic into the product service
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task UpdateProduct(ProductToGet product);
        /// <summary>
        /// product getting by id
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task<ProductToGet> GetProductById(string _id);
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public Task<List<Product>> GetAllProducts();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Task<List<Product>> GetProductsPagination(int page);
    }
}
