using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiEntities.Input
{
    public class PaymentMethod
    {
        /// <summary>
        ///     PaymentMethodId
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        ///     BankCode
        /// </summary>
        public int BankCode { get; set; }
        /// <summary>
        ///     UserType
        /// </summary>
        public int UserType { get; set; }
    }
}
