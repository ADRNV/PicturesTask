using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class FriendMappingConfiguration : Profile
    {
        public FriendMappingConfiguration()
        {
            CreateMap<EntityFriend, CoreFriend>();
        }
    }
}
