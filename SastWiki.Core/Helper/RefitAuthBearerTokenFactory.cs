using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Helper
{
    public static class RefitAuthBearerTokenFactory
    {
        private static Func<CancellationToken, Task<string>>? _getBearerTokenAsyncFunc;

        public static void SetBearerTokenGetterFunc(
            Func<CancellationToken, Task<string>> getBearerTokenAsyncFunc
        ) => _getBearerTokenAsyncFunc = getBearerTokenAsyncFunc;

        public static Task<string> GetBearerTokenAsync(CancellationToken cancellationToken)
        {
            if (_getBearerTokenAsyncFunc is null)
                throw new InvalidOperationException("先设置getBearerTokenAsyncFunc的方法");
            return _getBearerTokenAsyncFunc!(cancellationToken);
        }
    }
}
