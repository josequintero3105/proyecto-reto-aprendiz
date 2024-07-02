using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entries
{
    public class InvoiceInput
    {
        public string? CustomerId { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
