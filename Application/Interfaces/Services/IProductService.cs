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
        /// <param name="productInput"></param>
        /// <returns></returns>
        public Task<ProductCollection> CreateProduct(ProductInput productInput);
        /// <summary>
        /// Defining contract for the business logic into the product service
        /// </summary>
        /// <param name="productInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Task<ProductOutput> UpdateProduct(ProductInput productInput, string _id);
        /// <summary>
        /// product getting by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Task<ProductOutput> GetProductById(string _id);
        /// <summary>
        /// List all products
        /// </summary>
        /// <returns></returns>
        public Task<List<ProductOutput>> GetAllProducts();
        /// <summary>
        /// List Products Per Pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Task<List<ProductInput>> GetProductsPagination(string page, string size);
    }
}
