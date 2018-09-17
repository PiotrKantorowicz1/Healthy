namespace Healthy.Api
{
    using Healthy.Infrastructure.Logging;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Healthy.Infrastructure.Host;

    public class Program
    {
        public static void Main(string[] args)
        {
           WebServiceHost
                .Create<Startup>(args: args)
                .UseAutofac(Framework.Bootstrapper.LifetimeScope)
                .Build()
                .Run();
        }
    }
}
