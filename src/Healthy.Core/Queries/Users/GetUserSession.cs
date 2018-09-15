using System;
using Healthy.Common.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUserSession : IQuery
    {
        public Guid Id { get; set; }
    }
}