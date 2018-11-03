using System.Linq;
using System.Threading.Tasks;
using Healthy.Write.Services.Users.Abstract;
using Healthy.Core.Pagination;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Queries;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, PagedResult<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public BrowseUsersHandler(IUserService userService,
            IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<PagedResult<UserDto>> HandleAsync(BrowseUsers query)
        {
            var pagedResult = await _userService.BrowseAsync(query);
            var users = pagedResult.Value.Items.Select(p => _userMapper.MapToUserDto(p));

            return PagedResult<UserDto>.From(pagedResult.Value, users);
        }
    }
}