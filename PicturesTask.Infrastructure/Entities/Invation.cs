namespace PicturesTask.Infrastructure.Entities
{
    public class Invation
    {
        public string Id { get; set; }

        public User From { get; set; }

        public User To { get; set; }

        public bool Accepted { get; set; }
    }
}
