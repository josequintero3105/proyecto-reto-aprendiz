using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Entities.MongoDB
{
    public class ShoppingCartCollection
    {
        /// <summary>
        /// _id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? _id { get; set; }

        [BsonElement("CustomerId")]
        public string? CustomerId { get; set; }

        [BsonElement("ProductsInCart")]
        public List<ProductInCartCollection>? ProductsInCart { get; set; }

        [BsonElement("PriceTotal")]
        public double PriceTotal { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Active")]
        public bool Active {  get; set; }
    }
}
