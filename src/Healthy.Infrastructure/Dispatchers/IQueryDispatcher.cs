using System.Threading.Tasks;
using Healthy.Core.Pagination;

namespace Healthy.Infrastructure.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}