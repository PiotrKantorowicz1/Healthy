using System.Threading.Tasks;
using Healthy.Services.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers.Users;
using Healthy.Read.Queries.Users;

namespace Healthy.Read.Handlers.QueryHandlers.Users
{
    public sealed class GetUserInfoByNameHandler : IQueryHandler<GetUserInfoByName, UserInfoDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public GetUserInfoByNameHandler(IUserService userService,
            IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<UserInfoDto> HandleAsync(GetUserInfoByName query)
        {
            var userInfo = await _userService.GetByNameAsync(query.Name);
            var userInfoDto = _userMapper.MapToUserInfoDto(userInfo.Value);
            
            return userInfoDto;
        }
    }
}