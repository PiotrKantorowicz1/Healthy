using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Diets.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Diets;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task<Maybe<Product>> GetByIdAsync(Guid id);
        Task<Maybe<Product>> GetByNameAsync(string name);
        Task<Maybe<PagedResult<Product>>> BrowseAsync(BrowseProducts query);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}