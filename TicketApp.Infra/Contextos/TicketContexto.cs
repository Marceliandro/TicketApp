using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using TicketApp.Base;

namespace TicketApp.Infra.Contextos
{
    public class TicketContexto : BaseContext
    {
        private readonly IConfiguration _configuration;
        protected Assembly ConfigurationAssembly => Assembly.GetExecutingAssembly();
        public TicketContexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (ConfigurationAssembly == null)
                throw new ArgumentNullException("O Assembly de configuração do contexto deve ser definido.", new Exception());

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(ConfigurationAssembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["Connection:ConnectionString"];
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory);
        }
    }
}
