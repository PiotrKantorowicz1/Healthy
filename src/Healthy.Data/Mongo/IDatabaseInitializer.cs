using System.Threading.Tasks;

namespace Healthy.Data.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}