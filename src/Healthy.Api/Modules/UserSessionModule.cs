using AutoMapper;
using Healthy.Api.Modules.Base;
using Healthy.Application.Dtos.Users;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Queries.Users;

namespace Healthy.Api.Modules
{
    public class UserSessionModule : ModuleBase
    {
        public UserSessionModule(IAuthenticationService authenticationService, IMapper mapper) 
            : base(mapper, "user-sessions")
        {
            Get("{id}", async args => await Fetch<GetUserSession, UserSession>
                (async x => await authenticationService.GetSessionAsync(x.Id))
                .MapTo<UserSessionDto>()
                .HandleAsync());
        }
    }
}