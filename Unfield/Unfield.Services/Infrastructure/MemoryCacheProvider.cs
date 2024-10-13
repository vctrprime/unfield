using Microsoft.Extensions.Caching.Memory;
using Unfield.Domain.Services.Infrastructure;

namespace Unfield.Services.Infrastructure;

internal class MemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _cache;

    public MemoryCacheProvider( IMemoryCache cache )
    {
        _cache = cache;
    }

    public async Task<T?> GetOrCreateAsync<T>( string key, int minutes, Func<Task<T>> factory ) =>
        await _cache.GetOrCreateAsync<T>(
            key,
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes( minutes );
                return factory();
            } );
}