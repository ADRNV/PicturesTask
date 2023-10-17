namespace PicturesTask.Infrastructure.Entities
{
    public class Friend
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public List<User> Users { get; set; }

        public string User1 { get; set; }

        public string User2 { get; set; }
    }
}
