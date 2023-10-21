using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PicturesTask.Infrastructure.Repositories
{
    public class FriendsRepository : RepositoryBase<CoreFriend>
    {
        public FriendsRepository(UsersContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task Create(CoreFriend enttity)
        {
            var dbFriend = MapToEntity(enttity);

            var user = await _usersContext.Users.FirstAsync(u => u.UserName == enttity.User2);

            user.Friends.Add(dbFriend);

            await _usersContext.AddAsync(dbFriend);

            await Save(dbFriend, EntityState.Added);
        }

        public override async Task<IEnumerable<CoreFriend>> Get(int page, int size)
        {
            var friends = _usersContext.Friends
                .AsNoTracking()
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
