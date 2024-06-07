using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Common.FluentValidations.Validators;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Services;
using AutoMapper;
using Core.Entities.MongoDB;
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
        public async Task<ShoppingCart> GetShoppingCartAsync(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection spCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            var filter = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(spCollectionToFind._id));
            var resultCart = await _context.ShoppingCartCollection.FindAsync(filter);
            return _mapper.Map<ShoppingCart>(resultCart.FirstOrDefault());
        }

        public async Task<bool> GetShoppingCartFromMongo(string _id)
        {
            var filter = Builders<ShoppingCartCollection>.Filter.Eq(s => s._id, _id);
            var shoppingCartCollection = await _context.ShoppingCartCollection.Find(filter).FirstOrDefaultAsync();
            return shoppingCartCollection != null;
        }

        /// <summary>
        /// Get an specific shoppingCart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public ShoppingCartCollection GetShoppingCart(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            var filter = Builders<ShoppingCartCollection>.Filter.And(
                Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id)),
                Builders<ShoppingCartCollection>.Filter.Eq(x => x.Active, true)
            );
            return _context.ShoppingCartCollection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Get the product collection only for update it's quantity
        /// </summary>
        /// <param name="productsToAdd"></param>
        /// <returns></returns>
        public async Task<bool> UpdateQuantityForProduct(ProductCollection productsToAdd)
        {
            var filter = Builders<ProductCollection>.Filter.Eq("_id", ObjectId.Parse(productsToAdd._id));
            var resultUpdate = await _context.ProductCollection.ReplaceOneAsync(filter, productsToAdd);
            return resultUpdate.ModifiedCount == 1;
        }

        /// <summary>
        /// Filter the products to do it the update process
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <param name="products"></param>
        public void FilterToGetProduct(List<WriteModel<ProductCollection>> listModelProducts, ProductCollection products)
        {
            var filter = Builders<ProductCollection>.Filter.Eq(x => x._id, products._id);
            var update = Builders<ProductCollection>.Update.Set(a => a.Quantity, products.Quantity);
            listModelProducts.Add(new UpdateOneModel<ProductCollection>(filter, update) { IsUpsert = true });
        }

        /// <summary>
        /// Update the product quantities in a just iterator
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <returns></returns>
        public async Task UpdateQuantitiesForProducts(List<WriteModel<ProductCollection>> listModelProducts)
        {
            await _context.ProductCollection.BulkWriteAsync(listModelProducts);
        }

        /// <summary>
        /// List specific products to add and delete
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<List<ProductCollection>> ListSpecificProducts(List<string> productIds)
        {
            var filter = Builders<ProductCollection>.Filter.And(
                Builders<ProductCollection>.Filter.In(x => x._id, productIds),
                Builders<ProductCollection>.Filter.Eq(x => x.State, true)
            );
            return await _context.ProductCollection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Take the shopping cart and take one of its products for remove
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public async Task RemoveProductFromCartAsync(ShoppingCartCollection shoppingCartCollection, string _id)
        {
            var filter = Builders<ShoppingCartCollection>.Filter.Eq(c => c._id, shoppingCartCollection._id);
            var remove = Builders<ShoppingCartCollection>.Update.PullFilter(c => c.ProductsInCart, p => p._id == _id);
            await _context.ShoppingCartCollection.UpdateOneAsync(filter, remove);
        }

        /// <summary>
        /// Add another product into the cart
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <param name="productInCart"></param>
        /// <returns></returns>
        public async Task AddAnotherProductInCartAsync(ShoppingCartCollection shoppingCartCollection, ProductInCart productInCart)
        {
            ProductInCartCollection productInCartCollection = _mapper.Map<ProductInCartCollection>(productInCart);
            var filter = Builders<ShoppingCartCollection>.Filter.Eq(c => c._id, shoppingCartCollection._id);
            var add = Builders<ShoppingCartCollection>.Update.Push(c => c.ProductsInCart, productInCartCollection);
            await _context.ShoppingCartCollection.UpdateOneAsync(filter, add);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePriceTotalFromShoppingCart(ShoppingCartCollection shoppingCartCollection)
        {
            var filter = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollection._id));
            _context.ShoppingCartCollection.Find(filter).FirstOrDefault();
            var resultAdd = await _context.ShoppingCartCollection.ReplaceOneAsync(filter, shoppingCartCollection);
            return resultAdd.ModifiedCount == 1;
        }

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShoppingCartAsync(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            var filter = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id));
            _context.ShoppingCartCollection.Find(filter).FirstOrDefault();
            var resultAdd = await _context.ShoppingCartCollection.ReplaceOneAsync(filter, shoppingCartCollectionToFind);
            return resultAdd.ModifiedCount == 1;
        }
    }
}
