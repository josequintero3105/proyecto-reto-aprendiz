using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.DTOs.Entries;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Core.Entities.MongoDB;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository, ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TransactionCollection> CreateTransaction(TransactionInput transactionInput)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetTransactionById
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TransactionOutput> GetTransactionById(string _id)
        {
            if (!String.IsNullOrEmpty(_id))
            {
                var result = await _transactionRepository.GetTransactionByIdAsync(_id);
                if (result != null)
                    return result;
                else
                    throw new BusinessException(nameof(GateWayBusinessException.CustomerIdNotFound),
                    nameof(GateWayBusinessException.CustomerIdNotFound));
            }
            else
                throw new BusinessException(nameof(GateWayBusinessException.CustomerIdCannotBeNull),
                nameof(GateWayBusinessException.CustomerIdCannotBeNull));
        }
    }
}
