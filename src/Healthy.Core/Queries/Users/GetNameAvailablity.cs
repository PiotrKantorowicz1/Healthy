using Healthy.Common.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetNameAvailability : IQuery
    {
        public string Name { get; set; }
    }
}