using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Core.Enumerations;

namespace Application.Interfaces.Infrastructure.Commands
{
    public interface ICommandEventRepository
    {
        Task<CommandResponse<T>> ExecuteProductMongo<T>(dynamic data);
        Task<CommandResponse<T>> ExecuteTransactions<T>(TransactionActions transactionActions, dynamic data);
    }
}
