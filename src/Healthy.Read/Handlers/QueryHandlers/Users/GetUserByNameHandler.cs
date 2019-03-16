using System.Threading.Tasks;
using Healthy.Services.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Mappers.Users;
using Healthy.Read.Queries;
using Healthy.Read.Queries.Users;

namespace Healthy.Read.Handlers.QueryHandlers.Users
{
    public sealed class GetUserByNameHandler : IQueryHandler<GetUserByName, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public GetUserByNameHandler(IUserService userService,
            IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<UserDto> HandleAsync(GetUserByName query)
        {
            var userFromDb = await _userService.GetByNameAsync(query.Name);
            var userDto = _userMapper.MapToUserDto(userFromDb.Value);
            
            return userDto;
        }
    }
}