using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Application.DTOs.Entries;
using Application.Interfaces.Infrastructure.RestService;
using Newtonsoft.Json;

namespace Infrastructure.Services.Rest
{
    public class CreateAdapter : ICreateRepository
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IClientService _clientService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientService"></param>
        public CreateAdapter(IClientService clientService) 
        { 
            _clientService = clientService;
        }

        /// <summary>
        /// PostCreateAdapter
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<CommandResponse<T>> PostCreateAdapter<T>(dynamic transaction)
        {
            TransactionInput transactionInput = new()
            {
                Invoice = transaction.Invoice,
                Description = transaction.Description,
                PaymentMethod = new()
                {
                    PaymentMethodId = 1,
                    BankCode = 1077,
                    UserType = 0,
                },
                Sandbox = new()
                {
                    IsActive = false,
                    Status = ""
                },
                Currency = transaction.Currency,
                Value = transaction.Value,
                UrlResponse = transaction.UrlResponse,
                UrlConfirmation = transaction.UrlConfirmation,
                MethodConfirmation = transaction.MethodConfirmation,
                Client = new()
                {
                    DocType = transaction.DocumentType,
                    Document = transaction.Document,
                    Name = transaction.Name,
                    LastName = transaction.LastName,
                    Email = transaction.Email,
                    IndCountry = transaction.IndCountry,
                    Phone = transaction.Phone,
                    Country = transaction.Country,
                    City = transaction.City,
                    Address = transaction.Address,
                    IpAddress = transaction.IpAddress,
                }
            };

            CommandResponse<T> commandResponse = new();
            Dictionary<string, string> headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(transaction.Headers);
            headers.Add("Authorization", "Bearer ");
            HttpResponseMessage httpResponseMessage = await _clientService.PostServiceAsync("https://devapi.credinet.co/pay/create",
                transactionInput, headers);
            T genericOutput = default!;
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            genericOutput = JsonConvert.DeserializeObject<T>(response)!;
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            commandResponse.ItemInOutput = new Hashtable
            {
                {typeof(T), genericOutput },
                {typeof(TransactionInput), transactionInput },
                {typeof(HttpStatusCode), statusCode }
            };
            return commandResponse;
        }
    }
}
