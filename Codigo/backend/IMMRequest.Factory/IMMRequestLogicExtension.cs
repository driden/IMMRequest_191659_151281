namespace IMMRequest.Factory
{
    using Logic.Core;
    using Logic.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class IMMRequestLogicExtension
    {
        public static IServiceCollection AddImmRequestLogic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRequestsLogic, RequestsLogic>();
            serviceCollection.AddScoped<ITypesLogic, TypesLogic>();
            serviceCollection.AddScoped<IAdminsLogic, AdminsLogic>();
            serviceCollection.AddScoped<IAreasLogic, AreasLogic>();
            serviceCollection.AddScoped<ITopicsLogic, TopicsLogic>();
            serviceCollection.AddScoped<ITypesLogic, TypesLogic>();
            serviceCollection.AddScoped<IReportsLogic, ReportsLogic>(); 
            serviceCollection.AddScoped<IImportsLogic, ImportsLogic>(); 

            return serviceCollection;
        }
    }
}
