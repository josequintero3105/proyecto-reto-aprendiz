using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// CreateTransaction
        /// </summary>
        /// <param name="transactionOutput"></param>
        /// <returns></returns>
        Task<TransactionCollection> CreateTransactionAsync(TransactionOutput transactionOutput);
        /// <summary>
        /// GetTrasactionById
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<TransactionOutput> GetTransactionByIdAsync(string _id);
    }
}
