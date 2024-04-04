using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        public Task InsertProduct(CreateProduct product);
        public Task UpdateProduct(CreateProduct product);
    }
}
