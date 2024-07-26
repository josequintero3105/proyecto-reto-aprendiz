using System.Net;
using System.Text;
using Application.DTOs;
using Application.DTOs.Responses;
using Application.DTOs.ApiEntities.Input;
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
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IHandle _handle;

        public TransactionController(IShoppingCartService shoppingCartService, IHandle handle)
        {
            _shoppingCartService = shoppingCartService;
            _handle = handle;
        }
        /// <summary>
        /// Process Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Process([FromBody] TransactionInput transactionInput)
        {
            var result = await _shoppingCartService.GetCartForTransaction(transactionInput);
            return Ok($"To see the transaction details, search it by id: {result._id}");
        }
    }
}
