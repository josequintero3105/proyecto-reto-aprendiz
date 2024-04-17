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
        /// <summary>
        /// Constructor
        /// <paramref name="productService"/>
        /// </summary>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Method Post Create Product
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] Product body)
        {
            await _productService.CreateProduct(body);
            return Ok(true);
        }

        /// <summary>
        /// Method Put Update Product
        /// </summary>
        /// <param name="body"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] Product body, string _id)
        {
            body._id = new MongoDB.Bson.ObjectId(_id).ToString();
            await _productService.UpdateProduct(body);
            return Ok(true);
        }
    }
}
