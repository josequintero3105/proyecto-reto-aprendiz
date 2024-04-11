using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Application.Interfaces.Infrastructure.Commands;
using Application.Interfaces.Infrastructure.Mongo;

namespace Application.Common.Helpers.Commands
{
    public class CommandEvent : ICommandRepository
    {
        private readonly IProductRepository _productRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        public CommandEvent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        /// <summary>
        /// Executes the wompi bancolombia driver transaction mongo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task<CommandResponse<T>> ExecuteProductMongo<T>(dynamic data)
        {
            CommandResponse<T> response = new();
            return await _productRepository.CreateProductAsync(data);
        }
    }
}
