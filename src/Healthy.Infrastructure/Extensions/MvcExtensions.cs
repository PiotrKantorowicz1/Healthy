
using Microsoft.Extensions.Configuration;

namespace Healthy.Infrastructure.Extensions
{
    public static class MvcExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            
            return model;
        }
    }
}