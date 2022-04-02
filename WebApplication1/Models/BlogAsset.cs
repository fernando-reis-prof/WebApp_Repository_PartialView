namespace WebApplication1.Models
{
    public class BlogAsset
    {
        public int Id { get; set; } // Primary key
        //public byte[] Banner { get; set; }

        public int? BlogId { get; set; } // Foreign key
        public Blog Blog { get; set; } // Reference navigation
    }

}
