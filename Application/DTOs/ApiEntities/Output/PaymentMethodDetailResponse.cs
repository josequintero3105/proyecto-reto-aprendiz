using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiEntities.Output
{
    public class PaymentMethodDetailResponse
    {
        /// <summary>
        ///     transactionId
        /// </summary>
        public string? TransactionId { get; set; }
        /// <summary>
        ///     statusResponse
        /// </summary>
        public string? StatusResponse { get; set; }
        /// <summary>
        ///     codeResponse
        /// </summary>
        public string? CodeResponse { get; set; }
        /// <summary>
        ///     description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        ///     autorizationCode
        /// </summary>
        public string? AuthorizationCode { get; set; }
        /// <summary>
        ///     approvalCode
        /// </summary>
        public string? ApprovalCode { get; set; }
        /// <summary>
        ///     receipt
        /// </summary>
        public string? Receipt { get; set; }
    }
}
