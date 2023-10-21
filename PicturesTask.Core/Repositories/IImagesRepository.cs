using PicturesTask.Core.Models;

namespace PicturesTask.Core.Repositories
{
    public interface IImagesRepository : IRepository<Image>
    {
        public Task<Guid> Create(string userName, MemoryStream imageFile);

        public Task<byte[]> Get(string id, string userName);
    }
}
