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
            double totalPrice = 0;
            for (int i = 0; i < shoppingCartToCreate.Products.Count; i++)
            {
                int newQuantity = 0, QuantityInCart = 0;
                ProductCollection productCollectionToFind = shoppingCartToCreate.Products[i];
                var IdFinded = Builders<ProductCollection>.Filter.Eq("_id", ObjectId.Parse(productCollectionToFind._id));
                var result = _context.ProductCollection.Find(IdFinded).FirstOrDefault();
                
                if (result != null)
                {
                    QuantityInCart = shoppingCartToCreate.Products[i].Quantity;
                    shoppingCartToCreate.Products[i] = result;
                    if (result.Quantity >= QuantityInCart)
                    {
                        newQuantity = result.Quantity - QuantityInCart;
                        totalPrice += shoppingCartToCreate.Products[i].Price * QuantityInCart;
                        result.Quantity = newQuantity;
                        await _context.ProductCollection.ReplaceOneAsync(IdFinded, result);
                    }
                }
            }
            shoppingCartToCreate.PriceTotal = totalPrice;
            ShoppingCartCollection shoppingCartCollectionToCreate = _mapper.Map<ShoppingCartCollection>(shoppingCartToCreate);
            await _context.ShoppingCartCollection.InsertOneAsync(shoppingCartCollectionToCreate);
            return _mapper.Map<ShoppingCart>(shoppingCartToCreate);
        }
    }
}
