using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Application.Common.Helpers.Exceptions;
using Application.Common.Helpers.Logger;
using Application.DTOs.Entries;
using Application.Interfaces.Infrastructure.RestService;
using Common.Helpers.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services.Rest
{
    public class ClientService : IClientService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ClientService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        public ClientService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger) 
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HttpResponseMessage> PostServiceAsync(string url, TransactionInput transactionInput, IDictionary<string, string> headers)
        {
            try
            {
                string json = JsonConvert.SerializeObject(transactionInput);
                HttpContent content = new StringContent(json, Encoding.UTF8);
                var _client = BuildClientHttp(headers);
                LoggerMessageDefinition.StartedServiceLog(_logger, url, json);
                HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                    LoggerMessageDefinition.FinalizedServiceLog(_logger, nameof(PostServiceAsync), response);
                else
                {
                    string error = string.Format("StatusCode: {0} -> Status: {1} -> Url: {2}",
                        (int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, url);
                    LoggerMessageDefinition.ErrorServiceLog(_logger, error, response);
                }
                return httpResponseMessage;
            }
            catch (BusinessException)
            {
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdNotFound),
                    nameof(GateWayBusinessException.TransactionIdNotFound));
            }
        }

        /// <summary>
        /// GetServiceAsync
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="queryString"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HttpResponseMessage> GetServiceAsync(string url, string path, NameValueCollection? queryString = null, IDictionary<string, string>? headers = null)
        {
            try
            {
                var _client = BuildClientHttp(headers);
                string fullUrl = BuildUrl(url, path, queryString);
                HttpResponseMessage httpResponseMessage = await _client.GetAsync(fullUrl);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                    LoggerMessageDefinition.FinalizedServiceLog(_logger, nameof(PostServiceAsync), response);
                else
                {
                    string error = string.Format("StatusCode: {0} -> Status: {1} -> Url: {2}",
                        (int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, url);
                    LoggerMessageDefinition.ErrorServiceLog(_logger, error, response);
                }
                return httpResponseMessage;
            }
            catch (BusinessException)
            {
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdNotFound),
                    nameof(GateWayBusinessException.TransactionIdNotFound));
            }
        }

        public Task<T> GetServiceAsync<T>(string url, string path, NameValueCollection? queryString = null, IDictionary<string, string>? headers = null)
        {
            throw new NotImplementedException();
        }

        private HttpClient BuildClientHttp(IDictionary<string, string>? headers)
        {
            var _client = _httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Clear();
            if (headers != null) foreach (var header in headers) _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            return _client;
        }

        private static string BuildUrl(string? baseUrl, string? path, NameValueCollection? queryString)
        {
            string fullUrl;
            if (queryString != null && queryString.Count > 0)
            {
                string query = ToQueryString(queryString);
                fullUrl = $"{baseUrl}/" + $"{path}{query}";
            }
            else
            {
                fullUrl = $"{baseUrl}/" + $"{path}";
            }
            return fullUrl;
        }

        private static string ToQueryString(NameValueCollection nvc)
        {
            var array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)!
                select string.Format(
                "{0}={1}",
                HttpUtility.UrlEncode(key),
                HttpUtility.UrlEncode(value))
                ).ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
