namespace PicturesTask.Core.Repositories
{
    public interface IRepository<T>
    {
        Task Create(T enttity);

        Task Update(T enttity);

        Task<IEnumerable<T>> Get(int page, int size);

        Task<T?> Get(string id);
    }
}
