namespace IMMRequest.WebApi
{
    using System.Reflection;
    using DataAccess.Core;
    using Factory;
    using Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter), 1);
                options.Filters.Add(typeof(LogicExceptionFilter), 2);
                options.Filters.Add(typeof(SystemExceptionFilter), 3);
            });


            services.AddImmRequestSwagger(Assembly.GetExecutingAssembly().GetName().Name);
            services.AddImmRequestCors();
            services.AddImmRequestDbConnectionString(Configuration.GetConnectionString("DefaultConnection"));
            services.AddImmRequestDatabase();
            services.AddImmRequestLogic();
            services.AddImmRequestAuthorization();

            services.AddScoped<DomainExceptionFilter>();
            services.AddScoped<LogicExceptionFilter>();
            services.AddScoped<SystemExceptionFilter>();
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
                conf.RoutePrefix = "swagger";
            });

            app.UseCors("CorsPolicy");

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbSeeder = scope.ServiceProvider.GetService<IDbSeeder>();
                dbSeeder.Seed();
            }
        }
    }
}
