using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Services
{
    public interface IShoppingCartService
    {
        /// <summary>
        /// Create shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task CreateShoppingCart(ShoppingCart shoppingCart);
        /// <summary>
        /// Get shopping cart by id
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task<ShoppingCart> GetShoppingCartById(string _id);
        /// <summary>
        /// Get shopping collection from mongo
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Task<bool> GetShoppingCartCollectionMongo(string _id);
        /// <summary>
        /// Add products from shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task AddToShoppingCart(ShoppingCart shoppingCart);
        /// <summary>
        /// Remove products from shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task RemoveFromShoppingCart(ShoppingCart shoppingCart);
    }
}
