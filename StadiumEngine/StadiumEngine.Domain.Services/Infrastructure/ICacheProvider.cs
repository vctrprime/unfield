namespace StadiumEngine.Domain.Services.Infrastructure;

public interface ICacheProvider
{
    Task<T?> GetOrCreateAsync<T>( string key, int minutes, Func<Task<T>> factory );
}