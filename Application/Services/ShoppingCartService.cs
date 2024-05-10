using System;
using System.Collections.Generic;
using System.Linq;
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
                shoppingCart.State = true;
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

        /// <summary>
        /// Private method controls the process to get a shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task<bool> ControlGetShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                ShoppingCart shoppingCartFound =  await _shoppingCartRepository.GetShoppingCartAsync(shoppingCart);
                return shoppingCartFound != null;
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
        
        public async Task<bool> GetShoppingCart(ShoppingCart shoppingCart)
        {
            return await ControlGetShoppingCart(shoppingCart);
        }

        /// <summary>
        /// Control to add a product into the shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task ControlAddToShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                shoppingCart.CreatedAt = DateTime.Now;
                shoppingCart.State = true;
                await shoppingCart.ValidateAndThrowsAsync<ShoppingCart, ShoppingCartValidator>();
                var update = await _shoppingCartRepository.AddToShoppingCartAsync(shoppingCart);
                if (update == false)
                {
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
                }
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

        public async Task AddToShoppingCart(ShoppingCart shoppingCart)
        {
            await ControlAddToShoppingCart(shoppingCart);
        }

        /// <summary>
        /// Control to delete a product from the cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <exception cref="BusinessException"></exception>
        private async Task ControlRemoveFromShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                shoppingCart.CreatedAt = DateTime.Now;
                shoppingCart.State = true;
                await shoppingCart.ValidateAndThrowsAsync<ShoppingCart, ShoppingCartValidator>();
                var remove = await _shoppingCartRepository.RemoveFromShoppingCartAsync(shoppingCart);
                if (remove == false)
                {
                    throw new BusinessException(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid),
                    nameof(GateWayBusinessException.ShoppingCartIdIsNotValid));
                }
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

        public async Task RemoveFromShoppingCart(ShoppingCart shoppingCart)
        {
            await ControlRemoveFromShoppingCart(shoppingCart);
        }
    }
}
