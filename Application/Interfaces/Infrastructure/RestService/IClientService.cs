using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entries;

namespace Application.Interfaces.Infrastructure.RestService
{
    public interface IClientService
    {
        Task<HttpResponseMessage> PostServiceAsync(string url, TransactionInput transactionInput, IDictionary<string, string> headers);
        Task<HttpResponseMessage> GetServiceAsync(string url, string path, NameValueCollection? queryString = null, IDictionary<string, string>? headers = null);
    }
}
