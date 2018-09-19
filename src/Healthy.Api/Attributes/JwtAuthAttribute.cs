using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Healthy.Api.Attributes
{
    public class JwtAuthAttribute : AuthAttribute
    {
        public JwtAuthAttribute(string policy = "")
            : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}