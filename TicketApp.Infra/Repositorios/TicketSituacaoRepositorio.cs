using TicketApp.Base;
using TicketApp.Dominio.Entidades;
using TicketApp.Dominio.Interfaces.Repositorio;
using TicketApp.Infra.Contextos;

namespace TicketApp.Infra.Repositorios
{
    public class TicketSituacaoRepositorio : RepositorioBase<TicketSituacao, short>, ITicketSituacaoRepositorio
    {
        public TicketSituacaoRepositorio(TicketContexto context) : base(context) { }
    }
}
