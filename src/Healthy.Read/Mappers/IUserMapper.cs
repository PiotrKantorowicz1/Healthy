using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Mappers
{
    public interface IUserMapper : IMapper
    {
        Task<UserDto> MapFromEntity(User entity);
    }
}