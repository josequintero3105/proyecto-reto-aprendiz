using Application.DTOs;
using Application.DTOs.Entries;
using Application.Interfaces.Common;
using Application.Interfaces.Services;
using Core.Entities.MongoDB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IHandle _handle;

        public TransactionController(ITransactionService transactionService, IHandle handle)
        {
            _transactionService = transactionService;
            _handle = handle;
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] TransactionInput transactionInput)
        {
            TransactionCollection transactionCollection = await _transactionService.CreateTransaction(transactionInput);
            return CreatedAtAction(nameof(Create), new {transactionCollection._id}, transactionCollection);
        }

        /// <summary>
        /// GetTransactionById
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTransactionById([FromQuery] string? _id = null)
        {
            var result = await _transactionService.GetTransactionById(_id);
            return Ok(result);
        }
    }
}
