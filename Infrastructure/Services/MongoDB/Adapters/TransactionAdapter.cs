using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using AutoMapper;
using Core.Entities.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class TransactionAdapter : ITransactionRepository
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
        public TransactionAdapter(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Constructor defines parameters for connection to mongodb database
        /// </summary>
        /// <param name="stringMongoConnection"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="mapper"></param>
        public TransactionAdapter(string stringMongoConnection, string dataBaseName, IMapper mapper)
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName);
            _mapper = mapper;
        }
        /// <summary>
        /// CreateTransaction
        /// </summary>
        /// <param name="transactionOutput"></param>
        /// <returns></returns>
        public async Task<TransactionCollection> CreateTransactionAsync(TransactionOutput transactionOutput)
        {
            TransactionCollection transactionCollection = _mapper.Map<TransactionCollection>(transactionOutput);
            await _context.TransactionCollection.InsertOneAsync(transactionCollection);
            return transactionCollection;
        }
        /// <summary>
        /// GetTransactionById
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<TransactionOutput> GetTransactionByIdAsync(string _id)
        {
            var IdFound = Builders<TransactionCollection>.Filter.Eq(c => c._id, _id);
            var result = await _context.TransactionCollection.FindAsync(IdFound);
            return _mapper.Map<TransactionOutput>(result.FirstOrDefault());
        }
    }
}
