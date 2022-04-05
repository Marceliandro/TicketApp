using TicketApp.Base.Interfaces;
using TicketApp.Dominio.DTO;

namespace TicketApp.Dominio.Interfaces.Servico
{
    public interface ITicketAnotacaoServico : IServicoBase<ResultDTO, TicketAnotacaoSalvarDTO, TicketAnotacaoEditarDTO, long>
    {
        ResultDTO AnotacoesTicket(long IdTicket);
    }
}
