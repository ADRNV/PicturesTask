using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PicturesTask.Infrastructure.Entities;

namespace PicturesTask.Infrastructure.Repositories
{
    public class FriendsRepository : RepositoryBase<CoreFriend>
    {
        private readonly IMapper _mapper;

        public FriendsRepository(UsersContext context, IMapper mapper) : base(context)
        { 
            _mapper = mapper;
        }

        public override async Task Create(CoreFriend enttity)
        {
            var dbFriend = MapToEntity(enttity);

            await _usersContext.AddAsync(dbFriend);

            await Save(dbFriend, EntityState.Added);
        }

        public override async Task<IEnumerable<CoreFriend>> Get(int page, int size)
        {
            var friends = _usersContext.Friends
                .AsNoTracking()
                .Include(e => e.Users)
                .Skip((page - 1) * size)
                .Take(size)
                .AsEnumerable();

            return await Task.FromResult(_mapper.Map<IEnumerable<EntityFriend>, IEnumerable<CoreFriend>>(friends));
        }

        public async override Task<CoreFriend> Get(string id)
        {
            var friend = await _usersContext.Friends
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);

            return MapToCore(friend);
        }    

        public override async Task Update(CoreFriend enttity)
        {
            var dbFriend = MapToEntity(enttity);

            _usersContext.Update(dbFriend);

            await Save(dbFriend, EntityState.Modified);
        }

        private EntityFriend MapToEntity(CoreFriend friend) =>
           _mapper.Map<CoreFriend, EntityFriend>(friend);

        private CoreFriend MapToCore(EntityFriend friend) =>
            _mapper.Map<EntityFriend, CoreFriend>(friend);


    }
}
