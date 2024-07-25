using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiEntities.Response
{
    public class PaymentMethodResponse
    {
        /// <summary>
        ///     transactionId
        /// </summary>
        public string? transactionId { get; set; }
        /// <summary>
        ///     driverId
        /// </summary>
        public string? driverId { get; set; }
        /// <summary>
        ///     statusResponse
        /// </summary>
        public string? statusResponse { get; set; }
        /// <summary>
        ///     codeResponse
        /// </summary>
        public string? codeResponse { get; set; }
        /// <summary>
        ///     description
        /// </summary>
        public string? description { get; set; }
        /// <summary>
        ///     autorizationCode
        /// </summary>
        public string? authorizationCode { get; set; }
        /// <summary>
        ///     approvalCode
        /// </summary>
        public string? approvalCode { get; set; }
        /// <summary>
        ///     receipt
        /// </summary>
        public string? receipt { get; set; }
        /// <summary>
        ///     paymentRedirectUrl
        /// </summary>
        public string? paymentRedirectUrl { get; set; }
    }
}
