using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Diets.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Diets;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Repositories
{
    public interface IMealRepository : IRepository
    {
        Task<Maybe<Meal>> GetByIdAsync(Guid id);
        Task<Maybe<PagedResult<Meal>>> BrowseAsync(BrowseMeals query);
        Task AddAsync(Meal dailySupplementation);
        Task UpdateAsync(Meal dailySupplementation);
        Task DeleteAsync(Guid id);
    }
}