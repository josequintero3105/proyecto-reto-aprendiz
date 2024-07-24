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
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IHandle _handle;

        public TransactionController(IShoppingCartService shoppingCartService, IHandle handle, ITransactionService transactionService)
        {
            _shoppingCartService = shoppingCartService;
            _handle = handle;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Process([FromBody] TransactionInput transactionInput)
        {
            var result = await _shoppingCartService.GetCartForTransaction(transactionInput);
            return Ok(result._id);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? _id = null)
        {
            var result = await _shoppingCartService.ResetShoppingCart(_id!);
            return Ok(result);
        }
    }
}
