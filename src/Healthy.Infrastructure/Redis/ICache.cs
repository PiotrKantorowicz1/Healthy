using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healthy.Core.Types;

namespace Healthy.Infrastructure.Redis
{
    public interface ICache
    {
        Task<Maybe<T>> GetAsync<T>(string key) where T : class;
        Task<IEnumerable<T>> GetManyAsync<T>(params string[] keys) where T : class;
        Task AddToSetAsync(string key, string value);
        Task AddToSetAsync(string key, object value);
        Task AddManyToSetAsync(string key, IEnumerable<string> values);
        Task<IEnumerable<string>> GetSetAsync(string key);
        Task<IEnumerable<T>> GetSetAsync<T>(string key);
        Task RemoveFromSetAsync(string key, string value);
        Task RemoveFromSetAsync(string key, object value);
        Task<IEnumerable<string>> GetSortedSetAsync(string key, int? limit = null);
        Task AddToSortedSetAsync(string key, string value, int score, int? limit = null);
        Task RemoveFromSortedSetAsync(string key, string value);
        Task AddAsync(string key, object value, TimeSpan? expiry = null);
        Task DeleteAsync(string key);
    }
}