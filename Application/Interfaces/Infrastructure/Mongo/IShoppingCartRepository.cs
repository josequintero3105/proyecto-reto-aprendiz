using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Entities.MongoDB;
using MongoDB.Driver;

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
        public Task<ShoppingCart> GetShoppingCartAlter(ShoppingCart shoppingCart);
        public Task<bool> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
        public ShoppingCartCollection GetShoppingCart(ShoppingCart shoppingCart);
        public Task<List<ProductCollection>> ListSpecificProducts(List<string> productIds);
        public Task<bool> UpdateQuantityForProduct(ProductCollection productsToAdd);
        public Task UpdateQuantitiesForProducts(List<WriteModel<ProductCollection>> BulkWriteQuantities);
        public void FilterToGetProduct(List<WriteModel<ProductCollection>> BulkWriteQuantities, ProductCollection products);
        public Task RemoveProductFromCartAsync(ShoppingCartCollection shoppingCart, string id);
    }
}
