using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Users.Enumerations
{
    public class ProviderType : Enumeration
    {
        public static ProviderType Healthy = new HealthyProvider();
        public static ProviderType Facebook = new FacebookProvider();

        public ProviderType(int id, string name)
            : base(id, name)
        {
        }

        private class HealthyProvider : ProviderType
        {
            public HealthyProvider() : base(1, "healthy") { }
        }

        private class FacebookProvider : ProviderType
        {
            public FacebookProvider() : base(2, "facebook") { }
        }
    }
}
