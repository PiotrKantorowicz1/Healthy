using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Workouts.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Diets;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Workouts.Repositories
{
    public interface IExerciseRepository : IRepository
    {
        Task<Maybe<Exercise>> GetByIdAsync(Guid id);
        Task<Maybe<PagedResult<Exercise>>> BrowseAsync(BrowseMeals query);
        Task AddAsync(Exercise exercise);
        Task UpdateAsync(Exercise exercise);
        Task DeleteAsync(Guid id);
    }
}