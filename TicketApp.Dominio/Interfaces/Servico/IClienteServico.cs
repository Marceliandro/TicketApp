using TicketApp.Base.Interfaces;
using TicketApp.Dominio.DTO;

namespace TicketApp.Dominio.Interfaces.Servico
{
    public interface IClienteServico : IServicoBase<ResultDTO, ClienteSalvarDTO, ClienteEditarDTO, long>
    {
        ResultDTO Get(string cpf);
    }
}
