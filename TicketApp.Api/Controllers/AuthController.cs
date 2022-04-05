using Microsoft.AspNetCore.Mvc;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        public AuthController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UsuarioAuthDTO usuarioAuthDTO)
        {
            return Ok(_usuarioServico.Auth(usuarioAuthDTO));
        }
    }
}
