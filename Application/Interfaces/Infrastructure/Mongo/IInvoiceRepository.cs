using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Generate a new invoice for testing
        /// </summary>
        /// <param name="invoiceInput"></param>
        /// <returns></returns>
        Task<InvoiceOutput> GenerateInvoiceAsync(InvoiceOutput invoiceInput);
        /// <summary>
        /// Generate a new invoice
        /// </summary>
        /// <param name="InvoiceToCreate"></param>
        /// <returns></returns>
        Task<InvoiceCollection> GenerateAsync(InvoiceOutput InvoiceToCreate);
    }
}
