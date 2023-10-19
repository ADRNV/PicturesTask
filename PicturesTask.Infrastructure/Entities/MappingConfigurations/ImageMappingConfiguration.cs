using AutoMapper;

namespace PicturesTask.Infrastructure.Entities.MappingConfigurations
{
    public class ImageMappingConfiguration : Profile
    {
        public ImageMappingConfiguration()
        {
            CreateMap<EntityImage, CoreImage>()
                .ReverseMap();
        }
    }
}
