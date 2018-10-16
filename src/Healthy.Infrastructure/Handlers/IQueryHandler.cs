using System.Threading.Tasks;
using Healthy.Core.Pagination;

namespace Healthy.Infrastructure.Handlers
{
    public interface IQueryHandler<in TQuery,TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}