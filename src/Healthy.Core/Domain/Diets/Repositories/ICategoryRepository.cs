using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Diets;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<Maybe<Category>> GetByNameAsync(string name);
        Task<Maybe<PagedResult<Category>>> BrowseAsync(BrowseCategories query);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string name);
    }
}