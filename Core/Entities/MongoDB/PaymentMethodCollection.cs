using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Entities.MongoDB
{
    public class PaymentMethodCollection
    {
        /// <summary>
        /// _id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? _id { get; set; }

        /// <summary>
        /// PaymentMethodId
        /// </summary>
        [BsonElement("PaymentMethodId")]
        public int PaymentMethodId { get; set; }

        [BsonElement("BankCode")]
        public int BankCode { get; set; }

        [BsonElement("UserType")]
        public int UserType { get; set; }
    }
}
