using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1"), Authorize("Bearer")]
    [ApiController]
    public class TicketAnotacaoController : ControllerBase
    {
        private readonly ITicketAnotacaoServico _ticketAnotacaoServico;

        public TicketAnotacaoController(ITicketAnotacaoServico ticketAnotacaoServico)
        {
            _ticketAnotacaoServico = ticketAnotacaoServico;
        }

        [HttpGet, Route("AnotacoesTicket")]
        public IActionResult AnotacoesTicket(long IdTicket)
        {
            return Ok(_ticketAnotacaoServico.AnotacoesTicket(IdTicket));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_ticketAnotacaoServico.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] TicketAnotacaoSalvarDTO ticketAnotacaoSalvarDTO)
        {
            return Created("", _ticketAnotacaoServico.Salvar(ticketAnotacaoSalvarDTO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] TicketAnotacaoEditarDTO ticketAnotacaoEditarDTO)
        {
            return Ok(_ticketAnotacaoServico.Editar(ticketAnotacaoEditarDTO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _ticketAnotacaoServico.Excluir(id);
            return NoContent();
        }
    }
}
