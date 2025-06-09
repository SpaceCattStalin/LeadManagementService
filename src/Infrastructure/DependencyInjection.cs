using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ardalis.GuardClauses;
using Application.Common.Interfaces.Identity;
using Infrastructure.Services;
using Infrastructure.UOWork;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Repositories;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Interfaces.Service;
using MassTransit;
using Infrastructure.Consumer;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? Environment.GetEnvironmentVariable("DefaultConnection");
            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped<BookingDbContextInitialiser>();

            builder.Services.AddScoped<IEmailValidator, EmailValidator>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddHttpContextAccessor();


            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<UserCreatedConsumer>();
                x.AddConsumer<UserAlreadyExistConsumer>();
                //x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
                //{
                //    o.UseSqlServer();
                //    o.UseBusOutbox();
                //});

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("user-created-queue", e =>
                    {
                        e.ConfigureConsumer<UserCreatedConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("user-already-exist-queue", e =>
                    {
                        e.ConfigureConsumer<UserAlreadyExistConsumer>(context);
                    });
                });
            });
        }
    }
}
