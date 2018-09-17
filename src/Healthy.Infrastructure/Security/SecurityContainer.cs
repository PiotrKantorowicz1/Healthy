using Autofac;
using Healthy.Infrastructure.Extensions;
using Healthy.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Healthy.Infrastructure.Security
{
    public class SecurityContainer
    {
        public static void Register(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterType<JwtTokenHandler>()
                .As<IJwtTokenHandler>()
                .SingleInstance();

            builder.RegisterInstance(configuration.GetSettings<JwtTokenSettings>())
                .SingleInstance();
        }
    }
}