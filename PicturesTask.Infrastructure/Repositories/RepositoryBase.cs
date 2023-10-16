using Microsoft.EntityFrameworkCore;
using PicturesTask.Core.Repositories;

namespace PicturesTask.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected UsersContext _usersContext;
        
        public RepositoryBase(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public abstract Task Create(T enttity);

        public abstract Task<IEnumerable<T>> Get(int page, int size);
        
        public abstract Task<T> Get(string id);
        
        public abstract Task Update(T enttity);

        protected virtual async Task Save<T>(T entity, EntityState entityState)
        {
            _usersContext.Entry(entity).State = entityState;

            await _usersContext.SaveChangesAsync();
        }
    }
}
