using Healthy.Api.Modules.Base;

namespace Healthy.Api.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule() : base(requireAuthentication: false)
        {
            Get("", args => "Welcome to the Healthy API!");
        }
    }
}