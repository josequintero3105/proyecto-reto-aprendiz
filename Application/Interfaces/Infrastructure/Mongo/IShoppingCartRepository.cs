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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task<ShoppingCart> GetShoppingCartObject(ShoppingCart shoppingCart);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public Task<bool> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public ShoppingCartCollection GetShoppingCart(ShoppingCart shoppingCart);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public Task<List<ProductCollection>> ListSpecificProducts(List<string> productIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productsToAdd"></param>
        /// <returns></returns>
        public Task<bool> UpdateQuantityForProduct(ProductCollection productsToAdd);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <returns></returns>
        public Task UpdateQuantitiesForProducts(List<WriteModel<ProductCollection>> listModelProducts);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <param name="products"></param>
        public void FilterToGetProduct(List<WriteModel<ProductCollection>> listModelProducts, ProductCollection products);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveProductFromCartAsync(ShoppingCartCollection shoppingCart, string id);
    }
}
