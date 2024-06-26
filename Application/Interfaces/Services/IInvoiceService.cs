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
    public interface IInvoiceService
    {
        /// <summary>
        /// Generate a new invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        Task<InvoiceOutput> GenerateInvoice(InvoiceInput invoice);
        /// <summary>
        /// Generate a new invoice
        /// </summary>
        /// <param name="invoiceInput"></param>
        /// <returns></returns>
        Task<InvoiceCollection> Generate(InvoiceInput invoiceInput);
        /// <summary>
        /// Delete an invoice
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task DeleteInvoice(string _id);
    }
}
