using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;

namespace Application.Interfaces.Infrastructure.RestService
{
    public interface ITokenRepository
    {
        public Task<CommandResponse<T>> GetTokenAdapter<T>(dynamic request);
    }
}
