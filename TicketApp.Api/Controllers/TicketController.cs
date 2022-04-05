using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Dominio.DTO;
using TicketApp.Dominio.Interfaces.Servico;

namespace TicketApp.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1"), Authorize("Bearer")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServico _ticketServico;

        public TicketController(ITicketServico ticketServico)
        {
            _ticketServico = ticketServico;
        }

        [HttpGet,Route("BuscaDetalhada")]
        public IActionResult Get(int codigoTicket, string codigoUsuario, string nomeUsuario, string codigoCliente, string cpfCliente)
        {
            return Ok(_ticketServico.BuscaDetalhada(codigoTicket, codigoUsuario, nomeUsuario, codigoCliente, cpfCliente));
        }

        [HttpPost, Route("Criar")]
        public IActionResult Post([FromBody] TicketSalvarDTO ticketSalvarDTO)
        {
            return Created("", _ticketServico.Salvar(ticketSalvarDTO));
        }

        [HttpPut, Route("Concluir")]
        public IActionResult Put([FromBody] TicketEditarDTO ticketEditarDTO)
        {
            return Ok(_ticketServico.Editar(ticketEditarDTO));
        }
    }
}
