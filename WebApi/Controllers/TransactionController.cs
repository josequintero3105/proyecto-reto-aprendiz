using System.Net;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionInput transactionInput)
        {
            var response = await _httpClient.PostAsJsonAsync("https://devapi.credinet.co/pay/create", transactionInput);
            var responseString = await response.Content.ReadAsStringAsync();
            return Created("~/api/Transaction/", responseString);
        }

        [HttpGet]
        public async Task<IActionResult> Get(/*[FromQuery] string? _id = null*/)
        {
            //_httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b0dc8eb7924540e1913ab262b8500721");
            //_httpClient.DefaultRequestHeaders.Add("ApplicationKey", "5d9b85f784c9d000019a9bff");
            //_httpClient.DefaultRequestHeaders.Add("ApplicationToken", "5d9b6bd284c9d000019a9bfd");
            //_httpClient.DefaultRequestHeaders.Add("SCLocation", "0,0");
            //_httpClient.DefaultRequestHeaders.Add("SCOrigen", "Qa");
            //_httpClient.DefaultRequestHeaders.Add("country", "co");
            //_httpClient.DefaultRequestHeaders.Add("Cookie", "__cf_bm=EqZ.c_ccNeK5MO9yB1DnT3NOxDvg6Hwc5Bv3O5z2QdY-1720470996-1.0.1.1-sLHGLmOgTDC59EjsFlNS0Q6BNcC2rctfSMPPwakOUNF5N691gxAfc9IGfhtZV1CuaV6eqMBEGWAsi_O2yUzJjA");
            //var response = await _httpClient.GetAsync("https://localhost:7083/api/Transaction/Get?_id=" + _id + "");
            //var responseString = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    return Ok(responseString);
            //}
            //else
            //{
            //    return BadRequest(responseString);
            //}
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                Headers = {
                    { "Ocp-Apim-Subscription-Key", "b0dc8eb7924540e1913ab262b8500721" },
                    { "ApplicationKey", "5d9b85f784c9d000019a9bff" },
                    { "ApplicationToken", "5d9b6bd284c9d000019a9bfd" },
                    { "SCLocation", "0,0" },
                    { "SCOrigen", "Qa" },
                    { "country", "co" },
                },
                RequestUri = new Uri("https://devapi.credinet.co/pay/GetTransactionResponse?transactionId=669041565d2baa5ccce8c7ec"),
            };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(true);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return Ok(responseBody);
        }
    }
}
