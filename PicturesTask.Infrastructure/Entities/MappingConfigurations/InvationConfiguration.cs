using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class InvationConfiguration : Profile
    {
        public InvationConfiguration() 
        {
            CreateMap<EntityInvation, CoreInvation>()
                .ForMember(i => i.Id, o => o.MapFrom(i => i.Id))
                .ForMember(i => i.From, o => o.MapFrom(i => i.From.UserName))
                .ForMember(i => i.To, o => o.MapFrom(i => i.From.UserName));
        }
    }
}
