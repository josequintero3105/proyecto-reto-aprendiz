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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient = new HttpClient();

        public TransactionController(ITransactionService transactionService, IHandle handle, IHttpClientFactory httpClientFactory, HttpClient httpClient)
        {
            _transactionService = transactionService;
            _handle = handle;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionInput transactionInput)
        {
            string json = JsonConvert.SerializeObject(transactionInput);
            HttpContent content = new StringContent(json, Encoding.UTF8);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b0dc8eb7924540e1913ab262b8500721");
            _httpClient.DefaultRequestHeaders.Add("ApplicationKey", "5d9b85f784c9d000019a9bff");
            _httpClient.DefaultRequestHeaders.Add("ApplicationToken", "5d9b6bd284c9d000019a9bfd");
            _httpClient.DefaultRequestHeaders.Add("SCLocation", "0,0");
            _httpClient.DefaultRequestHeaders.Add("SCOrigen", "Qa");
            _httpClient.DefaultRequestHeaders.Add("country", "co");
            var response = await _httpClient.PostAsync("https://devapi.credinet.co/pay/create", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return Created("~/api/Transaction/", responseString);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? _id = null)
        {
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
                RequestUri = new Uri($"https://devapi.credinet.co/pay/GetTransactionResponse?transactionId={_id}"),
            };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(true);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return Ok(responseBody);
        }
    }
}
