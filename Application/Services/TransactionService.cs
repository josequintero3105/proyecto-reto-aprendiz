using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Transactions;
using Application.Common.FluentValidations.Extentions;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.Common;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Application.Interfaces.Infrastructure.Commands;
using Application.Interfaces.Infrastructure.Mongo;
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
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICommandEventRepository _commandEventRepository;
        private readonly ILogger<TransactionService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionService(ITransactionRepository transactionRepository, ILogger<TransactionService> logger, ICommandEventRepository commandEventRepository)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
            _commandEventRepository = commandEventRepository;
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
                await _commandEventRepository.ExecuteTransactions<GenericOutput<TransactionOutput, ErrorOutput>>(
                    TransactionActions.CreateTransaction, transactionInput);

            GenericOutput<TransactionOutput, ErrorOutput> wompiOutput = (GenericOutput<TransactionOutput, ErrorOutput>)commandResponseCreateTransaction.ItemInOutput![typeof(GenericOutput<TransactionOutput, ErrorOutput>)]!;

            TransactionOutput transactionOutput = new()
            {
                Invoice = transactionInput.Invoice,
                Description = transactionInput.Description,
                PaymentMethod = new()
                {
                    PaymentMethodId = 1,
                    BankCode = 1077,
                    UserType = 0,
                },
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
        public async Task<TransactionResponse> GetTransactionById(string _id)
        {
            try
            {
                if (!String.IsNullOrEmpty(_id))
                {
                    using (var client = new HttpClient())
                    {
                        HttpResponseMessage responseMessage =
                            await client.GetAsync($"https://devapi.credinet.co/pay/GetTransactionResponse?transactionId={_id}");
                        responseMessage.EnsureSuccessStatusCode();
                        string responseBody = await responseMessage.Content.ReadAsStringAsync();
                        JObject jsonObject = JObject.Parse(responseBody);
                        dynamic innerObject = JsonConvert.DeserializeObject(responseBody)!;
                        TransactionResponse transactionResponse = new()
                        {
                            _id = jsonObject["_id"]!.ToString(),
                            invoice = jsonObject["invoice"]!.ToString(),
                            storeId = jsonObject["storeId"]!.ToString(),
                            vendorId = jsonObject["vendorId"]!.ToString(),
                            description = jsonObject["description"]!.ToString(),
                            paymentMethod = innerObject.levels["paymentMethod"],
                            transactionStatus = jsonObject["transactionStatus"]!.ToString(),
                            currency = jsonObject["currency"]!.ToString(),
                            value = double.Parse(jsonObject["value"]!.ToString()),
                            sandbox = innerObject.levels["sandBox"],
                            creationDate = (DateTime)jsonObject["creationDate"]!,
                            paymentMethodResponse = innerObject.levels["paymentMethodResponse"],
                            UrlConfirmation = jsonObject["urlConfirmacion"]!.ToString(),
                            UrlResponse = jsonObject["urlResponse"]!.ToString(),
                            MethodConfirmation = jsonObject["methodConfirmation"]!.ToString()
                        };
                        return transactionResponse;
                    }
                }
                else
                    throw new BusinessException(nameof(GateWayBusinessException.TransactionIdCannotBeNull),
                    nameof(GateWayBusinessException.TransactionIdCannotBeNull));
            }
            catch (HttpRequestException)
            {
                throw new BusinessException(nameof(GateWayBusinessException.TransactionIdCannotBeNull),
                    nameof(GateWayBusinessException.TransactionIdCannotBeNull));
            }
        }
    }
}
