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
        public Task<ProductToGet> GetProductById(ProductToGet product);
        public Task<List<Product>> GetAllProducts();
    }
}
