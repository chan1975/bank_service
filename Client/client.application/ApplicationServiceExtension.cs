using client.application.Features.Client;
using client.application.Features.Person;
using Microsoft.Extensions.DependencyInjection;

namespace client.application
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IClientService, ClientService>();

            return services;
        }

    }
}