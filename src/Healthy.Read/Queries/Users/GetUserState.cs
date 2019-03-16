using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;
using System;

namespace Healthy.Read.Queries.Users
{
    public class GetUserState : PagedQueryBase, IQuery<UserStateDto>
    {
        public Guid UserId { get; set; }
    }
}