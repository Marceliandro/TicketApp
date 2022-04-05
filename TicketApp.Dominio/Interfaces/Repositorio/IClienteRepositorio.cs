using TicketApp.Base.Interfaces;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Dominio.Interfaces.Repositorio
{
    public interface IClienteRepositorio : IRepositorioBase<Cliente, long>
    {
        long GetCodigoSequence();
    }
}
