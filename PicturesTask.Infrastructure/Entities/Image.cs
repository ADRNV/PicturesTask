namespace PicturesTask.Infrastructure.Entities
{
    public class Image
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public User User { get; set; }
    }
}
