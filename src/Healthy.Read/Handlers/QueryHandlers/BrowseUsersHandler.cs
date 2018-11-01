using System;
using System.Threading.Tasks;
using Healthy.Core.Pagination;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Queries;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, PagedResult<UserDto>>
    {
        public Task<PagedResult<UserDto>> HandleAsync(BrowseUsers query)
        {
            throw new NotImplementedException();
        }
    }
}