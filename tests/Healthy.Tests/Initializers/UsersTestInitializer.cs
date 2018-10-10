using System;
using Healthy.Core.Domain.Users.DomainClasses;
using Microsoft.AspNetCore.Identity;

namespace Healthy.Tests.Initializers
{
    public abstract class UsersTestsInitializer
    {
        protected Avatar Avatar;
        protected OneTimeSecuredOperation OneTimeSecuredOperation;
        protected User User;
        protected UserSession UserSession;

        protected void Initialize()
        {
            Avatar = new Avatar("kfosdkfosdkf", "skfkskjfks.jpg");
            OneTimeSecuredOperation = new OneTimeSecuredOperation(Guid.NewGuid(), "change_password", 
                "pk-admin", "sdfsdkghuidfhgfiohjgidfsjhioj", DateTime.UtcNow);
            User = new User($"{Guid.NewGuid()}", "sdfsdfd@email.com", "admin", "healthy");
            UserSession = new UserSession(Guid.NewGuid(), "asIIIjfdsfKKFJ");
        }
    }
}