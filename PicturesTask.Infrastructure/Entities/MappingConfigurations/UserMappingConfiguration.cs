using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class UserMappingConfiguration : Profile
    {
        public UserMappingConfiguration()
        {
            CreateMap<EntityUser, CoreUser>()
                .ForMember(u => u.Id, o => o.Ignore())
                .ForMember(u => u.Name, o => o.MapFrom(u => u.UserName))
                .ReverseMap();
        }
    }
}
