using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Application.Interfaces.Infrastructure.RestService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.Rest
{
    public class GetAdapter : IGetRepository
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IClientService _clientService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientService"></param>
        public GetAdapter(IClientService clientService)
        {
            _clientService = clientService;
        }
        /// <summary>
        /// GetTAdapter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetTAdapter(dynamic request, NameValueCollection _id)
        {
            Dictionary<string, string> headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.Headers);
            HttpResponseMessage httpResponseMessage = await _clientService.GetServiceAsync("https://devapi.credinet.co/pay/",
                request, _id, headers);
            await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseMessage;
        }
    }
}
