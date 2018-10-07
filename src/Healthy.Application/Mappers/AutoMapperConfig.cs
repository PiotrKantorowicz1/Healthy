using AutoMapper;
using Healthy.Application.Dtos.Users;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Application.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                    .ForMember(x => x.AvatarUrl, o => o.MapFrom(s => s.Avatar == null ? string.Empty : s.Avatar.Url));
                cfg.CreateMap<dynamic, AvailableResourceDto>()
                    .ForMember(x => x.IsAvailable, o => o.MapFrom(s => s));
                cfg.CreateMap<UserSession, UserSessionDto>();
            });

            return config.CreateMapper();
        }
    }
}