using System.Threading.Tasks;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Queries;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public class GetUserStateHandler : IQueryHandler<GetUserState, UserStateDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public GetUserStateHandler(IUserService userService, IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<UserStateDto> HandleAsync(GetUserState query)
        {
            var state = await _userService.GetStateAsync(query.UserId);
            var userStateDto = _userMapper.MapToUserStateDto(state.Value);
            return userStateDto;
        }
    }
}