using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.DTOs.Entries;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Core.Entities.MongoDB;
using Core.Enumerations;
using Microsoft.Extensions.Logging;
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
        private readonly ITransactionService _transactionService;
        private readonly ILogger<ShoppingCartService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCartRepository"></param>
        /// <param name="logger"></param>
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ILogger<ShoppingCartService> logger, ITransactionService transactionService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _logger = logger;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Private method controls the process to create a shopping cart
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ShoppingCartCollection> CreateShoppingCart(ShoppingCartInput shoppingCartInput)
        {
            try
            {
                ShoppingCart shoppingCart = new()
                {
                    ProductsInCart = shoppingCartInput.ProductsInCart,
                    Status = "Pending"
                };
                await GetAtLeastOneProduct(shoppingCart);
                return await _shoppingCartRepository.CreateAsync(shoppingCart);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                    throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlledException),
                    nameof(GateWayBusinessException.NotControlledException));
            }
        }

        /// <summary>
        /// Get the products
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ShoppingCart> GetShoppingCartById(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    var result = await _shoppingCartRepository.GetShoppingCartAsync(_id);
                    if (result != null)
                        return result;
                    else
                        throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdNotFound),
                        nameof(GateWayBusinessException.ShoppingCartIdNotFound));
                }   
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull),
                    nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} getting shoppingCart"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} getting shoppingCart ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
            }
        }

        /// <summary>
        /// Lis only the specific products of the json format
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        private async Task<List<ProductCollection>> ListProductCollections(ShoppingCart shoppingCart)
        {
            try
            {
                List<string> productIds = shoppingCart.ProductsInCart.Select(p => p._id.ToString()).ToList();
                var specificProducts = await _shoppingCartRepository.ListSpecificProducts(productIds);
                return specificProducts;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                    throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ProductIdCannotBeNull),
                    nameof(GateWayBusinessException.ProductIdCannotBeNull));
            }
        }

        /// <summary>
        /// Logic For Each Product
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        private async Task LogicForEachProduct(ShoppingCart shoppingCart)
        {
            var resultCart = DefineShoppingCart(shoppingCart);
            ProductInCart productInCart;
            var specificProducts = await ListProductCollections(shoppingCart);
            var listModelProducts = new List<WriteModel<ProductCollection>>();
            foreach (var products in specificProducts)
            {
                if (!GetCorrectQuantity(shoppingCart, products))
                    continue;
                resultCart.PriceTotal = CalculateTotal(shoppingCart, products, resultCart.PriceTotal);
                productInCart = GetObjectFromArray(shoppingCart, products);
                _shoppingCartRepository.FilterToGetProduct(listModelProducts, products);
                await SearchIfExistsAProductInCart(resultCart, shoppingCart, productInCart, products);
                await ConfirmChanges(listModelProducts, shoppingCart, resultCart);
            }
            shoppingCart.ProductsInCart.RemoveAll(x => x.Name == null);
        }
        /// <summary>
        /// Search if exists a product in cart
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <param name="shoppingCart"></param>
        /// <param name="productInCart"></param>
        /// <param name="productCollection"></param>
        /// <returns></returns>
        private async Task SearchIfExistsAProductInCart(ShoppingCartCollection shoppingCartCollection, ShoppingCart shoppingCart, 
            ProductInCart productInCart, ProductCollection productCollection)
        {
            if (shoppingCart._id != null)
            {
                if (!shoppingCartCollection.ProductsInCart.Any(p => p._id == productInCart._id))
                    await _shoppingCartRepository.AddAnotherProductInCartAsync(shoppingCartCollection, productInCart);
                else
                {
                    productInCart.QuantityInCart = GetNewCountForCurrentProduct(shoppingCart, shoppingCartCollection, productCollection);
                    await _shoppingCartRepository.AddMoreCountOfCurrentProduct(shoppingCartCollection, productInCart);
                }
            }
            else
                await _shoppingCartRepository.AddAnotherProductInCartAsync(shoppingCartCollection, productInCart);
        }
        /// <summary>
        /// Get New Count For Current Product
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="shoppingCartCollection"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        private int GetNewCountForCurrentProduct(ShoppingCart shoppingCart, ShoppingCartCollection shoppingCartCollection, ProductCollection products)
        {
            var productToAdd = shoppingCart.ProductsInCart.First(s => s._id == products._id);
            var productObjectToFind = shoppingCartCollection.ProductsInCart.First(s => s._id == products._id); 
            products.Quantity -= (int)productToAdd.QuantityInCart;
            productToAdd.QuantityInCart += productObjectToFind.QuantityInCart;
            return (int)productToAdd.QuantityInCart;
        }

        /// <summary>
        /// Confirm changes for total price and quantities
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <param name="shoppingCart"></param>
        /// <param name="resultCart"></param>
        /// <returns></returns>
        private async Task ConfirmChanges(List<WriteModel<ProductCollection>> listModelProducts, ShoppingCart shoppingCart, ShoppingCartCollection resultCart)
        {
            await _shoppingCartRepository.UpdateQuantitiesForProducts(listModelProducts);
            if (shoppingCart._id != null)
            {
                var resultShopping = _shoppingCartRepository.GetShoppingCart(shoppingCart);
                resultShopping.PriceTotal = resultCart.PriceTotal;
                await _shoppingCartRepository.UpdatePriceTotalFromShoppingCart(resultShopping);
            }
            shoppingCart.PriceTotal = resultCart.PriceTotal;
        }
        
        /// <summary>
        /// Define if shopping cart is new or not
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        private ShoppingCartCollection DefineShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart._id != null)
                return _shoppingCartRepository.GetShoppingCart(shoppingCart);
            else
                return new ShoppingCartCollection();
        }

        /// <summary>
        /// Get Object Of Product From Array
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private ProductInCart GetObjectFromArray(ShoppingCart shoppingCart, ProductCollection products)
        {
            try
            {
                var productToAdd = shoppingCart.ProductsInCart.First(s => s._id == products._id);
                productToAdd.Name = products.Name;
                productToAdd.UnitPrice = products.Price;
                return productToAdd;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ProductIdCannotBeNull),
                    nameof(GateWayBusinessException.ProductIdCannotBeNull));
            }
        }
        
        /// <summary>
        /// Calculate Total For Pay
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="products"></param>
        /// <param name="PriceTotal"></param>
        /// <returns></returns>
        private double CalculateTotal(ShoppingCart shoppingCart, ProductCollection products, double PriceTotal)
        {
            try
            {
                var productToAdd = shoppingCart.ProductsInCart.First(s => s._id == products._id);
                products.Quantity -= (int)productToAdd.QuantityInCart;
                PriceTotal += productToAdd.QuantityInCart * products.Price;
                return PriceTotal;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ProductCountNotValid),
                    nameof(GateWayBusinessException.ProductCountNotValid));
            }
        }
        /// <summary>
        /// Get correct quantity
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        private static bool GetCorrectQuantity(ShoppingCart shoppingCart, ProductCollection products)
        {
            var productToAdd = shoppingCart.ProductsInCart.First(s => s._id == products._id);
            if (products.Quantity >= productToAdd.QuantityInCart && productToAdd.QuantityInCart > 0 && productToAdd.QuantityInCart < Int32.MaxValue)
                return true;
            else
                return false;
        }

        /// <summary>
        /// method garantizes there not cero products into the array
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task GetAtLeastOneProduct(ShoppingCart shoppingCart)
        {
            var specificProducts = await ListProductCollections(shoppingCart);
            if (specificProducts.Count > 0)
                await LogicForEachProduct(shoppingCart);
            else
                throw new BusinessException(nameof(GateWayBusinessException.ProductListCannotBeNull),
                    nameof(GateWayBusinessException.ProductListCannotBeNull));
        }

        /// <summary>
        /// Add product to shopping cart
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ShoppingCart> AddToShoppingCart(ShoppingCartInput shoppingCartInput, string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    ShoppingCart shoppingCart = new()
                    {
                        _id = _id,
                        ProductsInCart = shoppingCartInput.ProductsInCart
                    };
                    await shoppingCart.ValidateAndThrowsAsync<ShoppingCart, ShoppingCartValidator>();
                    var result = _shoppingCartRepository.GetShoppingCart(shoppingCart);
                    if (result != null)
                    {
                        await GetAtLeastOneProduct(shoppingCart);
                        return await _shoppingCartRepository.GetShoppingCartAsync(_id);
                    }
                    else
                        throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                            nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull),
                            nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull));

            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                    throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
            }
        }

        /// <summary>
        /// logic for remove a product from shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <exception cref="BusinessException"></exception>
        private async Task LogicRemoveFromShoppingCart(ShoppingCart shoppingCart)
        {
            var resultCart = _shoppingCartRepository.GetShoppingCart(shoppingCart);
            var specificProducts = await ListProductCollections(shoppingCart);
            var listModelProducts = new List<WriteModel<ProductCollection>>();
            foreach (var products in specificProducts)
            {
                if (!resultCart.ProductsInCart.Any(p => p._id == products._id))
                    continue;
                resultCart.PriceTotal = DiscountTotalPrice(resultCart, products, resultCart.PriceTotal);
                _shoppingCartRepository.FilterToGetProduct(listModelProducts, products);
                await _shoppingCartRepository.RemoveProductFromCartAsync(resultCart, products._id);
                await ConfirmChanges(listModelProducts, shoppingCart, resultCart);
            }
        }
        /// <summary>
        /// Discount total price for pay
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="products"></param>
        /// <param name="PriceTotal"></param>
        /// <returns></returns>
        private double DiscountTotalPrice(ShoppingCartCollection shoppingCart, ProductCollection products, double PriceTotal)
        {
            try
            {
                var productToRemove = shoppingCart.ProductsInCart.First(s => s._id == products._id);
                products.Quantity += (int)productToRemove.QuantityInCart;
                PriceTotal -= productToRemove.QuantityInCart * products.Price;
                return PriceTotal;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}",
                    bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} ", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ProductNotExistsInTheCart),
                    nameof(GateWayBusinessException.ProductNotExistsInTheCart));
            }
        }
        /// <summary>
        /// Logic for transaction rejected
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        private async Task LogicForTransactionRejected(string _id)
        {
            var resultCart = _shoppingCartRepository.GetShoppingCartForReset(_id);
            var specificProducts = await ListProductsInCart(resultCart);
            var listModelProducts = new List<WriteModel<ProductCollection>>();
            foreach (var products in specificProducts)
            {                
                resultCart.PriceTotal = DiscountTotalPrice(resultCart, products, resultCart.PriceTotal);
                _shoppingCartRepository.FilterToGetProduct(listModelProducts, products);
                await _shoppingCartRepository.RemoveProductFromCartAsync(resultCart, products._id!);
                await ConfirmRestore(listModelProducts);
            }
        }

        /// <summary>
        /// Confirme Restore Stock
        /// </summary>
        /// <param name="listModelProducts"></param>
        /// <param name="resultCartCollection"></param>
        /// <returns></returns>
        private async Task ConfirmRestore(List<WriteModel<ProductCollection>> listModelProducts)
        {
            await _shoppingCartRepository.UpdateQuantitiesForProducts(listModelProducts);
        }

        /// <summary>
        /// Lis only the specific products of the json format
        /// </summary>
        /// <param name="shoppingCartCollection"></param>
        /// <returns></returns>
        private async Task<List<ProductCollection>> ListProductsInCart(ShoppingCartCollection shoppingCartCollection)
        {
            try
            {
                List<string> productIds = shoppingCartCollection.ProductsInCart!.Select(p => p._id!.ToString()).ToList();
                var specificProducts = await _shoppingCartRepository.ListSpecificProducts(productIds);
                return specificProducts;
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ProductIdCannotBeNull),
                    nameof(GateWayBusinessException.ProductIdCannotBeNull));
            }
        }

        /// <summary>
        /// Remove a product from shopping cart
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ShoppingCart> ResetShoppingCart(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    var resultCartCollection = _shoppingCartRepository.GetShoppingCartForReset(_id);
                    if (resultCartCollection != null && resultCartCollection.ProductsInCart!.Count > 0)
                    {
                        await LogicForTransactionRejected(_id);
                        return await _shoppingCartRepository.GetShoppingCartAsync(_id);
                    }
                    else
                        throw new BusinessException(nameof(GateWayBusinessException.NotProductsInCart),
                            nameof(GateWayBusinessException.NotProductsInCart));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull),
                        nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
            }
        }

        /// <summary>
        /// Remove a product from shopping cart
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<ShoppingCart> RemoveFromShoppingCart(ShoppingCartInput shoppingCartInput, string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    ShoppingCart shoppingCart = new()
                    {
                        _id = _id,
                        ProductsInCart = shoppingCartInput.ProductsInCart
                    };
                    await shoppingCart.ValidateAndThrowsAsync<ShoppingCart, ShoppingCartValidator>();
                    var result = _shoppingCartRepository.GetShoppingCart(shoppingCart);
                    if (result != null && result.ProductsInCart.Count > 0)
                    {
                        await LogicRemoveFromShoppingCart(shoppingCart);
                        return await _shoppingCartRepository.GetShoppingCartAsync(_id);
                    }
                    else
                        throw new BusinessException(nameof(GateWayBusinessException.NotProductsInCart),
                            nameof(GateWayBusinessException.NotProductsInCart));
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull),
                        nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull));
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                    throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
            }
        }

        public async Task ChangeCartStatus(TransactionInput transactionInput)
        {
            try
            {
                var transactionResponse = await _transactionService.ProcessTransaction(transactionInput);
                var resultCart = await _shoppingCartRepository.GetShoppingCartAsync(transactionInput.Invoice!);
                if (resultCart != null)
                {
                    switch (transactionResponse.TransactionStatus)
                    {
                        case "Approved":
                            resultCart.Status = TransactionCoreStatus.Approved.ToString();
                            await _shoppingCartRepository.UpdateShoppingCartAsync(resultCart);
                            break;

                        case "Pending":
                            resultCart.Status = TransactionCoreStatus.Pending.ToString();
                            await RestoreProductStock(resultCart, transactionInput.Invoice!);
                            break;

                        default:
                            resultCart.Status = TransactionCoreStatus.Rejected.ToString();
                            await _shoppingCartRepository.UpdateShoppingCartAsync(resultCart);
                            break;
                    }
                }
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message}"
                    , bex.Code, bex.Message);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message}", ex.Message);
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdNotFound),
                    nameof(GateWayBusinessException.TransactionIdNotFound));
            }
            
        }

        public async Task RestoreProductStock(ShoppingCart shoppingCart, string _id)
        {
            while (shoppingCart.Status == "Pending")
            {
                var transactionStatus = await _transactionService.CheckTransactionStatus(_id);
                if (transactionStatus.transactionStatus == "Approved")
                {
                    shoppingCart.Status = "Aprobado";
                    break;
                }
                else if (transactionStatus.transactionStatus == "Pending")
                {
                    shoppingCart.Status = "Sin_Procesar";
                    await RemoveFromShoppingCart(null!, _id);
                    break;
                }
            }
        }
    }
}
