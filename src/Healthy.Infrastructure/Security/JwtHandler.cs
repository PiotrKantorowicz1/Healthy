﻿using Healthy.Core.Extensions;
using Healthy.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Healthy.Infrastructure.Security
{
    public class JwtHandler : IJwtHandler
    {
        private static readonly string StateClaim = "state";
        private static readonly ISet<string> DefaultClaims = new HashSet<string>
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            ClaimTypes.Role,
            StateClaim
        };

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtTokenSettings _options;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(JwtTokenSettings options)
        {
            _options = options;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.ValidAudience,
                ValidateAudience = _options.ValidateAudience,
                ValidateLifetime = _options.ValidateLifetime
            };
        }

        public JsonWebToken CreateToken(Guid userId, string role = null, string state = "active",
            IDictionary<string, string> claims = null)
        {
            var idAsString = userId.ToString("N");

            if (string.IsNullOrWhiteSpace(idAsString))
            {
                throw new ArgumentException("User id claim can not be empty.", nameof(userId));
            }

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, idAsString),
                new Claim(JwtRegisteredClaimNames.UniqueName, idAsString),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new Claim(StateClaim, state)
            };
            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var customClaims = claims?.Select(claim => new Claim(claim.Key, claim.Value))
                               ?? Enumerable.Empty<Claim>();
            jwtClaims.AddRange(customClaims);
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = expires.ToTimestamp(),
                Role = role ?? string.Empty,
                Claims = customClaims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

        public JsonWebTokenPayload GetTokenPayload(string accessToken)
        {
            _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
                out var validatedSecurityToken);
            if (!(validatedSecurityToken is JwtSecurityToken jwt))
            {
                return null;
            }

            return new JsonWebTokenPayload
            {
                Subject = jwt.Subject,
                Role = jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                State = jwt.Claims.SingleOrDefault(x => x.Type == StateClaim)?.Value,
                Expires = jwt.ValidTo.ToTimestamp(),
                Claims = jwt.Claims.Where(x => !DefaultClaims.Contains(x.Type))
                    .ToDictionary(k => k.Type, v => v.Value)
            };
        }
    }
}