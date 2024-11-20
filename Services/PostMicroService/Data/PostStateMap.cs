using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostMicroService.States;

namespace PostMicroService.Data
{
    public class PostStateMap : SagaClassMap<PostStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<PostStateInstance> entityTypeBuilder, ModelBuilder modelBuilder)
        {
            entityTypeBuilder.Property(x => x.CurrentState).HasMaxLength(64);
            entityTypeBuilder.HasIndex(x => x.Id).IsUnique();
            entityTypeBuilder.Property(x => x.Id).IsRequired();
            entityTypeBuilder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}