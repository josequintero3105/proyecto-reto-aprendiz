using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.PlantillaEntitys;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Core.Entities.MongoDB;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        public async Task<Entity> InsertProduct(Product product)
        {
            return new Entity();
        }

        public async Task<Entity> UpdateProduct(Product product)
        {
            return new Entity();
        }
    }
}
