using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Entities;

namespace PostMicroService.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

            modelBuilder.Entity<Post>().HasKey(t => t.Id);
            modelBuilder.Entity<Post>().Property(t => t.Id).IsRequired();
            modelBuilder.Entity<Post>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Post>().Property(t => t.Visible).IsRequired();
            modelBuilder.Entity<Post>().Property(t => t.CreatedAt).IsRequired();
            modelBuilder.Entity<Post>().Property(t => t.UpdatedAt).IsRequired();

            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 1,
                Title = "Post #1",
                Visible = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 2,
                Title = "Post #2",
                Visible = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            modelBuilder.Entity<Post>().HasData(new Post
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
