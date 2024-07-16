using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.Entries;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// CreateTransactionAsync
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        Task<TransactionOutput> CreateTransactionAsync(TransactionInput transactionInput);
        /// <summary>
        /// GetTransactionByIdAsync
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<TransactionOutput> GetTransactionByIdAsync(string _id);
    }
}
