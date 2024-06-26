using Application.DTOs;
using Application.DTOs.Entries;
using Application.Interfaces.Common;
using Application.Interfaces.Services;
using Application.Services;
using Azure.Core;
using Core.Entities.MongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IHandle _handle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCartService"></param>
        /// <param name="handle"></param>
        public ShoppingCartController(IShoppingCartService shoppingCartService, IHandle handle)
        {
            _shoppingCartService = shoppingCartService;
            _handle = handle;   
        }

        /// <summary>
        /// Method Post Create a shoppingcart
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] ShoppingCartInput body)
        {
            ShoppingCartCollection shoppingCart = await _handle.HandleRequestContextException(_shoppingCartService.CreateShoppingCart, body);
            return CreatedAtAction(nameof(Create), new { shoppingCart._id }, shoppingCart);
        }

        /// <summary>
        /// Method Put Add one or more products to shoppingcart
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProductToCart([FromBody] ShoppingCartInput body, [FromQuery] string? _id = null)
        {
            ShoppingCart shoppingCart = await _shoppingCartService.AddToShoppingCart(body, _id);
            return Ok(shoppingCart);
        }

        /// <summary>
        /// Method get shoppingcart by id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetShoppingCartById([FromQuery] string? _id = null)
        {
            var result = await _shoppingCartService.GetShoppingCartById(_id);
            return Ok(result);
        }

        /// <summary>
        /// Remove a product from the shopping cart
        /// </summary>
        /// <param name="body"></param>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveProductFromCart([FromBody] ShoppingCartInput body, [FromQuery] string? _id = null)
        {
            await _handle.HandleRequestContextCatchException(_shoppingCartService.RemoveFromShoppingCart(body, _id));
            return Ok("Products removed successfully");
        }
    }
}
