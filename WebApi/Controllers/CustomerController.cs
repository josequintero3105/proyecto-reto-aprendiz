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
        /// Method Post Create Product
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
    }
}
