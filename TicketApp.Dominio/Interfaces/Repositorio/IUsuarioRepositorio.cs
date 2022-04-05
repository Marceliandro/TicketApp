using TicketApp.Base.Interfaces;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Dominio.Interfaces.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario, long>
    {
        int GetCodigoSequence();
    }
}
