﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TransactionOutput
    {
        public string? _id { get; set; }
        /// <summary>
        ///     TransactionId
        /// </summary>
        public string? Invoice { get; set; }
        /// <summary>
        ///     Description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        ///     PaymentMethod
        /// </summary>
        public PaymentMethod? PaymentMethod { get; set; }
        /// <summary>
        ///     Status
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        ///     Currency    
        /// </summary>
        public string? Currency { get; set; }
        /// <summary>
        ///     Value
        /// </summary>
        public double Value { get; set; }
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
    }
}
