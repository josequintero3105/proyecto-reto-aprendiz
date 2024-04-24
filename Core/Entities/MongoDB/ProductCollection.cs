using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.MongoDB
{
    public class ProductCollection
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
        /// Price
        /// </summary>
        [BsonElement("Price")]
        public double Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [BsonElement("Description")]
        public string? Description { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        [BsonElement("Category")]
        public string? Category { get; set; }
        /// <summary>
        /// State
        /// </summary>
        [BsonElement("State")]
        public bool State { get; set; }
    }
}
