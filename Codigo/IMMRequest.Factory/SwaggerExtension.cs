namespace IMMRequest.Factory
{
    using System;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerExtension
    {
        public static IServiceCollection AddImmRequestSwagger(this IServiceCollection services, string assemblyName)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "IMM Request API", Version = "v1" });

                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
