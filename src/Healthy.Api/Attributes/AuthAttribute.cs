﻿using Microsoft.AspNetCore.Authorization;

namespace Healthy.Api.Attributes
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string scheme, string policy = "") : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}