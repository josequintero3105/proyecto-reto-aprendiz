using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Commands;
using Application.DTOs.Common;
using Application.Interfaces.Infrastructure.RestService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.Rest
{
    public class TokenAdapter : ITokenRepository
    {
        private readonly IClientService _clientService;
        
        public TokenAdapter(IClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<CommandResponse<T>> GetTokenAdapter<T>(dynamic request)
        {
            return null!;
            //CommandResponse<T> commnadResponse = new();
            //var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>("");
            //GenericOutput<T, ErrorOutput> response = await _clientService.GetServiceAsync<GenericOutput<T, ErrorOutput>>("", request, null, headers);
            //if(response.data is not null) commnadResponse.Item = response.data;
            //return commnadResponse;
        }
    }
}
