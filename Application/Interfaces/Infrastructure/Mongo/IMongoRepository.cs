using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.PlantillaEntitys;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IMongoRepository
    {
        Task<CommandResponse<Product>> SaveProductAsync(Product product);
    }
}
