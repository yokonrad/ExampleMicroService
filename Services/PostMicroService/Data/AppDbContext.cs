using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Entities;

namespace PostMicroService.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            builder.Entity<Post>().HasKey(t => t.Id);
            builder.Entity<Post>().Property(t => t.Id).IsRequired();
            builder.Entity<Post>().Property(t => t.Title).IsRequired();
            builder.Entity<Post>().Property(t => t.Visible).IsRequired();
            builder.Entity<Post>().Property(t => t.CreatedAt).IsRequired();
            builder.Entity<Post>().Property(t => t.UpdatedAt).IsRequired();

            builder.Entity<Post>().HasData(new Post
            {
                Id = 1,
                Title = "Post #1",
                Visible = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            builder.Entity<Post>().HasData(new Post
            {
                Id = 2,
                Title = "Post #2",
                Visible = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            builder.Entity<Post>().HasData(new Post
            {
                Id = 3,
                Title = "Post #3",
                Visible = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });
        }
    }
}
