using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IMongoRepository
    {
        Task<CommandResponse<bool>> SaveProductAsync(Product product);
    }
}
