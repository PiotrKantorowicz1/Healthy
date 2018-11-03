using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Mappers
{
    public interface IUserMapper : IMapper
    {
        UserDto MapToUserDto(User entity);
        UserInfoDto MapToUserInfoDto(User entity);
        AvailableResourceDto MapToAvailableResourceDto(bool availableResource);
        UserSessionDto MapToUserSessionDto(UserSession entity);
        UserStateDto MapToUserStateDto(User user);
    }
}