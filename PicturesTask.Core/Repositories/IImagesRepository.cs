using PicturesTask.Core.Models;

namespace PicturesTask.Core.Repositories
{
    public interface IImagesRepository : IRepository<Image>
    {
        public Task<Guid> Create(Image image, string userName);

        public Task<Image> Get(string id, string userName);
    }
}
