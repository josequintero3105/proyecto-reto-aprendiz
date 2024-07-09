using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Services.MongoDB
{
    public interface IContext
    {
        public IMongoCollection<ProductCollection> ProductCollection { get; }
        public IMongoCollection<ShoppingCartCollection> ShoppingCartCollection { get; }
        public IMongoCollection<InvoiceCollection> InvoiceCollection { get; }
        public IMongoCollection<CustomerCollection> CustomerCollection { get; }
        public IMongoCollection<TransactionCollection> TransactionCollection { get; }
    }
}
