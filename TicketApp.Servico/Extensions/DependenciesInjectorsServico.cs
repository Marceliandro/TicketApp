using Microsoft.Extensions.DependencyInjection;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Servico.Extensions
{
    public static class DependenciesInjectorsServico
    {
        public static IServiceCollection AddDependenciesInjectorsServico(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<IClienteServico, ClienteServico>();
            services.AddScoped<ITicketSituacaoServico, TicketSituacaoServico>();
            services.AddScoped<ITicketServico, TicketServico>();
            services.AddScoped<ITicketAnotacaoServico, TicketAnotacaoServico>();
            return services;
        }
    }
}
