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
        
        private readonly IMongoDatabase _databaseName;

        public DataBaseContext(string connectionString, string databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            _databaseName = mongoClient.GetDatabase(databaseName);
            
        }

        public static DataBaseContext GetMongoDatabase(string connectionString, string databaseName)
        {
            if (_instance is null)
                lock (SyncLock)
                {
                    _instance ??= new DataBaseContext(connectionString, databaseName);
                }

            return _instance;
        }

        public IMongoCollection<ProductCollection> ProductCollection => _databaseName.GetCollection<ProductCollection>("Product");
        public IMongoCollection<ShoppingCartCollection> ShoppingCartCollection => _databaseName.GetCollection<ShoppingCartCollection>("ShoppingCart");
        public IMongoCollection<InvoiceCollection> InvoiceCollection => _databaseName.GetCollection<InvoiceCollection>("Invoice");
        public IMongoCollection<CustomerCollection> CustomerCollection => _databaseName.GetCollection<CustomerCollection>("Customer");
        public IMongoCollection<TransactionCollection> TransactionCollection => _databaseName.GetCollection<TransactionCollection>("Transaction");
    }
}
