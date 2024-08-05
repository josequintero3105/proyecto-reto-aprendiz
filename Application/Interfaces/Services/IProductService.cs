using System.Reflection.Metadata;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
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
        Task<ProductCollection> CreateProduct(ProductInput productInput);
        /// <summary>
        /// Defining contract for the business logic into the product service
        /// </summary>
        /// <param name="productInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ProductOutput> UpdateProduct(ProductInput productInput, string _id);
        /// <summary>
        /// product getting by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ProductOutput> GetProductById(string _id);
        /// <summary>
        /// List all products
        /// </summary>
        /// <returns></returns>
        Task<List<ProductOutput>> ListProducts();
        /// <summary>
        /// List Products Per Pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<ProductOutput>> ListProductsPerPage(string page, string size);
    }
}
