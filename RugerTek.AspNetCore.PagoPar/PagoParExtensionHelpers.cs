using Microsoft.Extensions.DependencyInjection;
using RugerTek.AspNetCore.PagoPar.Configuration;
using RugerTek.AspNetCore.PagoPar.HttpClients;
using RugerTek.AspNetCore.PagoPar.Services;
using RugerTek.AspNetCore.PagoPar.Services.Implementations;
using System;

namespace RugerTek.AspNetCore.PagoPar
{
    public static class PagoParExtensionHelpers
    {
        public static void AddPagoParServices(this IServiceCollection services, Action<PagoParConfiguration> config)
        {
            services.Configure(config);
            services.AddHttpClient<PagoParHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.pagopar.com/api/");
            });
            services.AddTransient<IPagoParService, PagoParService>();
        }
    }
}
