namespace IMMRequest.Factory
{
    using DataAccess.Core;
    using DataAccess.Core.Repositories;
    using DataAccess.Interfaces;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DbContextExtension
    {
        public static IServiceCollection AddImmRequestDbConnectionString(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<DbContext, IMMRequestContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
        public static IServiceCollection AddImmRequestDatabase(this IServiceCollection services)
        {
            services.AddScoped<IDbSeeder, IMMRequestDBSeeder>();
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Request>, RequestRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<Type>, TypeRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Admin>, AdminRepository>();
            services.AddScoped<IRepository<Type>, TypeRepository>();

            return services;
        }
    }
}
