using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries
{
    public class GetNameAvailability : GetNameAvailabilityBase, IQuery<AvailableResourceDto>
    {      
    }
}