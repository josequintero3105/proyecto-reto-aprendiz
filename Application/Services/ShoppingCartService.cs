using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Core.Entities.MongoDB;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using Newtonsoft.Json;

namespace Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ILogger<ShoppingCartService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCartRepository"></param>
        /// <param name="logger"></param>
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ILogger<ShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _logger = logger;
        }

        /// <summary>
        /// Private method controls the process to create a shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task ControlCreateShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                shoppingCart.CreatedAt = DateTime.Now;
                shoppingCart.Active = true;
                await _shoppingCartRepository.CreateShoppingCartAsync(shoppingCart);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating shoppingCart: {shoppingCart}"
                    , bex.Code, bex.Message, shoppingCart);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating shoppingCart: {shoppingCart} ", ex.Message, shoppingCart);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }

        public async Task CreateShoppingCart(ShoppingCart shoppingCart)
        {
            await ControlCreateShoppingCart(shoppingCart);
        }

        private async Task<List<ProductCollection>> ListProductCollections(ShoppingCart shoppingCart)
        {
            List<string> productIds = shoppingCart.ProductsInCart.Select(p => p._id.ToString()).ToList();
            var specificProducts = await _shoppingCartRepository.ListSpecificProducts(productIds);
            return specificProducts;
        }

        private async Task ProcessForEachProduct(ShoppingCart shoppingCart)
        {
            var resultCart = _shoppingCartRepository.GetShoppingCart(shoppingCart);
            var specificProducts = await ListProductCollections(shoppingCart);
            var listModelProducts = new List<WriteModel<ProductCollection>>();
            foreach (var products in specificProducts)
            {
                resultCart.PriceTotal = CalculateAndAssign(shoppingCart, products, resultCart.PriceTotal);
                _shoppingCartRepository.FilterToGetProduct(listModelProducts, products);   
            }
            await _shoppingCartRepository.UpdateQuantitiesForProducts(listModelProducts);
            shoppingCart.PriceTotal = resultCart.PriceTotal;
            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);
        }

        private double CalculateAndAssign(ShoppingCart shoppingCart, ProductCollection products, double PriceTotal)
        {
            var productToBill = shoppingCart.ProductsInCart.First(s => s._id == products._id);
            products.Quantity -= productToBill.QuantityInCart;
            productToBill.Name = products.Name;
            productToBill.UnitPrice = products.Price;
            PriceTotal += productToBill.QuantityInCart * products.Price;
            return PriceTotal;
        }

        private async Task GetAtLeastOneProduct(ShoppingCart shoppingCart)
        {
            var specificProducts = await ListProductCollections(shoppingCart);
            if (specificProducts.Count > 0)
                await ProcessForEachProduct(shoppingCart);
            else
                throw new BusinessException(nameof(GateWayBusinessException.ProductIdIsNotValid),
                    nameof(GateWayBusinessException.ProductIdIsNotValid));
        }

        public async Task AddToShoppingCart(ShoppingCart shoppingCart)
        {
            shoppingCart.CreatedAt = DateTime.Now;
            await shoppingCart.ValidateAndThrowsAsync<ShoppingCart, ShoppingCartValidator>();
            var result = _shoppingCartRepository.GetShoppingCart(shoppingCart);
            if (result != null)
            {
                await GetAtLeastOneProduct(shoppingCart);
            }
            else
            {
                throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
            }
        }

        /// <summary>
        /// Control to delete a product from the cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <exception cref="BusinessException"></exception>
        private async Task LogicRemoveFromShoppingCart(ShoppingCart shoppingCart)
        {
            shoppingCart.CreatedAt = DateTime.Now;
            var resultCart = _shoppingCartRepository.GetShoppingCart(shoppingCart);
            List<string> productIds = shoppingCart.ProductsInCart.Select(p => p._id.ToString()).ToList();
            var specificProducts = await _shoppingCartRepository.ListSpecificProducts(productIds);
            foreach (var products in specificProducts)
            {
                await _shoppingCartRepository.RemoveProductFromCartAsync(resultCart, products._id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task RemoveFromShoppingCart(ShoppingCart shoppingCart)
        {
            shoppingCart.CreatedAt = DateTime.Now;
            var result = _shoppingCartRepository.GetShoppingCart(shoppingCart);
            if (result != null)
            {
                await LogicRemoveFromShoppingCart(shoppingCart);
            }
            else
            {
                throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
            }
        }
    }
}
