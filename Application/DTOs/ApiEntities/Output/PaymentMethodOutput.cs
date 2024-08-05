using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiEntities.Output
{
    public class PaymentMethodOutput
    {
        public string? PaymentMethodId { get; set; }
        public int BankCode { get; set; }
        public string? BankName { get; set; }
    }
}
