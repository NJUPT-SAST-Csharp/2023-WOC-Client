using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Infrastructure
{
    /// <summary>
    /// 对LocalOptions里定义的的服务器发出类restful风格的http请求的接口
    /// </summary>
    internal interface IHttpClient
    {
        Task<TResponse> GetAsync<TResponse>(string path);
        Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest request);
        Task<TResponse> PutAsync<TRequest, TResponse>(string path, TRequest request);
        Task<TResponse> DeleteAsync<TResponse>(string path);
    }
}
