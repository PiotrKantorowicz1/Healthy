using System.Threading.Tasks;

namespace Healthy.Infrastructure.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}