using AutoMapper;
using PicturesTask.Core.Models;
using PicturesTask.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Drawing.Imaging;

namespace PicturesTask.Infrastructure.Repositories
{
    public class ImagesRepository : RepositoryBase<CoreImage>, IImagesRepository
    {
        private readonly FileStoreOptions _fileStoreOptions;

        public ImagesRepository(UsersContext usersContext, IMapper mapper, FileStoreOptions fileStoreOptions) : base(usersContext, mapper)
        {
            _fileStoreOptions = fileStoreOptions;
        }

        public async Task<Guid> Create(string userName, MemoryStream imageFile)
        {
            var id = Guid.NewGuid().ToString();

            var path = $@"{_fileStoreOptions.PathToSave}\{userName}_{id}.jpeg";

            var dbImage = new EntityImage {
                Path = path,
                Id = id,
            };

            dbImage.User = await _usersContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            await _usersContext.Images.AddAsync(dbImage);

            await Save(dbImage, EntityState.Added);

            var img = System.Drawing.Image.FromStream(imageFile);

            img.Save(path, ImageFormat.Jpeg);
            
            return Guid.Parse(dbImage.Id);
        }

        public override async Task Create(Image enttity)
        {
            var dbImage = MapToEntity<EntityImage>(enttity);

            await _usersContext.Images.AddAsync(dbImage);

            await Save(dbImage, EntityState.Added);
        }

        public async Task<byte[]> Get(string id, string userName)
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

            var imageFile = await File.ReadAllBytesAsync(image.Path);

            if (isOwner)
            {
                return imageFile;
            }
            else
            {
                if(image.User.Friends.Where(f => f.User1 == user.UserName).Count() != 0)
                {
                    return imageFile;
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
