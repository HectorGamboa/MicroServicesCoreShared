using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Core.Commons.Behaviours;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shared.Core.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Shared.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesShared<TContext>( this IServiceCollection services, IConfiguration configuration, string fileName) where TContext : DbContext
        {

           //services.AddDbContext<TContext>( options => options
           //.UseMySQL(configuration.GetConnectionString("DefaultConnection")!, b => b.MigrationsAssembly(typeof(TContext).Assembly.GetName().Name)));

            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SmartStoreManagementConnection")!,
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(TContext).Assembly.GetName().Name)
                )
            );

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .WriteTo.Debug()
              .WriteTo.Console()
              .WriteTo.File(path: $"{fileName}-.txt",
              restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
              rollingInterval: RollingInterval.Day)
              .CreateLogger();
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddAuthentication(configuration);
            return services;        
        }
    }
}
