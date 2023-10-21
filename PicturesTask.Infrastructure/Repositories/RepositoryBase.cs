using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PicturesTask.Core.Repositories;

namespace PicturesTask.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected UsersContext _usersContext;

        protected readonly IMapper _mapper;

        public RepositoryBase(UsersContext usersContext, IMapper mapper)
        {
            _usersContext = usersContext;

            _mapper = mapper;
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

        protected D MapToEntity<D>(T invation) =>
         _mapper.Map<T, D>(invation);

        protected T MapToCore<D>(D invation) =>
            _mapper.Map<D, T>(invation);
    }
}
