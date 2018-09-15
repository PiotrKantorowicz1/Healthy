using System.Threading.Tasks;

namespace Healthy.Infrastructure.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}