using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Application.Interfaces.Infrastructure.Mongo;

namespace Application.Common.Helpers.Commands
{
    public class CommandEvent
    {
        private readonly IMongoRepository _repository;
        /// <summary>
        /// Executes the wompi bancolombia driver transaction mongo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wompiBancolombiaDriverTransactionMongoActions">The wompi bancolombia driver transaction mongo actions.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task<CommandResponse<T>> ExecuteProductMongo<T>(dynamic data)
        {
            CommandResponse<T> response = new();
            return response = await _repository.SaveProductAsync(data);
        }
    }
}
