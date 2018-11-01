using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Mappers
{
    public class UserMapper : IUserMapper
    {
        public async Task<UserDto> MapFromEntity(User entity)
        {
            var dto = new UserDto
            {
                UserId = entity.UserId,
                Email = entity.Email,
                Name = entity.Name,
                Provider = entity.Provider,
                Role = entity.Role,
                State = entity.State,
                ExternalUserId = entity.ExternalUserId,
                AvatarUrl = entity.Avatar.Url,
                CreatedAt = entity.CreatedAt,
            };

            return await Task.FromResult(dto);
        }
    }
}