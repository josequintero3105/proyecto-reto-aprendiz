using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using AutoMapper;
using Core.Entities.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class CustomerAdapter : ICustomerRepository
    {

        /// <summary>
        /// Variables
        /// </summary>
        private readonly IContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor defines the DataBase context and the mapper between product and productCollection
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public CustomerAdapter(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Constructor defines parameters for connection to mongodb database
        /// </summary>
        /// <param name="stringMongoConnection"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="mapper"></param>
        public CustomerAdapter(string stringMongoConnection, string dataBaseName, IMapper mapper)
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName);
            _mapper = mapper;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customerToCreate)
        {
            CustomerCollection customerCollectionToCreate = _mapper.Map<CustomerCollection>(customerToCreate);
            await _context.CustomerCollection.InsertOneAsync(customerCollectionToCreate);
            return _mapper.Map<Customer>(customerToCreate);
        }

        public async Task<Customer> GetCustomerByIdAsync(Customer customerToGet)
        {
            CustomerCollection pCollection = _mapper.Map<CustomerCollection>(customerToGet);
            var IdFinded = Builders<CustomerCollection>.Filter.Eq("_id", ObjectId.Parse(pCollection._id));
            var result = await _context.CustomerCollection.FindAsync(IdFinded);
            return _mapper.Map<Customer>(result.FirstOrDefault());
        }

        public async Task<bool> UpdateCustomerAsync(Customer customerToUpdate)
        {
            CustomerCollection collectionToUpdate = _mapper.Map<CustomerCollection>(customerToUpdate);
            var IdFinded = Builders<CustomerCollection>.Filter.Eq("_id", ObjectId.Parse(collectionToUpdate._id));
            var result = _context.CustomerCollection.Find(IdFinded).FirstOrDefault();
            if (result != null)
            {
                var resultUpdate = await _context.CustomerCollection.ReplaceOneAsync(IdFinded, collectionToUpdate);
                return resultUpdate.ModifiedCount == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
