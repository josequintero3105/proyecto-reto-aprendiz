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
        /// Private method controls the process of create a shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        private async Task ControlCreateShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                await shoppingCart.ValidateAndThrowsAsync<ShoppingCart, ShoppingCartValidator>();
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
    }
}
