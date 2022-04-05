using TicketApp.Base.Interfaces;
using TicketApp.Dominio.DTO;

namespace TicketApp.Dominio.Interfaces.Servico
{
    public interface ITicketSituacaoServico : IServicoBase<ResultDTO, TicketSituacaoSalvarDTO, TicketSituacaoEditarDTO, short>
    {
        ResultDTO Get();
    }
}
