using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Tests.Application.Tests.DTOs
{
    public static class InvoiceHelperModel
    {
        public static Invoice GetInvoiceFromCreation()
        {
            return new Invoice
            {
                ShoppingCartId = "66574ea38d0535a677a3e029",
                CustomerId = "664f42e8f14d2a474b61159e"
            };
        }

        public static Invoice GetInvoiceFromCreationCustomerIdInvalid()
        {
            return new Invoice
            {
                ShoppingCartId = "66574ea38d0535a677a3e029",
                CustomerId = "664f42e8f14d2%&$74b61159e"
            };
        }

        public static Invoice GetInvoiceFromCreationShoppingCartIdInvalid()
        {
            return new Invoice
            {
                ShoppingCartId = "66574ea38d0%&$a677a3e029",
                CustomerId = "664f42e8f14d2a474b61159e"
            };
        }
    }
}
