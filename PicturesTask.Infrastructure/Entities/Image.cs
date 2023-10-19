namespace PicturesTask.Infrastructure.Entities
{
    public class Image
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Path { get; set; }

        public User User { get; set; }
    }
}
