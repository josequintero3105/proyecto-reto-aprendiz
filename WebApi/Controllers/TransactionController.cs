using Application.DTOs;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient = new HttpClient();

        public TransactionController(ITransactionService transactionService, IHandle handle, IHttpClientFactory httpClientFactory, HttpClient httpClient)
        {
            _transactionService = transactionService;
            _handle = handle;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Method Test
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> Test()
        //{
        //    var client = _httpClientFactory.CreateClient("pasarela");
        //    var result = await client.GetAsync("https://devapi.credinet.co/pay/");
        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] TransactionInput transactionInput)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("https://devapi.credinet.co/pay/create", transactionInput);
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    return Created("~/api/Transaction/", responseString);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] string? _id = null)
        //{
        //    var response = await _httpClient.GetAsync("https://devapi.credinet.co/pay/GetTransactionResponse?transactionId=" + _id + "");
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    return Ok(responseString);
        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? _id = null)
        {
            TransactionResponse transactionResponse = await _transactionService.GetTransactionById(_id!);
            return Ok(transactionResponse);
        }
    }
}
