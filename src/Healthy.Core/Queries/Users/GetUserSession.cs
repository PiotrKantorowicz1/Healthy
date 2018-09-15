using System;
using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUserSession : IQuery
    {
        public Guid Id { get; set; }
    }
}