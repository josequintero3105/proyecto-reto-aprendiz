using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Application.Interfaces.Services;
using Application.Common.Utilities;
using Newtonsoft.Json;
using Application.DTOs;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Services;
using FluentValidation.Results;
using Core.Entities.MongoDB;
using Application.Interfaces.Infrastructure.Mongo;

namespace WebApiHttp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        /// <summary>
        /// 
        /// </summary>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] Product body)
        {
            await body.ValidateAndThrowsAsync<Product, ProductValidator>();
            await _productService.CreateProduct(body);
            return Ok(body);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] Product body)
        {
            await body.ValidateAndThrowsAsync<Product, ProductValidator>();
            await _productService.UpdateProduct(body);
            return Ok(body);
        }
    }
}
