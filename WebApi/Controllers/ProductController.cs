using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Application.Interfaces.Services;
using Application.Common.Utilities;
using Newtonsoft.Json;
using Application.DTOs;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Core.Entities.MongoDB;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Common;

namespace WebApiHttp.Controllers
{
    /// <summary>
    /// General route of endpoints
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IProductService _productService;
        private readonly IHandle _handle;
        /// <summary>
        /// Constructor
        /// <paramref name="productService"/>
        /// <paramref name="handle"/>
        /// </summary>
        public ProductController(IProductService productService, IHandle handle)
        {
            _productService = productService;
            _handle = handle;
        }
        /// <summary>
        /// Method Post Create Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] Product body)
        {
            await _handle.HandleRequestContextCatchException(_productService.CreateProduct(body));
            return Created("~/api/Product/", body);
        }

        /// <summary>
        /// Method Get Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById([FromQuery] string _id)
        {
            var result = await _productService.GetProductById(_id);
            return Ok(result);
        }

        /// <summary>
        /// Method Get Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }

        /// <summary>
        /// Method Pagination requires page number and products count
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPage([FromQuery] int page, [FromQuery] int size)
        {
            var result = await _productService.GetProductsPagination(page, size);
            return Ok(result);
        }

        /// <summary>
        /// Method Put Update Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] ProductToGet body)
        {
            await _handle.HandleRequestContextCatchException(_productService.UpdateProduct(body));
            return Ok(body);
        }
    }
}
