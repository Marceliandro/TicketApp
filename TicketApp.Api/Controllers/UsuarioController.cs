using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1"), Authorize("Bearer")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        public IActionResult Get(string nome, string login)
        {
            return Ok(_usuarioServico.Get(nome, login));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_usuarioServico.GetById(id));
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Post([FromBody] UsuarioSalvarDTO usuarioSalvarDTO)
        {
            return Created("", _usuarioServico.Salvar(usuarioSalvarDTO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] UsuarioEditarDTO usuarioEditarDTO)
        {
            return Ok(_usuarioServico.Editar(usuarioEditarDTO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _usuarioServico.Excluir(id);
            return NoContent();
        }
    }
}
