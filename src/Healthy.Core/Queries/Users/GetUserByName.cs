using Healthy.Common.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUserByName : IQuery
    {
        public string Name { get; set; }
    }
}