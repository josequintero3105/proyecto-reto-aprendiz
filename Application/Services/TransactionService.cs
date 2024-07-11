using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.FluentValidations.Extentions;
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
        public async Task<TransactionCollection> CreateTransaction(TransactionInput transactionInput)
        {
            try
            {
                TransactionOutput transactionOutput = new()
                {
                    Invoice = transactionInput.Invoice,
                    Description = transactionInput.Description,
                    Currency = transactionInput.Currency,
                    Value = transactionInput.Value,
                    UrlConfirmation = transactionInput.UrlConfirmation,
                    UrlResponse = transactionInput.UrlResponse,
                };
                return await _transactionRepository.CreateTransactionAsync(transactionOutput);
            }
            catch (BusinessException)
            {
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdNotFound),
                    nameof(GateWayBusinessException.TransactionIdNotFound));
            }
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
                    throw new BusinessException(nameof(GateWayBusinessException.TransactionIdNotFound),
                    nameof(GateWayBusinessException.TransactionIdNotFound));
            }
            else
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdCannotBeNull),
                nameof(GateWayBusinessException.TransactionIdCannotBeNull));
        }
    }
}
