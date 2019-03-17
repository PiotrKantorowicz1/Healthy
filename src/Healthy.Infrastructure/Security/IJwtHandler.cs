using System;
using System.Collections.Generic;

namespace Healthy.Infrastructure.Security
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(Guid userId, string role = null,
            string state = "active", IDictionary<string, string> claims = null);

        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}