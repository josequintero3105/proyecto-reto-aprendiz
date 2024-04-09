using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.Interfaces.Infrastructure.Mongo;
using Core.Entities.MongoDB;
using MongoDB.Bson;
using AutoMapper;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class MongoAdapter : IMongoRepository
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MongoAdapter(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringMongoConnection"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="collectionName"></param>
        public MongoAdapter(string stringMongoConnection, string dataBaseName, IMapper mapper, string collectionName) 
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName, collectionName);
            _mapper = mapper;
        }

        public async Task<CommandResponse<Product>> SaveProductAsync(Product product)
        {
            CommandResponse<Product> commandResponse = new();
            ProductCollection productCollection = _mapper.Map<ProductCollection>(product);
            await _context.ProductCollection.InsertOneAsync(productCollection);
            commandResponse.ToBsonDocument();
            return commandResponse;
        }
    }
}
