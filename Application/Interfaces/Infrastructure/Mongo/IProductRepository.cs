using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IProductRepository
    {
        Task<CommandResponse<Product>> CreateProductAsync(Product product);
        Task<CommandResponse<Product>> UpdateProductAsync(Product product);
    }
}
