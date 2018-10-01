using System;
using Healthy.Core.Domain.Users.DomainClasses;
using Microsoft.AspNetCore.Identity;

namespace Healthy.Tests.Initializers
{
    public abstract class UsersTestsInitializer
    {
        protected Avatar Avatar;
        protected OneTimeSecuredOperation OneTimeSecuredOperation;
        protected RefreshToken RefreshToken;
        protected User User;
        protected UserSession UserSession;

        protected void Initialize()
        {
            Avatar = new Avatar("kfosdkfosdkf", "skfkskjfks.jpg");
            OneTimeSecuredOperation = new OneTimeSecuredOperation(Guid.NewGuid(), "change_password", 
                "pk-admin", "sdfsdkghuidfhgfiohjgidfsjhioj", DateTime.UtcNow);
            User = new User($"{Guid.NewGuid()}", "sdfsdfd@email.com", "admin", "healthy");
            RefreshToken = new RefreshToken(User, new PasswordHasher<User>());
            UserSession = new UserSession(Guid.NewGuid(), "asIIIjfdsfKKFJ");
        }
    }
}