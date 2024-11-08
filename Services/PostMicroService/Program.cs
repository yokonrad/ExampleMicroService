using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Consumers;
using PostMicroService.Data;
using PostMicroService.Services;
using Shared.Filters;

namespace PostMicroService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers(x => x.Filters.Add(typeof(ExceptionFilter))).AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                o.SerializerSettings.MaxDepth = 1;
            });
            builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient<PostService>();
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<PostNotCreatedConsumer>();
                x.AddConsumer<PostNotDeletedConsumer>();

                x.AddEntityFrameworkOutbox<AppDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });

                x.AddTransactionalEnlistmentBus();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
                    {
                        host.Username(builder.Configuration["RabbitMq:Username"]!);
                        host.Password(builder.Configuration["RabbitMq:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            builder.Services.AddRouting(o => o.LowercaseUrls = true);
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}