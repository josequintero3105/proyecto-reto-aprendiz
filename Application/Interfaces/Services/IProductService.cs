using System.Reflection.Metadata;
using Application.DTOs;
using Application.DTOs.PlantillaEntitys;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        public Task<Entity> InsertProduct(Product product);
        
    }
}
