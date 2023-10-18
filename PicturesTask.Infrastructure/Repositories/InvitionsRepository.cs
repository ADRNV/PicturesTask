using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PicturesTask.Infrastructure.Repositories
{
    public class InvitionsRepository : RepositoryBase<CoreInvation>
    {
        private readonly IMapper _mapper;

        public InvitionsRepository(UsersContext usersContext, IMapper mapper) : base(usersContext)
        {
            _mapper = mapper;
        }

        public async Task Create(EntityUser entityUserFrom, EntityUser entityUserTo)
        {
            var dbInvation = new EntityInvation
            {
                Id = Guid.NewGuid().ToString(),
                From = entityUserFrom,
                To = entityUserTo
            };

            await _usersContext.AddAsync(dbInvation);

            await Save(dbInvation, EntityState.Added);
        }

        public override async Task<IEnumerable<CoreInvation>> Get(int page, int size)
        {
            var invations = _usersContext.Invations
               .AsNoTracking()
               .Include(e => e.From)
               .Skip((page - 1) * size)
               .Take(size)
               .AsEnumerable();

            return await Task.FromResult(_mapper.Map<IEnumerable<EntityInvation>, IEnumerable<CoreInvation>>(invations));
        }

        public async Task<IEnumerable<CoreInvation>> Get(string userName, int page, int size)
        {
            var invations = _usersContext.Invations
               .AsNoTracking()
               .Include(e => e.From)
               .Include(e => e.To)
               .Skip((page - 1) * size)
               .Take(size)
               .AsEnumerable()
               .Where(i => i.From.UserName == userName || i.To.UserName == userName);

            return await Task.FromResult(_mapper.Map<IEnumerable<EntityInvation>, IEnumerable<CoreInvation>>(invations));
        }

        public override async Task<CoreInvation> Get(string id)
        {
            var invation = await _usersContext.Invations
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);

            return MapToCore(invation);
        }

        public async Task<CoreInvation> Get(string userName, string id)
        {
            var user = await _usersContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            var invation = await _usersContext.Invations
                .Include(e => e.To)
                .Include(e => e.From)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id || f.From.Id == user.Id);

            return new CoreInvation() {
                From = invation.From.UserName,
                To = user.UserName, 
                Accepted = invation.Accepted,
                Id = id
            };
        }

        public override async Task Update(CoreInvation enttity)
        {
            var fromUser = await _usersContext.Users.FirstOrDefaultAsync(u => u.UserName == enttity.From);

            var toUser = await _usersContext.Users.FirstOrDefaultAsync(u => u.UserName == enttity.To);

            var dbInvation = new EntityInvation() {
                Id = enttity.Id,
                From = fromUser, 
                To = toUser,
                Accepted = enttity.Accepted
            };

            _usersContext.Invations.Update(dbInvation);

            await Save(dbInvation, EntityState.Modified);
        }

        private EntityInvation MapToEntity(CoreInvation invation) =>
         _mapper.Map<CoreInvation, EntityInvation>(invation);

        private CoreInvation MapToCore(EntityInvation invation) =>
            _mapper.Map<EntityInvation, CoreInvation>(invation);

        public override Task Create(CoreInvation enttity)
        {
            throw new NotImplementedException();
        }
    }
}
