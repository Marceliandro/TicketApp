using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1"), Authorize("Bearer")]
    [ApiController]
    public class TicketSituacaoController : ControllerBase
    {
        private readonly ITicketSituacaoServico _ticketSituacaoServico;
        public TicketSituacaoController(ITicketSituacaoServico ticketSituacaoServico)
        {
            _ticketSituacaoServico = ticketSituacaoServico;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ticketSituacaoServico.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(short id)
        {
            return Ok(_ticketSituacaoServico.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] TicketSituacaoSalvarDTO ticketSituacaoSalvarDTO)
        {
            return Created("", _ticketSituacaoServico.Salvar(ticketSituacaoSalvarDTO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] TicketSituacaoEditarDTO ticketSituacaoEditarDTO)
        {
            return Ok(_ticketSituacaoServico.Editar(ticketSituacaoEditarDTO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(short id)
        {
            _ticketSituacaoServico.Excluir(id);
            return NoContent();
        }
    }
}
