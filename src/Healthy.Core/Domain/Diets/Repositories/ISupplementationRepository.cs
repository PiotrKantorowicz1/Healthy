using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Diets;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Repositories
{
    public interface ISupplementationRepository : IRepository
    {
        Task<Maybe<Supplementation>> GetByIdAsync(Guid id);
        Task<Maybe<Supplementation>> GetByUserIdAsync(Guid userId);
        Task<Maybe<PagedResult<Supplementation>>> BrowseAsync(BrowseSupplementations query);
        Task AddAsync(Supplementation dailySupplementation);
        Task UpdateAsync(Supplementation dailySupplementation);
        Task DeleteAsync(Guid id);
    }
}