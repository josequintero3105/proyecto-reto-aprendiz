using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.ApiEntities.Input;
using Application.DTOs.ApiEntities.Output;
using Application.DTOs.ApiEntities.Response;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// Process Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        Task<TransactionOutput> ProcessTransaction(TransactionInput transactionInput);
        /// <summary>
        /// Get Transaction Response
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<TransactionResponse> GetTransaction(string _id);
    }
}
