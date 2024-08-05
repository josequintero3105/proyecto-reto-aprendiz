using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.ApiEntities.Input;

namespace Application.Interfaces.Infrastructure.RestService
{
    public interface IClientService
    {
        Task<HttpResponseMessage> PostServiceAsync(string url, TransactionInput transactionInput, IDictionary<string, string> headers);
        Task<HttpResponseMessage> GetServiceAsync(string url, string path, NameValueCollection? queryString = null, IDictionary<string, string>? headers = null);
        Task<T> GetServiceAsync<T>(string url, string path, NameValueCollection? queryString = null, IDictionary<string, string>? headers = null);
    }
}
