namespace PicturesTask.Infrastructure.Entities
{
    public class Friend
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string User1 { get; set; }

        public string User2 { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
