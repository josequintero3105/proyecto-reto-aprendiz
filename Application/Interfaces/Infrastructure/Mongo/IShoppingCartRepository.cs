using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;

namespace Application.Interfaces.Infrastructure.Mongo
{
    public interface IShoppingCartRepository
    {
        /// <summary>
        /// Defining contract for create product in the database
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCart);
        public Task<bool> AddToShoppingCartAsync(ShoppingCart shoppingCart);
        public Task<bool> RemoveFromShoppingCartAsync(ShoppingCart shoppingCartToFind);
    }
}
