using System.Reflection.Metadata;
using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        public Task CreateProduct(Product product);
        public Task UpdateProduct(Product product);
    }
}
