using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class Invoice
    {
        public string? CustomerId { get; set; }
        public string? ShoppingCartId { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Total { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
