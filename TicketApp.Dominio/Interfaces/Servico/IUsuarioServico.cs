using TicketApp.Base.Interfaces;
using TicketApp.Dominio.DTO;

namespace TicketApp.Dominio.Interfaces.Servico
{
    public interface IUsuarioServico : IServicoBase<ResultDTO, UsuarioSalvarDTO, UsuarioEditarDTO, long>
    {
        ResultDTO Get(string nome, string login);
        ResultDTO Auth(UsuarioAuthDTO usuarioAuthDTO);
    }
}
