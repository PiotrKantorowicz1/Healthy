using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Types;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IAuthenticationService : IService
    {
        Task<Maybe<UserSession>> GetSessionAsync(Guid id);

        Task SignInAsync(Guid sessionId, string email, string password,
            string ipAddress = null, string userAgent = null);

        Task SignInViaFacebookAsync(Guid sessionId, string accessToken,
            string ipAddress = null, string userAgent = null);

        Task SignOutAsync(Guid sessionId, string userId);

        Task CreateSessionAsync(Guid sessionId, string userId,
            string ipAddress = null, string userAgent = null);

        Task RefreshSessionAsync(Guid sessionId, Guid newSessionId, 
            string sessionKey, string ipAddress = null, string userAgent = null);
    }
}