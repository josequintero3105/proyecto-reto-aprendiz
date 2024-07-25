using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Interfaces.Common;
using Core.Entities.MongoDB;
using System.Net;
using Application.DTOs.Entries;
using Application.DTOs.Responses;

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
        public async Task<IActionResult> Create([FromBody] CustomerInput body)
        {
            CustomerCollection customer = await _handle.HandleRequestContextException(_customerService.CreateCustomer, body);
            return CreatedAtAction(nameof(Create), new { customer._id }, customer);
        }
        /// <summary>
        /// Method Put Update Customer
        /// </summary>
        /// <param name="body"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] CustomerInput body, [FromQuery] string? _id = null)
        {
            CustomerOutput customer = await _customerService.UpdateCustomerData(body, _id);
            return Created("~/api/Customer/", customer);
        }
        /// <summary>
        /// Method Get Get Customer
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCustomerById([FromQuery] string? _id = null)
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
        public async Task<IActionResult> Delete([FromQuery] string? _id = null)
        {
            await _handle.HandleRequestContextCatchException(_customerService.DeleteCustomer(_id));
            return Ok("Customer deleted successfully");
        }
    }
}
