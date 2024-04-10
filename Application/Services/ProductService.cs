using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.PlantillaEntitys;
using Application.Interfaces.Infrastructure.Commands;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Core.Entities.MongoDB;
using FluentValidation.TestHelper;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ICommandRepository _commandRepository;
        public async Task<Entity> InsertProduct(Product request)
        {
            Product product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Description = request.Description,
                Category = request.Category,
                State = request.State
            };

            return new Entity();
        }
    }
}
