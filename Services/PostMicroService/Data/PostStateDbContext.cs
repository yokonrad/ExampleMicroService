using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace PostMicroService.Data
{
    public class PostStateDbContext : SagaDbContext
    {
        public PostStateDbContext(DbContextOptions<PostStateDbContext> options) : base(options)
        { }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new PostStateMap(); }
        }
    }
}