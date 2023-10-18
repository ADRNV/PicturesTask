using Microsoft.AspNetCore.Identity;

namespace PicturesTask.Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Friend> FriendsOf { get; set; }

        public ICollection<Friend> Friends { get; set; }

        public List<Image> Images { get; set; }

        public List<Invation> Invations { get; set; }
    }
}
