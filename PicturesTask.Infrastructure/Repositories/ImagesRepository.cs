using AutoMapper;
using PicturesTask.Core.Models;
using PicturesTask.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PicturesTask.Infrastructure.Repositories
{
    public class ImagesRepository : RepositoryBase<CoreImage>, IImagesRepository
    {
        public ImagesRepository(UsersContext usersContext, IMapper mapper) : base(usersContext, mapper)
        {
        }
        public async Task<Guid> Create(CoreImage image, string userName)
        {
            var dbImage = MapToEntity<EntityImage>(image);

            dbImage.User = await _usersContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            await _usersContext.Images.AddAsync(dbImage);

            await Save(dbImage, EntityState.Added);

            return Guid.Parse(dbImage.Id);
        }

        public override async Task Create(Image enttity)
        {
            var dbImage = MapToEntity<EntityImage>(enttity);

            await _usersContext.Images.AddAsync(dbImage);

            await Save(dbImage, EntityState.Added);
        }

        public async Task<CoreImage> Get(string id, string userName)
        {
            var image = await _usersContext.Images
                .Include(i => i.User)
                .Include(i => i.User.Friends)
                .Where(i => i.Id == id)
                .FirstAsync();

            var user = await _usersContext.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            var isOwner = user == image.User;

            if(isOwner)
            {
                return MapToCore<EntityImage>(image);
            }
            else
            {
                if(image.User.Friends.Where(f => f.User2 == user.UserName).Count() != 0)
                {
                    return MapToCore(image);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

                      
        }

        public override Task<IEnumerable<Image>> Get(int page, int size)
        {
            throw new NotImplementedException();
        }

        public override Task<Image> Get(string id)
        {
            throw new NotImplementedException();
        }

        public override Task Update(Image enttity)
        {
            throw new NotImplementedException();
        }
    }
}
