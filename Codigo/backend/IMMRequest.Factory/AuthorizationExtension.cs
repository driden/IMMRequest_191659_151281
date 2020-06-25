namespace IMMRequest.Factory
{
    using Logic.Core;
    using Logic.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class AuthorizationExtension
    {
        public static IServiceCollection AddImmRequestAuthorization(this IServiceCollection services)
        {
            services.AddScoped<ISessionLogic, SessionLogic>();
            return services;
        }
    }
}
