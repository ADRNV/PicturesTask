using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class FriendMappingConfiguration : Profile
    {
        public FriendMappingConfiguration()
        {
            CreateMap<CoreFriend, EntityFriend>()
                .ForMember(e => e.Id, o => o.Ignore())
                .ForMember(e => e.User1, o => o.MapFrom(o => o.User1))
                .ForMember(e => e.User2, o => o.MapFrom(o => o.User2))
                .ReverseMap();

        }
    }
}
