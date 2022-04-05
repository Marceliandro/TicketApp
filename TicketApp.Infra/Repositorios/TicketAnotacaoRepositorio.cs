using TicketApp.Base;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Infra.Contextos;

namespace TicketApp.Infra.Repositorios
{
    public class TicketAnotacaoRepositorio : RepositorioBase<TicketAnotacao, long>, ITicketAnotacaoRepositorio
    {
        public TicketAnotacaoRepositorio(TicketContexto context) : base(context) { }
    }
}
