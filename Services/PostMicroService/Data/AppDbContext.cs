using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Entities;

namespace PostMicroService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Post> Posts { get; set; }

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
        }
    }
}