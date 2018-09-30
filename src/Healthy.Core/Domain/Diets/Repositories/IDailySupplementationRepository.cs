using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Repositories
{
    public interface IDailySupplementationRepository : IRepository
    {
        Task<Maybe<DailySupplementation>> GetByIdAsync(Guid id);
        Task AddAsync(DailySupplementation dailySupplementation);
        Task UpdateAsync(DailySupplementation dailySupplementation);
        Task DeleteAsync(Guid id);
    }
}