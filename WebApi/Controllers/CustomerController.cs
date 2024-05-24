using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Interfaces.Common;

namespace WebApi.Controllers
{
    /// <summary>
    /// General route of endpoints
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly ICustomerService _customerService;
        private readonly IHandle _handle;
        /// <summary>
        /// Constructor
        /// <paramref name="customerService"/>
        /// <paramref name="handle"/>
        /// </summary>
        public CustomerController(ICustomerService customerService, IHandle handle)
        {
            _customerService = customerService;
            _handle = handle;
        }
        /// <summary>
        /// Method Post Create Customer
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] Customer body)
        {
            await _handle.HandleRequestContextCatchException(_customerService.CreateCustomer(body));
            return Ok(body);
        }
        /// <summary>
        /// Method Post Update Customer
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] Customer body)
        {
            await _handle.HandleRequestContextCatchException(_customerService.UpdateCustomer(body));
            return Ok(body);
        }
        /// <summary>
        /// Method Get Get Customer
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromBody] Customer body)
        {
            var result = await _customerService.GetCustomerById(body);
            return Ok(result);
        }
    }
}
