using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    }
}
