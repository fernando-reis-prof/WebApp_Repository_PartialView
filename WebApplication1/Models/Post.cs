namespace WebApplication1.Models
{
    public class Post
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; }
        public string Content { get; set; }

        public int? BlogId { get; set; } // Foreign key
        public Blog Blog { get; set; } // Reference navigation

        public IList<Tag> Tags { get; } = new List<Tag>(); // Skip collection navigation

    }
}
