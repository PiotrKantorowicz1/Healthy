using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;
using System;

namespace Healthy.Read.Queries.Users
{
    public class GetUser : PagedQueryBase, IQuery<UserDto>
    {
        public Guid UserId { get; set; }
    }
}