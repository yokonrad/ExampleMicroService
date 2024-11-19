using CommentMicroService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CommentMicroService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

            modelBuilder.Entity<Post>().HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<Post>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Id).ValueGeneratedNever();

            modelBuilder.Entity<Post>().Ignore(p => p.Comments);
            modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.PostId).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.CreatedAt).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.UpdatedAt).IsRequired();

            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 1,
            });

            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 2,
            });

            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 3,
            });

            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 1,
                PostId = 1,
                Text = "Post #1 Comment #1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 2,
                PostId = 1,
                Text = "Post #1 Comment #2",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            });

            modelBuilder.Entity<Comment>().HasData(new Comment
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