using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class MongoAdapter
    {
        private readonly IContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MongoAdapter(IContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringMongoConnection"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="collectionName"></param>
        public MongoAdapter(string stringMongoConnection, string dataBaseName, string collectionName) 
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName, collectionName);
        }
    }
}
