using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Handle;
using Microsoft.Extensions.Options;
using Application.Interfaces.Services;
using Application.Interfaces.Common;
using Application.Common.Utilities;
using Newtonsoft.Json;
using Application.DTOs;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.ProductValidators;
using Application.Services;

namespace WebApiHttp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IHandle _handle;

        public ProductController(IHandle handle)
        {
            _productService = new ProductService();
            _handle = handle;
            
        }

        [HttpPost()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] Product body)
        {
            await body.ValidateAndThrowsAsync<Product, ProductValidator>();
            var entity = await _handle.HandleRequestContextCatchException(_productService.InsertProduct, body);
            return Ok(entity);
        }

        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] Product body)
        {
            await body.ValidateAndThrowsAsync<Product, ProductValidator>();
            var entity = await _handle.HandleRequestContextCatchException(_productService.InsertProduct, body);
            return Ok(entity);
        }
    }
}
