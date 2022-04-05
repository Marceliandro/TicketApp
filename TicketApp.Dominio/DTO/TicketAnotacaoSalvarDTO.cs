using System;

namespace TicketApp.Dominio.DTO
{
    public class TicketAnotacaoSalvarDTO
    {
        public long IdTicket { get; set; }
        public long IdUsuario { get; set; }
        public string Texto { get; set; }
    }
}
