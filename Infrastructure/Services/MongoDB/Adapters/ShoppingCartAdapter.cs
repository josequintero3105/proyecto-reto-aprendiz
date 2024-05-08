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
            ShoppingCartCollection shoppingCartCollectionToCreate = _mapper.Map<ShoppingCartCollection>(shoppingCartToCreate);
            await _context.ShoppingCartCollection.InsertOneAsync(shoppingCartCollectionToCreate);
            return _mapper.Map<ShoppingCart>(shoppingCartToCreate);
        }

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public async Task<bool> RemoveFromShoppingCartAsync(ShoppingCart shoppingCartToFind)
        {
            return await RemoveFromCartLogic(shoppingCartToFind);
        }

        private async Task<bool> RemoveFromCartLogic(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            
            var IdCartFinded = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id));
            var resultCart = _context.ShoppingCartCollection.Find(IdCartFinded).FirstOrDefault();
            string IdProductFinded = "";
            double NewTotalPrice = 0;

            if (resultCart != null && shoppingCartToFind.ProductsInCart.Count == 1)
            {
                for (int i = 0; i < resultCart.ProductsInCart.Count; i++)
                {
                    if (resultCart.ProductsInCart[i]._id == shoppingCartToFind.ProductsInCart[0]._id)
                    {
                        IdProductFinded = resultCart.ProductsInCart[i]._id;
                        NewTotalPrice = resultCart.ProductsInCart[i].UnitPrice * resultCart.ProductsInCart[i].QuantityInCart;
                        break;
                    }
                }
                resultCart.PriceTotal -= NewTotalPrice;
                shoppingCartCollectionToFind.PriceTotal = resultCart.PriceTotal;
                resultCart.ProductsInCart.RemoveAll(x => x._id == IdProductFinded);
                shoppingCartCollectionToFind = resultCart;

                var resultRemove = await _context.ShoppingCartCollection.ReplaceOneAsync(IdCartFinded, shoppingCartCollectionToFind);
                return resultRemove.ModifiedCount == 1;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="shoppingCartToFind"></param>
        /// <returns></returns>
        public async Task<bool> AddToShoppingCartAsync(ShoppingCart shoppingCartToFind)
        {
            return await AddToCartLogic(shoppingCartToFind);
        }

        private async Task<bool> AddToCartLogic(ShoppingCart shoppingCartToFind)
        {
            ShoppingCartCollection shoppingCartCollectionToFind = _mapper.Map<ShoppingCartCollection>(shoppingCartToFind);
            List<ProductCollection> productsInStock = _context.ProductCollection.Find(new BsonDocument()).ToList();
            var IdCartFinded = Builders<ShoppingCartCollection>.Filter.Eq("_id", ObjectId.Parse(shoppingCartCollectionToFind._id));
            var resultCart = _context.ShoppingCartCollection.Find(IdCartFinded).FirstOrDefault();

            if (resultCart != null)
            {
                double totalPrice = resultCart.PriceTotal;
                for (int j = 0; j < shoppingCartToFind.ProductsInCart.Count; j++)
                {
                    for (int i = 0; i < productsInStock.Count; i++)
                    {
                        int NewQuantity = 0, QuantityInCart = 0;
                        ProductCollection productCollectionToFind = productsInStock[i];
                        var IdFinded = Builders<ProductCollection>.Filter.Eq("_id", ObjectId.Parse(productCollectionToFind._id));
                        var result = _context.ProductCollection.Find(IdFinded).FirstOrDefault();

                        if (productsInStock[i]._id == shoppingCartToFind.ProductsInCart[j]._id)
                        {
                            if (result != null)
                            {
                                shoppingCartToFind.ProductsInCart[j].Name = result.Name;
                                shoppingCartCollectionToFind.ProductsInCart[j].Name = shoppingCartToFind.ProductsInCart[j].Name;

                                shoppingCartToFind.ProductsInCart[j].UnitPrice = result.Price;
                                shoppingCartCollectionToFind.ProductsInCart[j].UnitPrice = shoppingCartToFind.ProductsInCart[j].UnitPrice;

                                QuantityInCart = shoppingCartToFind.ProductsInCart[j].QuantityInCart;
                                shoppingCartCollectionToFind.ProductsInCart[j].QuantityInCart = QuantityInCart;

                                if (result.Quantity >= QuantityInCart)
                                {
                                    NewQuantity = result.Quantity - QuantityInCart;
                                    totalPrice += shoppingCartCollectionToFind.ProductsInCart[j].UnitPrice * QuantityInCart;
                                    result.Quantity = NewQuantity;
                                    await _context.ProductCollection.ReplaceOneAsync(IdFinded, result);
                                }
                            }
                            break;
                        }
                    }
                }
                shoppingCartToFind.PriceTotal = totalPrice;
                shoppingCartCollectionToFind.PriceTotal = shoppingCartToFind.PriceTotal;
                var resultAdd = await _context.ShoppingCartCollection.ReplaceOneAsync(IdCartFinded, shoppingCartCollectionToFind);
                return resultAdd.ModifiedCount == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
