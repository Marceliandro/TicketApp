using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1"), Authorize("Bearer")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;
        public ClienteController(IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        [HttpGet]
        public IActionResult Get(string cpf)
        {
            return Ok(_clienteServico.Get(cpf));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_clienteServico.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteSalvarDTO clienteSalvarDTO)
        {
            return Created("", _clienteServico.Salvar(clienteSalvarDTO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] ClienteEditarDTO clienteEditarDTO)
        {
            return Ok(_clienteServico.Editar(clienteEditarDTO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _clienteServico.Excluir(id);
            return NoContent();
        }
    }
}
