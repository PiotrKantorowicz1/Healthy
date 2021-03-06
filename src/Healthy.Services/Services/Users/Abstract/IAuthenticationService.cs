using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Types;
using Healthy.Infrastructure.Security;

namespace Healthy.Services.Services.Users.Abstract
{
    public interface IAuthenticationService : IService
    {
        Task<Maybe<UserSession>> GetSessionAsync(Guid id);

        Task<Maybe<JwtSession>> HandleSessionAsync(Guid sessionId);

        Task SignInAsync(Guid sessionId, string email, string password,
            string ipAddress = null, string userAgent = null);

        Task SignInViaFacebookAsync(Guid sessionId, string accessToken,
            string ipAddress = null, string userAgent = null);

        Task SignOutAsync(Guid sessionId, Guid userId);

        Task CreateSessionAsync(Guid sessionId, Guid userId,
            string ipAddress = null, string userAgent = null);

        Task RefreshSessionAsync(Guid sessionId, Guid newSessionId, 
            string sessionKey, string ipAddress = null, string userAgent = null);
    }
}