using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Application.Services;
using RickAndMorty.Domain.Interfaces;
using RickAndMorty.Infra.Clients;

namespace RickAndMorty.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string baseRickAndMortyApiRoute = configuration.GetSection("BaseRickAndMortyApiRoute").Value;

            #region RickAndMorty
            services.AddScoped<IRickAndMortyService, RickAndMortyService>();
            services.AddScoped<IRickAndMortyClient, RickAndMortyClient>();

            services.AddHttpClient<IRickAndMortyClient, RickAndMortyClient>(client =>
            {
                client.BaseAddress = new Uri(baseRickAndMortyApiRoute);
            });
            #endregion

            return services;
        }
    }
}
