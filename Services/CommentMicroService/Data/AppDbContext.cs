using CommentMicroService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentMicroService.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>().HasIndex(p => p.Id).IsUnique();
            builder.Entity<Post>().Property(p => p.Id).IsRequired();
            builder.Entity<Post>().Property(p => p.Id).ValueGeneratedNever();

            builder.Entity<Comment>().HasKey(c => c.Id);
            builder.Entity<Comment>().Property(c => c.Id).IsRequired();
            builder.Entity<Comment>().Property(c => c.PostId).IsRequired();
            builder.Entity<Comment>().Property(c => c.Text).IsRequired();
            builder.Entity<Comment>().Property(c => c.CreatedAt).IsRequired();
            builder.Entity<Comment>().Property(c => c.UpdatedAt).IsRequired();

            builder.Entity<Comment>().Ignore(c => c.Post);
            builder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>().HasData(new Post
            {
                Id = 1,
            });

            builder.Entity<Post>().HasData(new Post
            {
                Id = 2,
            });

            builder.Entity<Post>().HasData(new Post
            {
                Id = 3,
            });

            builder.Entity<Comment>().HasData(new Comment
            {
                Id = 1,
                PostId = 1,
                Text = "Post #1 Comment #1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            builder.Entity<Comment>().HasData(new Comment
            {
                Id = 2,
                PostId = 1,
                Text = "Post #1 Comment #2",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            builder.Entity<Comment>().HasData(new Comment
            {
                Id = 3,
                PostId = 2,
                Text = "Post #2 Comment #1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });
        }
    }
}
