using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class TransactionResponse
    {
        /// <summary>
        ///     _id
        /// </summary>
        public string? _id { get; set; }
        /// <summary>
        ///     Invoice
        /// </summary>
        public string? invoice { get; set; }
        /// <summary>
        ///     StoreId
        /// </summary>
        public string? storeId { get; set; }
        /// <summary>
        /// VendorId
        /// </summary>
        public string? vendorId { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string? description { get; set; }
        /// <summary>
        ///     paymentMethod
        /// </summary>
        public PaymentMethod? paymentMethod { get; set; }
        /// <summary>
        ///     TransactionStatus
        /// </summary>
        public string? transactionStatus { get; set; }
        /// <summary>
        ///     Currency
        /// </summary>
        public string? currency { get; set; }
        /// <summary>
        ///     Value
        /// </summary>
        public double value { get; set; }
        /// <summary>
        ///     SandBox
        /// </summary>
        public Sandbox? sandbox { get; set; }
        /// <summary>
        ///     CreationDate
        /// </summary>
        public DateTime? creationDate { get; set; }
        /// <summary>
        ///     UrlResponse
        /// </summary>
        public string? UrlResponse { get; set; }
        /// <summary>
        ///     UrlConfirmation
        /// </summary>
        public string? UrlConfirmation { get; set; }
        /// <summary>
        ///     MethodConfirmation
        /// </summary>
        public string? MethodConfirmation { get; set; }
        /// <summary>
        ///     PaymentMethodResponse
        /// </summary>
        public PaymentMethodResponse? paymentMethodResponse { get; set; }
    }
}
