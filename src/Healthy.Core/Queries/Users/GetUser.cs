using Healthy.Common.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUser : IQuery
    {
        public string Id { get; set; }
    }
}