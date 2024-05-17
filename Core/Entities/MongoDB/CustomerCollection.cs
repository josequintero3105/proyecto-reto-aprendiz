using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Core.Entities.MongoDB
{
    public class CustomerCollection
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
        [BsonElement("Email")]
        public string? Email { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [BsonElement("Phone")]
        public string? Phone { get; set; }
    }
}
