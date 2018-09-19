using Microsoft.Extensions.Configuration;

namespace Healthy.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration, string section = "") where T : new()
        {
            if (string.IsNullOrWhiteSpace(section))
            {
                section = typeof(T).Name.Replace("Settings", string.Empty).Replace("Configuration", string.Empty);
            }

            var configurationValue = new T();
            configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}