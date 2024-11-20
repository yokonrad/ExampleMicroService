using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PostMicroService.Data;
using PostMicroService.Repositories;
using PostMicroService.Services;
using PostMicroService.States;
using Shared.Filters;

namespace PostMicroService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers(o => o.Filters.Add(typeof(ExceptionFilter))).AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                o.SerializerSettings.MaxDepth = 1;
            });
            builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<AppDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });

                x.AddSagaStateMachine<PostStateMachine, PostStateInstance>().EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Pessimistic;

                    r.AddDbContext<DbContext, PostStateDbContext>((_, c) => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
            builder.Services.AddSwaggerGen(o => o.SwaggerDoc("v1", new OpenApiInfo { Title = "Post Service API", Version = "v1" }));

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