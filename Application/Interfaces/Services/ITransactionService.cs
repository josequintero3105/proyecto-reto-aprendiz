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
        /// <param name="headers"></param>
        /// <param name="transactionInput"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> CreateTransaction(string url, TransactionInput transactionInput, IDictionary<string, string> headers);
        /// <summary>
        /// Create Transaction Async
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Task<TransactionResponse> GetTransactionById(string _id);
    }
}
