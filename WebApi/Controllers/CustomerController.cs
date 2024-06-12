using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Interfaces.Common;
using Core.Entities.MongoDB;
using System.Net;

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
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] Customer body)
        {
            await _handle.HandleRequestContextCatchException(_customerService.CreateCustomer(body));
            return Created("~/api/Customer/", body);
        }
        /// <summary>
        /// Method Put Update Customer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] Customer body, [FromQuery] string _id)
        {
            body._id = _id;
            await _handle.HandleRequestContextCatchException(_customerService.UpdateCustomer(body));
            return Created("~/api/Customer/", body);
        }
        /// <summary>
        /// Method Get Get Customer
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromQuery] string _id)
        {
            var result = await _customerService.GetCustomerById(_id);
            return Ok(result);
        }

        /// <summary>
        /// Method Delete a Customer
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpDelete()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] string _id)
        {
            await _handle.HandleRequestContextCatchException(_customerService.DeleteCustomer(_id));
            return Ok("Customer deleted successfully");
        }
    }
}
