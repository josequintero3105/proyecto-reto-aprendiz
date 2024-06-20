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
using System.Drawing;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class ProductAdapter : IProductRepository
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
        public ProductAdapter(IContext context, IMapper mapper)
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
        public ProductAdapter(string stringMongoConnection, string dataBaseName, string collectionName, IMapper mapper) 
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName);
            _mapper = mapper;
        }

        /// <summary>
        /// Business logic create product
        /// </summary>
        /// <param name="productToCreate"></param>
        /// <returns></returns>
        public async Task<Product> CreateProductAsync(Product productToCreate)
        {
            ProductCollection productCollectionToCreate = _mapper.Map<ProductCollection>(productToCreate);
            await _context.ProductCollection.InsertOneAsync(productCollectionToCreate);
            return _mapper.Map<Product>(productToCreate);
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task<ProductToGet> GetProductByIdAsync(string _id)
        {
            var IdFound = Builders<ProductCollection>.Filter.Eq(x => x._id, _id);
            var result = await _context.ProductCollection.FindAsync(IdFound);
            return _mapper.Map<ProductToGet>(result.FirstOrDefault());
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductToGet>> GetAllProductsAsync()
        {
            var result = await _context.ProductCollection.FindAsync(Builders<ProductCollection>.Filter.Eq(x => x.State, true));
            return _mapper.Map<List<ProductToGet>>(result.ToList());
        }

        /// <summary>
        /// Get Products for pages
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<Product>> GetProductsPaginationAsync(int page, int size)
        {
            var result = await _context.ProductCollection.Find(Builders<ProductCollection>.Filter.Eq(x => x.State, true))
                .Skip((page - 1) * size).Limit(size).ToListAsync();
            return _mapper.Map<List<Product>>(result.ToList());
        }

        /// <summary>
        /// Business logic update product
        /// </summary>
        /// <param name="productToUpdate"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(ProductToGet productToUpdate)
        {
            ProductCollection productCollectionToUpdate = _mapper.Map<ProductCollection>(productToUpdate); 
            var IdFound = Builders<ProductCollection>.Filter.Eq("_id", ObjectId.Parse(productCollectionToUpdate._id));
            var result = _context.ProductCollection.Find(IdFound).FirstOrDefault();
            if (result != null)
            {
                var resultUpdate = await _context.ProductCollection.ReplaceOneAsync(IdFound, productCollectionToUpdate);
                return resultUpdate.ModifiedCount == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
