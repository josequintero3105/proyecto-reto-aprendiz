using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Entries;
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
        Task<TransactionCollection> CreateTransaction(TransactionInput transactionInput);
        /// <summary>
        /// Create Transaction Async
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<TransactionOutput> GetTransactionById(string _id);
    }
}
