using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Entities.MongoDB
{
    public class TransactionCollection
    {
        /// <summary>
        /// _id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? _id {  get; set; }
        /// <summary>
        ///     TransactionId
        /// </summary>
        [BsonElement("Invoice")]
        public string? Invoice { get; set; }
        /// <summary>
        ///     Description
        /// </summary>
        [BsonElement("Description")]
        public string? Description { get; set; }
        /// <summary>
        ///     PaymentMethod
        /// </summary>
        [BsonElement("PaymentMethod")]
        public PaymentMethodCollection? PaymentMethod { get; set; }
        /// <summary>
        ///     Status
        /// </summary>
        [BsonElement("Status")]
        public string? Status { get; set; }
        /// <summary>
        ///     Currency    
        /// </summary>
        [BsonElement("Currency")]
        public string? Currency { get; set; }
        /// <summary>
        ///     Value
        /// </summary>
        [BsonElement("Value")]
        public double Value { get; set; }
        /// <summary>
        ///     UrlResponse
        /// </summary>
        [BsonElement("UrlResponse")]
        public string? UrlResponse { get; set; }
        /// <summary>
        ///     UrlConfirmation
        /// </summary>
        [BsonElement("UrlConfirmacion")]
        public string? UrlConfirmation { get; set; }
        /// <summary>
        ///     MethodConfirmation
        /// </summary>
        [BsonElement("MethodConfirmation")]
        public string? MethodConfirmation { get; set; }
    }
}
