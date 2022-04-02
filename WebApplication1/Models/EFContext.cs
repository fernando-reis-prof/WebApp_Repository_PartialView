using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class EFContext : DbContext
    {

		private string connectionString;

		public EFContext(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);
		}


		public DbSet<Blog>? Blogs { get; set; }
		public DbSet<BlogAsset>? BlogAssetses { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Tag>? Tags { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Blog>()
                .ToTable("Blogs")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Blog>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Blog>()
                .Property(p => p.Url)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            modelBuilder.Entity<Blog>()
                .HasOne(p => p.BlogAssets)
                .WithOne(p => p.Blog)
                .HasConstraintName("FK_Blog_BlogAssets")
                .HasForeignKey<BlogAsset>(b => b.BlogId);


            modelBuilder.Entity<Blog>()
                .HasMany(p => p.Posts)
                .WithOne(p => p.Blog)
                .HasConstraintName("FK_Blog_Post");

            modelBuilder.Entity<BlogAsset>()
                .ToTable("BlogAssets")
                .HasKey(p => p.Id);

            modelBuilder.Entity<BlogAsset>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<BlogAsset>()
            //    .Property(p => p.Banner)
            //    .HasColumnType("image")
            //    .IsRequired();


            modelBuilder.Entity<Post>()
                .ToTable("Posts")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Content)
                .HasColumnType("VARCHAR(1000)")
                .IsRequired();


            modelBuilder.Entity<Tag>()
                .ToTable("Tags")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Tag>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Tag>()
                .Property(p => p.Text)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();


/*
            modelBuilder.Entity<PostTag>()
                .ToTable("PostTags")
                .HasKey(p => new { p.PostId, p.TagId});
*/


            modelBuilder
                .Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Posts)
                .UsingEntity(j => j.ToTable("PostTags"));
        }
    }
}
