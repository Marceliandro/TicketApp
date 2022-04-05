using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Infra.Contextos;
using TicketApp.Infra.Repositorios;

namespace TicketApp.Infra.Extensions
{
    public static class DependenciesInjectorsInfra
    {
        public static IServiceCollection AddDependenciesInjectorsInfra(this IServiceCollection services)
        {
            services.AddDbContext<TicketContexto>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<ITicketAnotacaoRepositorio, TicketAnotacaoRepositorio>();
            services.AddScoped<ITicketRepositorio, TicketRepositorio>();
            services.AddScoped<ITicketSituacaoRepositorio, TicketSituacaoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsuarioDTO, Usuario>().ReverseMap();
                cfg.CreateMap<ClienteDTO, Cliente>().ReverseMap();
                cfg.CreateMap<TicketSituacaoDTO, TicketSituacao>().ReverseMap();
                cfg.CreateMap<TicketDTO, Ticket>().ReverseMap();
                cfg.CreateMap<TicketAnotacaoDTO, TicketAnotacao>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
