using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class UserMappingConfiguration : Profile
    {
        public UserMappingConfiguration()
        {
            CreateMap<EntityUser, CoreUser>()
                .ForMember(u => u.Id, o => o.Ignore())
                .ReverseMap();
        }
    }
}
