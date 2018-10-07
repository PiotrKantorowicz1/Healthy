using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Workouts.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Diets;
using Healthy.Core.Queries.Workouts;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.Repositories
{
    public interface IWorkoutPlanRepository : IRepository
    {
        Task<Maybe<WorkoutPlan>> GetByIdAsync(Guid id);
        Task<Maybe<PagedResult<WorkoutPlan>>> BrowseAsync(BrowseWorkoutPlans query);
        Task AddAsync(WorkoutPlan workoutPlan);
        Task UpdateAsync(WorkoutPlan workoutPlan);
        Task DeleteAsync(Guid id);
    }
}