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

        public async Task<Customer> GetCustomerByIdAsync(string _id)
        {
            var IdFound = Builders<CustomerCollection>.Filter.Eq(c => c._id, _id);
            var result = await _context.CustomerCollection.FindAsync(IdFound);
            return _mapper.Map<Customer>(result.FirstOrDefault());
        }

        public CustomerCollection GetCustomer(Customer customerToFind)
        {
            CustomerCollection customerCollectionToFind = _mapper.Map<CustomerCollection>(customerToFind);
            var IdCustomerFound = Builders<CustomerCollection>.Filter.Eq("_id", ObjectId.Parse(customerCollectionToFind._id));
            return _context.CustomerCollection.Find(IdCustomerFound).FirstOrDefault();
        }

        public async Task<bool> UpdateCustomerAsync(Customer customerToUpdate)
        {
            CustomerCollection collectionToUpdate = _mapper.Map<CustomerCollection>(customerToUpdate);
            var IdFound = Builders<CustomerCollection>.Filter.Eq(c => c._id, collectionToUpdate._id);
            var result = _context.CustomerCollection.Find(IdFound).FirstOrDefault();
            if (result != null)
            {
                var resultUpdate = await _context.CustomerCollection.ReplaceOneAsync(IdFound, collectionToUpdate);
                return resultUpdate.ModifiedCount == 1;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(string _id)
        {
            
            var IdFound = Builders<CustomerCollection>.Filter.Eq(c => c._id, _id);
            var result = _context.CustomerCollection.Find(IdFound).FirstOrDefault();
            if (result != null)
            {
                var resultDelete = await _context.CustomerCollection.DeleteOneAsync(IdFound);
                return resultDelete.DeletedCount == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
