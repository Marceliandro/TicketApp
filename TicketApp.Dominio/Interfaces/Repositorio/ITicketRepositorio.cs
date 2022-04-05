using System.Collections.Generic;
using TicketApp.Base.Interfaces;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Entidades;

namespace TicketApp.Dominio.Interfaces.Repositorio
{
    public interface ITicketRepositorio : IRepositorioBase<Ticket, long>
    {
        int GetCodigoSequence();
        IEnumerable<ViewTicketDetalhesDTO> BuscaDetalhada(int codigoTicket, string codigoUsuario, string nomeUsuario, string codigoCliente, string cpfCliente);
    }
}
