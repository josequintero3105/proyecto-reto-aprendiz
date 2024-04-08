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
        /// 
        /// </summary>
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Price")]
        public double Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Description")]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Category")]
        public string Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("State")]
        public bool State { get; set; }
    }
}
