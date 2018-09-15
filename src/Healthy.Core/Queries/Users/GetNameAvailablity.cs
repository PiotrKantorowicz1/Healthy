using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetNameAvailability : IQuery
    {
        public string Name { get; set; }
    }
}