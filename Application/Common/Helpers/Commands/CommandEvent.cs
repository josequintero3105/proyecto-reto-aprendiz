using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Application.Interfaces.Infrastructure.Commands;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Infrastructure.RestService;
using Core.Enumerations;

namespace Application.Common.Helpers.Commands
{
    public class CommandEvent : ICommandEventRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly ICreateRepository _createRepository;
        private readonly IGetRepository _getRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        public CommandEvent(IProductRepository productRepository,
            IGetRepository getRepository, ICreateRepository createRepository)
        {
            _productRepository = productRepository;
            _getRepository = getRepository;
            _createRepository = createRepository;
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

        /// <summary>
        /// Execute Transactions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transactionActions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CommandResponse<T>> ExecuteCreateTransaction<T>(dynamic data)
        {
            CommandResponse<T> response = await _createRepository.PostCreateAdapter<T>(data);     
            return response;
        }

        public async Task<CommandResponse<T>> ExecuteGetTransaction<T>(dynamic data, NameValueCollection _id)
        {
            CommandResponse<T> response = await _getRepository.GetTAdapter(data, _id);
            return response;
        }
    }
}
