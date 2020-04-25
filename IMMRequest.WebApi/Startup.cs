using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMMRequest.DataAccess.Core;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.DataAccess.Core.Repositories;
using IMMRequest.Logic.Core;
using IMMRequest.Logic.Interfaces;

namespace IMMRequest.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "CorsPolicy",
                        builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });

            services.AddControllers();

            services.AddDbContext<DbContext, IMMRequestContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "IMM Request API", Version = "v1" });
            });

            // Database Injections
            services.AddScoped<IDbSeeder, IMMRequestDBSeeder>();
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Request>, RequestRepository>();
            services.AddScoped<IAreaQueries, AreaRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<Domain.Type>, TypeRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();

            // Logic Injection
            services.AddScoped<IRequestsLogic, RequestsLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "IMM Request API");
            });

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbSeeder = scope.ServiceProvider.GetService<IDbSeeder>();
                dbSeeder.Seed();
            }
        }
    }
}
