using BananaGame.Application.Abstractions.Persistence;
using BananaGame.Infrastructure;
using BananaGame.Persistance.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace BananaGame.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistanceService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b =>
                        {
                            b.MigrationsAssembly(
                                typeof(ApplicationDbContext).Assembly.FullName
                            );
                            b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        }
                    )
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            services.AddScoped<IDbConnection>(
                sp => new SqlConnection(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            // Apply any pending migrations automatically at startup
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();
                }
            }

            // services.AddHttpClient<IQuestionDataApiClient, QuestionDataApiClient>();
            // services.AddHttpClient<IComputerVisionApiClient, ComputerVisionApiClient>();

            return services;
        }
    }
}
