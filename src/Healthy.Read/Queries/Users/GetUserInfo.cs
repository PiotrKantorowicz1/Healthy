using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;
using System;

namespace Healthy.Read.Queries.Users
{
    public class GetUserInfo : PagedQueryBase, IQuery<UserInfoDto>
    {
        public Guid UserId { get; set; }
    }
}