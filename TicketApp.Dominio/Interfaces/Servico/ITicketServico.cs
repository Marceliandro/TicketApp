using System.Collections.Generic;
using TicketApp.Base.Interfaces;
using TicketApp.Dominio.DTO;

namespace TicketApp.Dominio.Interfaces.Servico
{
    public interface ITicketServico : IServicoBase<ResultDTO, TicketSalvarDTO, TicketEditarDTO, long>
    {
        ResultDTO BuscaDetalhada(int codigoTicket, string codigoUsuario, string nomeUsuario, string codigoCliente, string cpfCliente);
    }
}
