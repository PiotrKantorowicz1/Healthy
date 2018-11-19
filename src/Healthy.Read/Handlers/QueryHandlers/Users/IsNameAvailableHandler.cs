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
    public sealed class IsNameAvailableHandler : IQueryHandler<GetNameAvailability, AvailableResourceDto>
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public IsNameAvailableHandler(IUserService userService,
            IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        public async Task<AvailableResourceDto> HandleAsync(GetNameAvailability query)
        {
            var available = await _userService.IsNameAvailableAsync(query.Name);
            var availableDto = _userMapper.MapToAvailableResourceDto(available);
            
            return availableDto;
        }
    }
}