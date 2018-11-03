using System.Threading.Tasks;
using Healthy.Write.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers;
using Healthy.Read.Queries;

namespace Healthy.Read.Handlers.QueryHandlers
{
    public sealed class GetUserSessionHandler : IQueryHandler<GetSession, UserSessionDto>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserMapper _userMapper;

        public GetUserSessionHandler(IUserMapper userMapper, 
            IAuthenticationService authenticationService)
        {
            _userMapper = userMapper;
            _authenticationService = authenticationService;
        }

        public async Task<UserSessionDto> HandleAsync(GetSession query)
        {
            var session = await _authenticationService.GetSessionAsync(query.Id);
            var sessionDto = _userMapper.MapToUserSessionDto(session.Value);
            
            return sessionDto;
        }
    }
}