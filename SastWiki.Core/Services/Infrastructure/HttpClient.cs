using SastWiki.Core.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Infrastructure
{
    public class HttpClient : IHttpClient
    {
        public HttpClient() { }

        public Task<TResponse> DeleteAsync<TResponse>(string path)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> GetAsync<TResponse>(string path)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PutAsync<TRequest, TResponse>(string path, TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
