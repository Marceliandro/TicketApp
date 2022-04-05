using System;

namespace TicketApp.Dominio.DTO
{
    public class TicketAnotacaoDTO
    {
        public long Id { get; set; }
        public long IdTicket { get; set; }
        public long IdUsuario { get; set; }
        public string Texto { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
