using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class FriendsMappingConfiguration : Profile
    {
        public FriendsMappingConfiguration()
        {
            CreateMap<EntityFriend, CoreFriend>();
        }
    }
}
