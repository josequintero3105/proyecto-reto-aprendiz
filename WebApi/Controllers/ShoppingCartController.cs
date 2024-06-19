using Application.DTOs;
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
        /// Method Post Create Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] ShoppingCart body)
        {
            await _handle.HandleRequestContextCatchException(_shoppingCartService.CreateShoppingCart(body));
            return Ok(body);
        }

        /// <summary>
        /// Method Post Create Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Add([FromBody] ShoppingCart body)
        {
            await _handle.HandleRequestContextCatchException(_shoppingCartService.AddToShoppingCart(body));
            return Ok(body);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromQuery] string _id)
        {
            var result = await _shoppingCartService.GetShoppingCartById(_id);
            return Ok(result);
        }

        /// <summary>
        /// Remove a product
        /// </summary>
        /// <param name="body"></param>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Remove([FromBody] ShoppingCart body)
        {
            await _handle.HandleRequestContextCatchException(_shoppingCartService.RemoveFromShoppingCart(body));
            return Ok("Products removed successfully");
        }
    }
}
