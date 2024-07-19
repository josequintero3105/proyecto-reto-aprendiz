﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;

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
        ///     Store
        /// </summary>
        public string? StoreId {  get; set; }
        /// <summary>
        ///     VendorId
        /// </summary>
        public string? VendorId { get; set; }
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
        ///     Sandbox
        /// </summary>
        public Sandbox? sandbox { get; set; }
        /// <summary>
        ///     CreationDate
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        ///     PaymentMethodResponse
        /// </summary>
        public PaymentMethodResponse? PaymentMethodResponse { get; set; }
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
