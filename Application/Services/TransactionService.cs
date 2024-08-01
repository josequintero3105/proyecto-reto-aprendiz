using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.Runtime.Internal;
using Application.Common.FluentValidations.Extentions;
using Application.Common.Helpers.Exceptions;
using Application.Common.Helpers.Policities;
using Application.DTOs.Commands;
using Application.DTOs.Common;
using Application.Interfaces.Infrastructure.Commands;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Infrastructure.RestService;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Core.Entities.MongoDB;
using Core.Enumerations;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver.Core.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Application.DTOs.ApiEntities.Output;
using Application.DTOs.ApiEntities.Input;
using Application.DTOs.ApiEntities.Response;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Constructor
        /// </summary>
        
        public TransactionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Pasarela");
        }
        /// <summary>
        /// Process Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<TransactionOutput> ProcessTransaction(TransactionInput transactionInput)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = new LowerCaseNamingPolicy(), WriteIndented = true };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = System.Text.Json.JsonSerializer.Serialize(transactionInput, options);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("create", content);
            var response = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonObject = JObject.Parse(response);
                var data = jsonObject["data"]!.ToString();
                return System.Text.Json.JsonSerializer.Deserialize<TransactionOutput>(data, options)!;
            }
            else if (responseMessage.StatusCode.Equals(200))
                throw new BusinessException(nameof(GateWayBusinessException.InternalServerError),
                    nameof(GateWayBusinessException.InternalServerError));
            else
                throw new BusinessException(nameof(GateWayBusinessException.TransactionAttemptFailed),
                    nameof(GateWayBusinessException.TransactionAttemptFailed));
        }

        /// <summary>
        /// Get Transaction Response
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<TransactionResponse> GetTransaction(string _id)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = new LowerCaseNamingPolicy(), WriteIndented = true };
            var responseMessage = await _httpClient.GetAsync($"GetTransactionResponse?transactionId={_id}");
            var response = await responseMessage.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(response);
            var data = jsonObject["data"]!.ToString();
            if (responseMessage.IsSuccessStatusCode)
                return System.Text.Json.JsonSerializer.Deserialize<TransactionResponse>(data, options)!;
            else
                throw new BusinessException(nameof(GateWayBusinessException.TransactionAttemptFailed),
                    nameof(GateWayBusinessException.TransactionAttemptFailed));
        }
    }
}
