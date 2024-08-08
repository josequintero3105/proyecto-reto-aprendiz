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
using Core.Enumerations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public TransactionController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        /// <summary>
        /// Process Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Process([FromBody] TransactionInput transactionInput)
        {
            var result = await _shoppingCartService.ProcessCartForTransaction(transactionInput);
            return Ok($"The Transaction {result._id} was created");
        }
    }
}
