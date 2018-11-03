using System.Threading.Tasks;
using Healthy.Write.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Queries;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public sealed class GetUserByNameHandler : IQueryHandler<GetUser, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public GetUserByNameHandler(IUserService userService,
            IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<UserDto> HandleAsync(GetUser query)
        {
            var userFromDb = await _userService.GetAsync(query.Id);
            var userDto = _userMapper.MapToUserDto(userFromDb.Value);
            
            return userDto;
        }
    }
}