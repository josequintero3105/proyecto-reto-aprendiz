using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Services;
using AutoMapper;
using Core.Entities.MongoDB;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class ShoppingCartAdapter : IShoppingCartRepository
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
        public ShoppingCartAdapter(IContext context, IMapper mapper)
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
        public ShoppingCartAdapter(string stringMongoConnection, string dataBaseName, IMapper mapper)
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName);
            _mapper = mapper;
        }

        /// <summary>
        /// Business logic create product
        /// </summary>
        /// <param name="shoppingCartToCreate"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCartToCreate)
        {
            ShoppingCartCollection shoppingCartCollectionToCreate = _mapper.Map<ShoppingCartCollection>(shoppingCartToCreate);
            await _context.ShoppingCartCollection.InsertOneAsync(shoppingCartCollectionToCreate);
            return _mapper.Map<ShoppingCart>(shoppingCartToCreate);
        }

        /// <summary>
        /// Getting The shopping cart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> GetShoppingCartAlter(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            var filter = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id));
            var resultCart = await _context.ShoppingCartCollection.Find(filter).FirstOrDefaultAsync();
            return _mapper.Map<ShoppingCart>(resultCart);
        }

        /// <summary>
        /// Get an specific shoppingCart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public ShoppingCartCollection GetShoppingCart(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            var IdCartFinded = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id));
            var resultCart = _context.ShoppingCartCollection.Find(IdCartFinded).FirstOrDefault();
            return resultCart;
        }

        /// <summary>
        /// Get the product collection only for update it's quantity
        /// </summary>
        /// <param name="productsToAdd"></param>
        /// <returns></returns>
        public async Task<bool> UpdateQuantityForProduct(ProductCollection productsToAdd)
        {
            var IdFinded = Builders<ProductCollection>.Filter.Eq("_id", ObjectId.Parse(productsToAdd._id));
            var resultUpdate = await _context.ProductCollection.ReplaceOneAsync(IdFinded, productsToAdd);
            return resultUpdate.ModifiedCount == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BulkWriteQuantities"></param>
        /// <param name="products"></param>
        public void FilterToGetProduct(List<WriteModel<ProductCollection>> BulkWriteQuantities, ProductCollection products)
        {
            var filter = Builders<ProductCollection>.Filter.Eq(x => x._id, products._id);
            var update = Builders<ProductCollection>.Update.Set(a => a.Quantity, products.Quantity);
            BulkWriteQuantities.Add(new UpdateOneModel<ProductCollection>(filter, update) { IsUpsert = true });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BulkWriteQuantities"></param>
        /// <returns></returns>
        public async Task UpdateQuantitiesForProducts(List<WriteModel<ProductCollection>> BulkWriteQuantities)
        {
            await _context.ProductCollection.BulkWriteAsync(BulkWriteQuantities);
        }

        /// <summary>
        /// List specific products to add and delete
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<List<ProductCollection>> ListSpecificProducts(List<string> productIds)
        {    
            var idFinded = Builders<ProductCollection>.Filter.In(x => x._id, productIds);
            var result = await _context.ProductCollection.Find(idFinded).ToListAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task RemoveProductFromCartAsync(ShoppingCartCollection shoppingCart, string _id)
        {
            var filter = Builders<ShoppingCartCollection>.Filter.Eq(c => c._id, shoppingCart._id);
            var remove = Builders<ShoppingCartCollection>.Update.PullFilter(c => c.ProductsInCart, p => p._id == _id);
            await _context.ShoppingCartCollection.UpdateOneAsync(filter, remove);
        }

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShoppingCartAsync(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            var IdCartFinded = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id));
            _context.ShoppingCartCollection.Find(IdCartFinded).FirstOrDefault();
            var resultAdd = await _context.ShoppingCartCollection.ReplaceOneAsync(IdCartFinded, shoppingCartCollectionToFind);
            return resultAdd.ModifiedCount == 1;
        }
    }
}
