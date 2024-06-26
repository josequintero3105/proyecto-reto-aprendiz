using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Entities.MongoDB
{
    public class ProductInCartCollection
    {
        /// <summary>
        /// _id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? _id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [BsonElement("Name")]
        public string? Name { get; set; }
        /// <summary>
        /// UnitPrice
        /// </summary>
        [BsonElement("UnitPrice")]
        public double UnitPrice { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [BsonElement("QuantityInCart")]
        public double QuantityInCart { get; set; }
    }
}
