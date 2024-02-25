namespace SastWiki.Core.Helper;

public static class RefitAuthBearerTokenFactory
{
    private static Func<CancellationToken, Task<string>>? _getBearerTokenAsyncFunc;

    public static void SetBearerTokenGetterFunc(
        Func<CancellationToken, Task<string>> getBearerTokenAsyncFunc
    ) => _getBearerTokenAsyncFunc = getBearerTokenAsyncFunc;

    public static Task<string> GetBearerTokenAsync(CancellationToken cancellationToken) =>
        _getBearerTokenAsyncFunc is null
            ? throw new InvalidOperationException("先设置getBearerTokenAsyncFunc的方法")
            : _getBearerTokenAsyncFunc!(cancellationToken);
}
