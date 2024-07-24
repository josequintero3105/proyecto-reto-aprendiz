using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// CreateTransaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        Task<TransactionOutput> CreateTransaction(TransactionInput transactionInput);
        /// <summary>
        /// Create Transaction Async
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetTransactionById(dynamic path, dynamic _id);

        Task<TransactionOutput> ProcessTransaction(TransactionInput transactionInput);
        Task<TransactionResponse> GetTransaction(string _id);
    }
}
