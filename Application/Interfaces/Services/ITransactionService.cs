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
        Task<TransactionOutput> ProcessTransaction(TransactionInput transactionInput);
        Task<TransactionResponse> GetTransaction(string _id);
    }
}
