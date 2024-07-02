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
using Application.Services;
using Application.DTOs.Entries;

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
        public async Task<IActionResult> Create([FromBody] ProductInput body)
        {
            ProductCollection product = await _handle.HandleRequestContextException(_productService.CreateProduct, body);
            return CreatedAtAction(nameof(Create), new { product._id}, product);
        }

        /// <summary>
        /// Method Get Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProductById([FromQuery] string? _id = null)
        {
            var result = await _productService.GetProductById(_id);
            return Ok(result);
        }

        /// <summary>
        /// Method List Products
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ListProducts()
        {
            var result = await _productService.ListProducts();
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
        public async Task<IActionResult> ListAnyProducts([FromQuery] string? page = null, [FromQuery] string? size = null)
        {
            var result = await _productService.ListProductsPerPage(page, size);
            return Ok(result);
        }

        /// <summary>
        /// Method Put Update Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] ProductInput body, [FromQuery] string? _id = null)
        {
            ProductOutput product = await _productService.UpdateProduct(body, _id);
            return Created("~/api/Product/", product);
        }
    }
}
