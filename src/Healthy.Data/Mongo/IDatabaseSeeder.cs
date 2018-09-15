using System.Threading.Tasks;

namespace Healthy.Data.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}