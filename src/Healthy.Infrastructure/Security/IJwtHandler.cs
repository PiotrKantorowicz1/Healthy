﻿using System.Collections.Generic;

namespace Healthy.Infrastructure.Security
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null,
            IDictionary<string, string> claims = null);

        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}