using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;

namespace Application.Interfaces.Infrastructure.Commands
{
    public interface ICommandRepository
    {
        Task<CommandResponse<T>> ExecuteProductMongo<T>(dynamic data);
    }
}
