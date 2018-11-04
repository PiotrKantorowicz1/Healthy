using System.Threading.Tasks;
using Healthy.Services.Services.Users.Abstract;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Infrastructure.Handlers;
using Healthy.Infrastructure.Redis;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Queries;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public sealed class GeUserHandler : IQueryHandler<GetUser, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;
        private readonly ICache _cache;

        public GeUserHandler(IUserService userService,
            IUserMapper userMapper, 
            ICache cache)
        {
            _userService = userService;
            _userMapper = userMapper;
            _cache = cache;
        }

        public async Task<UserDto> HandleAsync(GetUser query)
        {
            UserDto userDto;
            
            var userFromCache = await _cache.GetAsync<User>($"users:{query.UserId}");
            if (userFromCache.HasValue)
            {
                userDto = _userMapper.MapToUserDto(userFromCache.Value);
                return userDto;
            }

            var userFromDb = await _userService.GetAsync(query.UserId);
            userDto = _userMapper.MapToUserDto(userFromDb.Value);
            
            return userDto;
        }
    }
}