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
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.Common;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
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

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly ICommandEventRepository _commandEventRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IGetRepository _getRepository;
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Constructor
        /// </summary>
        
        public TransactionService(IHttpClientFactory httpClientFactory, ILogger<TransactionService> logger, ICommandEventRepository commandEventRepository, IGetRepository getRepository)
        {
            _logger = logger;
            _commandEventRepository = commandEventRepository;
            _getRepository = getRepository;
            _httpClient = httpClientFactory.CreateClient("Pasarela");
        }

        public async Task<TransactionOutput> ProcessTransaction(TransactionInput transactionInput)
        {
            var response = await _httpClient.PostAsJsonAsync("create", transactionInput);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TransactionOutput>();
        }

        public async Task<TransactionResponse> CheckTransactionStatus(string _id)
        {
            var response = await _httpClient.GetAsync($"GetTransactionResponse?transactionId={_id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TransactionResponse>();
        }

        /// <summary>
        /// Create Transaction
        /// </summary>
        /// <param name="transactionInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TransactionOutput> CreateTransaction(TransactionInput transactionInput)
        {
            CommandResponse<GenericOutput<TransactionOutput, ErrorOutput>> commandResponseCreateTransaction =
                await _commandEventRepository.ExecuteCreateTransaction<GenericOutput<TransactionOutput, ErrorOutput>>(transactionInput);
            PaymentMethod paymentMethod = (PaymentMethod)commandResponseCreateTransaction.ItemInOutput![typeof(PaymentMethod)]!;
            GenericOutput<TransactionOutput, ErrorOutput> StatusOutput = (GenericOutput<TransactionOutput, ErrorOutput>)commandResponseCreateTransaction.ItemInOutput![typeof(GenericOutput<TransactionOutput, ErrorOutput>)]!;

            TransactionOutput transactionOutput = new()
            {
                _id = string.Empty,
                Invoice = transactionInput.Invoice,
                Description = transactionInput.Description,
                PaymentMethod = paymentMethod,
                TransactionStatus = (StatusOutput.data != null) ? TransactionCoreStatus.Pending.ToString() : TransactionCoreStatus.Rejected.ToString(),
                CreationDate = DateTime.Now,
                Currency = transactionInput.Currency,
                Value = transactionInput.Value,
                UrlResponse = transactionInput.UrlResponse,
                UrlConfirmation = transactionInput.UrlConfirmation,
                MethodConfirmation = transactionInput.MethodConfirmation,
            };
            return transactionOutput;
        }

        /// <summary>
        /// GetTransactionById
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HttpResponseMessage> GetTransactionById(dynamic path, dynamic _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    CommandResponse<GenericOutput<TransactionOutput, ErrorOutput>> commandResponseCreateTransaction =
                        await _commandEventRepository.ExecuteCreateTransaction<GenericOutput<TransactionOutput, ErrorOutput>>(path);
                    GenericOutput<TransactionOutput, ErrorOutput> StatusOutput = (GenericOutput<TransactionOutput, ErrorOutput>)commandResponseCreateTransaction.ItemInOutput![typeof(GenericOutput<TransactionOutput, ErrorOutput>)]!;
                    HttpResponseMessage response = await _getRepository.GetTAdapter(path, _id);
                    return response;
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.TransactionIdCannotBeNull),
                    nameof(GateWayBusinessException.TransactionIdCannotBeNull));
            }
            catch (HttpRequestException)
            {
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdNotFound),
                    nameof(GateWayBusinessException.TransactionIdNotFound));
            }
        }
    }
}
