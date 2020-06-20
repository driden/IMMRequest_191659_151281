namespace IMMRequest.Factory
{
    using Microsoft.Extensions.DependencyInjection;

    public static class CorsExtension
    {
        public static IServiceCollection AddImmRequestCors(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "CorsPolicy",
                        builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            );
                });
            return services;
        }
    }
}
