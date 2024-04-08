using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Services.MongoDB
{
    public class DataBaseContext : IContext
    {
        private static volatile DataBaseContext? _instance;
        private static readonly object SyncLock = new object();
        private readonly string _collectionName;
        private readonly IMongoDatabase _databaseName;

        public DataBaseContext(string connectionString, string databaseName, string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            _databaseName = mongoClient.GetDatabase(databaseName);
            _collectionName = collectionName;
        }

        public static DataBaseContext GetMongoDatabase(string connectionString, string databaseName, string collectionName)
        {
            if (_instance is null)
                lock (SyncLock)
                {
                    _instance ??= new DataBaseContext(connectionString, databaseName, collectionName);
                }

            return _instance;
        }

        public IMongoCollection<ProductCollection> ProductCollection => _databaseName.GetCollection<ProductCollection>(_collectionName);
    }
}
