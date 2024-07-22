using System.Net;
using System.Text;
using Application.DTOs;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Application.Interfaces.Common;
using Application.Interfaces.Services;
using Core.Entities.MongoDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IHandle _handle;
        private readonly IShoppingCartService _shoppingCartService;
        

        public TransactionController(ITransactionService transactionService, IHandle handle, IShoppingCartService shoppingCartService)
        {
            _transactionService = transactionService;
            _handle = handle;
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionInput transactionInput)
        {
            //TransactionOutput transactionOutput = await _transactionService.ProcessTransaction(transactionInput);
            //return CreatedAtAction(nameof(Create), new { transactionOutput._id}, transactionOutput);
            await _shoppingCartService.ProcessTransaction(transactionInput);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? _id = null)
        {
            var result = await _transactionService.GetTransactionById(null!, _id!);
            return Ok(result);
        }
    }
}
