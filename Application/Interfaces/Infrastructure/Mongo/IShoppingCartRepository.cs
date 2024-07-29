using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;
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
        Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCart);
        /// <summary>
        /// Get shopping cart by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ShoppingCart> GetShoppingCartAsync(string _id);
        /// <summary>
        /// Method post for reset
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        ShoppingCartCollection GetShoppingCartForReset(string _id);
        /// <summary>
        /// Update content from shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        Task<bool> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
        /// <summary>
        /// Update price total from shopping cart
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <returns></returns>
        Task<bool> UpdatePriceTotalFromShoppingCart(ShoppingCartCollection shoppingCartCollection);
        /// <summary>
        /// Get shopping cart returns whole the document
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        ShoppingCartCollection GetShoppingCart(ShoppingCart shoppingCart);
        /// <summary>
        /// List specific products
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        Task<List<ProductCollection>> ListSpecificProducts(List<string> productIds);
        /// <summary>
        /// Update quantity from products in stock
        /// </summary>
        /// <param name="productsToAdd"></param>
        /// <returns></returns>
        Task<bool> UpdateQuantityForProduct(ProductCollection productsToAdd);
        /// <summary>
        /// Update Quantities from products
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <returns></returns>
        Task UpdateQuantitiesForProducts(List<WriteModel<ProductCollection>> listModelProducts);
        /// <summary>
        /// Filter products to to bulk update
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <param name="products"></param>
        void FilterToGetProduct(List<WriteModel<ProductCollection>> listModelProducts, ProductCollection products);
        /// <summary>
        /// Remove products from shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveProductFromCartAsync(ShoppingCartCollection shoppingCart, string id);
        /// <summary>
        /// Add products in row into products array from shopping cart
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <param name="productInCart"></param>
        /// <returns></returns>
        Task AddAnotherProductInCartAsync(ShoppingCartCollection shoppingCartCollection, ProductInCart productInCart);
        /// <summary>
        /// Update the quantity in specific object into the products array
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <param name="productInCart"></param>
        /// <returns></returns>
        Task AddMoreCountOfCurrentProduct(ShoppingCartCollection shoppingCartCollection, ProductInCart productInCart);
        /// <summary>
        /// Create shopping cart returns whole the document created
        /// </summary>
        /// <param name="shoppingCartToCreate"></param>
        /// <returns></returns>
        Task<ShoppingCartCollection> CreateAsync(ShoppingCart shoppingCartToCreate);
        /// <summary>
        /// List All Shopping Carts
        /// </summary>
        /// <returns></returns>
        Task<List<ShoppingCartCollection>> ListAllCarts();
    }
}
