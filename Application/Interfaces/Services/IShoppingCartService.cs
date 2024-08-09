using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Application.DTOs.ApiEntities.Input;
using Application.DTOs.ApiEntities.Output;
using Core.Entities.MongoDB;
using Application.DTOs.ApiEntities.Response;

namespace Application.Interfaces.Services
{
    public interface IShoppingCartService
    {
        /// <summary>
        /// Create shopping cart
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <returns></returns>
        Task<ShoppingCartCollection> CreateShoppingCart(ShoppingCartInput shoppingCartInput);
        /// <summary>
        /// Get shopping cart by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ShoppingCart> GetShoppingCartById(string _id);
        /// <summary>
        /// Reset Shopping Cart
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ShoppingCart> ResetShoppingCart(string _id);
        /// <summary>
        /// Add products from shopping cart
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ShoppingCart> AddToShoppingCart(ShoppingCartInput shoppingCartInput, string _id);
        /// <summary>
        /// Remove products from shopping cart
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        Task<ShoppingCart> RemoveFromShoppingCart(ShoppingCartInput shoppingCartInput, string _id);
        /// <summary>
        /// Process Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        Task<TransactionResponse> ProcessCartForTransaction(TransactionInput transactionInput);
        /// <summary>
        /// Define Final Status
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="transactionOutput"></param>
        /// <returns></returns>
        Task<string> DefineFinalStatus(ShoppingCart shoppingCart, TransactionResponse transactionResponse);
    }
}
