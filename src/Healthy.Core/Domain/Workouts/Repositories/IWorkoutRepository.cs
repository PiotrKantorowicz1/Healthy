using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Workouts.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Workouts;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.Repositories
{
    public interface IWorkoutRepository : IRepository
    {
        Task<Maybe<Workout>> GetByIdAsync(Guid id);
        Task<Maybe<PagedResult<Workout>>> BrowseAsync(BrowseWorkouts query);
        Task AddAsync(Workout workout);
        Task UpdateAsync(Workout workout);
        Task DeleteAsync(Guid id);
    }
}