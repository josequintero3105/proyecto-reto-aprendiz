using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Entities.MongoDB
{
    public class InvoiceCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? _id { get; set; }

        [BsonElement("CustomerId")]
        public string? CostumerId { get; set; }

        [BsonElement("ShoppingCartId")]
        public string? ShoppingCartId { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("PaymentMethod")]
        public string? PaymentMethod { get; set; }
    }
}
