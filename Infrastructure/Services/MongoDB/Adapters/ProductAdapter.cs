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
using MongoDB.Driver;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class ProductAdapter : IProductRepository
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ProductAdapter(IContext context, IMapper mapper)
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
        public ProductAdapter(string stringMongoConnection, string dataBaseName, string collectionName, IMapper mapper) 
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName, collectionName);
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<CommandResponse<Product>> CreateProductAsync(Product product)
        {
            CommandResponse<Product> commandResponse = new();
            ProductCollection productCollection = _mapper.Map<ProductCollection>(product);
            await _context.ProductCollection.InsertOneAsync(productCollection);
            commandResponse.ToBsonDocument();
            return commandResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<CommandResponse<Product>> UpdateProductAsync(Product product)
        {
            ProductCollection productCollection = _mapper.Map<ProductCollection>(product);
            var IdFinded = Builders<ProductCollection>.Filter.Eq(s => s.Id, productCollection.Id);
            CommandResponse<Product> commandResponse = new();
            await _context.ProductCollection.ReplaceOneAsync(IdFinded, productCollection);
            commandResponse.ToBsonDocument();
            return commandResponse;
        }
    }
}
