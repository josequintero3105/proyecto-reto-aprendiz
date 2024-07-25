using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.ApiEntities.Input;
using Application.DTOs.ApiEntities.Output;

namespace Application.DTOs.ApiEntities.Response
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
        public string? Invoice { get; set; }
        /// <summary>
        ///     StoreId
        /// </summary>
        public string? StoreId { get; set; }
        /// <summary>
        /// VendorId
        /// </summary>
        public string? VendorId { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        ///     paymentMethod
        /// </summary>
        public PaymentMethodOutput? PaymentMethod { get; set; }
        /// <summary>
        ///     TransactionStatus
        /// </summary>
        public string? TransactionStatus { get; set; }
        /// <summary>
        ///     Currency
        /// </summary>
        public string? Currency { get; set; }
        /// <summary>
        ///     Value
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        ///     SandBox
        /// </summary>
        public SandboxInactive? Sandbox { get; set; }
        /// <summary>
        ///     CreationDate
        /// </summary>
        public DateTime? CreationDate { get; set; }
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
        public PaymentMethodResponse? PaymentMethodResponse { get; set; }
    }
}
