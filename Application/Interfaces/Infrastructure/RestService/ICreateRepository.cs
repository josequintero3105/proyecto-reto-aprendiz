using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;

namespace Application.Interfaces.Infrastructure.RestService
{
    public interface ICreateRepository
    {
        /// <summary>
        /// PostCreateAdapter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<CommandResponse<T>> PostCreateAdapter<T>(dynamic transaction);
    }
}
