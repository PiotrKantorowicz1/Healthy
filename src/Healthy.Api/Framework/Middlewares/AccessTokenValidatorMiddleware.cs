﻿using Healthy.Write.Services.Users.Abstract;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Healthy.Api.Framework.Middlewares
{
    public class AccessTokenValidatorMiddleware : IMiddleware
    {
        private readonly IAccessTokenService _accessTokenService;

        public AccessTokenValidatorMiddleware(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _accessTokenService.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}